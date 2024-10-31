using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Numerics;
using System.DirectoryServices.ActiveDirectory;
using System.Collections.Specialized;

namespace progress_check_task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Make our storage controller accessible on the main window
        public StorageController store;

        public string lastQuestion;

        // If we've managed to load the planets data
        // add our planets to the combo box in the UI.
        public void InitPlanets()
        {
            if (this.store.data?.planets != null)
            {
                PlanetsComboBox.Items.Clear();

                foreach (var planet in this.store.data.planets)
                {
                    PlanetsComboBox.Items.Add(planet.Key);
                }

                PlanetsComboBox.SelectedIndex = 0;
            }
        }

        // Toggles the enabled state of the planet rocker buttons
        // as this allows the user to quickly move between items
        // in the combo box without opening the dropdown.
        public void UpdatePlanetRockerButtons()
        {
            int planetSelectionIndex = PlanetsComboBox.SelectedIndex;
            string planetSelection = (string)PlanetsComboBox.SelectedValue;

            // Previous planet is easy because we can only ever be enabled if our index isn't 0
            PrevPlanet.IsEnabled = planetSelectionIndex > 0;

            // Next planet requires us to ensure the index is less than the combo box items count
            NextPlanet.IsEnabled = planetSelectionIndex < PlanetsComboBox.Items.Count - 1;
        }

        public MainWindow()
        {
            // Init all the things
            this.store = new StorageController();

            InitializeComponent();
            InitPlanets();
            UpdatePlanetRockerButtons();

            // Ensure the question input is focused right away
            QuestionInput.Focus();

            ((INotifyCollectionChanged)Response.Items).CollectionChanged += Response_CollectionChanged;
        }

        // Handler to listen for changes to the combo box selection
        // this allows us to update the rocker buttons and access planet
        // information to fill out the UI.
        private void PlanetsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePlanetRockerButtons();

            string planetSelection = (string)PlanetsComboBox.SelectedValue;
            Planet planet;

            // Safely try access the planet in the planets dictionary
            // This ensures if the planet disappears from the store dictionary
            // we don't encounter a race condition 
            if (this.store.data.planets.TryGetValue(planetSelection, out planet))
            {
                Debug.WriteLine($"got planet {planetSelection}");

                PlanetName.Content = planetSelection;
                FactsTitle.Content = "Facts about " + planetSelection;
                FactsLabel.Content = "";

                // Limitations with JSON deserialisation:
                // We're unable to store the full distance from the sun in KM
                // as the number exceeds the allowed JSON max interger for the C#
                // JSON serialiser.
                //
                // Workaround this by storing a value at a lower power and then
                // multiplying by 1000 once deserialised into a long.
                long distanceFromSun = planet.metadata.distance_from_sun_km * 1000;

                // Use n0 to format the numbers in the user's locale with commas
                MetadataLabel.Content =
                    $"Diameter (km): {planet.metadata.diameter_km:n0}km\n" +
                    $"Distance from sun (km): {distanceFromSun:n0}km\n" +
                    $"Orbital period in days: {planet.metadata.orbital_period_days:n0}\n" +
                    $"Number of moons: {planet.metadata.number_of_moons:n0}\n";

                // Handle case for dwarf planets as well as the optional dwarf since value.
                if (planet.dwarf)
                {
                    MetadataLabel.Content += $"\n{planetSelection} is not considered to be a planet, as it has been designated as dwarf planet status";

                    if (planet.dwarf_since != null)
                    {
                        MetadataLabel.Content += $" since {planet.dwarf_since}.";
                    } else
                    {
                        MetadataLabel.Content += ".";
                    }
                }
                else
                {
                    MetadataLabel.Content += $"\n{planetSelection} is a planet.";
                }

                // Iterate over all facts for each planet and add them to the string
                foreach (string fact in planet.facts)
                {
                    FactsLabel.Content += fact + "\n";
                }
            }
            else
            {
                Debug.WriteLine($"Error getting planet information for '{planetSelection}'.");
            }
        }

        private void PrevPlanet_Click(object sender, RoutedEventArgs e)
        {
            PlanetsComboBox.SelectedIndex--;
        }

        private void NextPlanet_Click(object sender, RoutedEventArgs e)
        {
            PlanetsComboBox.SelectedIndex++;
        }

        // Common method for answering to an input using a set of keywords
        private void AnswerPrompt(string input, string[] keywords, Func<string, bool> callback)
        {
            foreach (string keyword in keywords)
            {
                // Clean up our keyword to verify that our 
                // contains the same formatted substring
                if (input.Contains(Regex.Replace(keyword.Trim().ToLower(), @"\s", string.Empty)))
                {
                    // If our callback lambda succeeds, break out the iterator
                    // we're done here.
                    if (callback(keyword))
                    {
                        break;
                    }
                }
            }
        }

        // Given the input and inferred planet name
        // we handle all potential prompt cases using their common keywords
        // This allows us to handle each type of prompt individually, allowing
        // multiple questions to be answered in one prompt.
        private void AnswerCommonFact(string originalInput, string input, string planetName)
        {
            bool didReply = false;
            Planet planet = this.store.data.planets[planetName];

            AnswerPrompt(input, new string[] { "diameter", "big", "size", "large", "small", "radius", "radii", "length", "massive" }, keyword =>
            {
                int size = planet.metadata.diameter_km;

                // Handle the case for radius when grabbing the diameter
                // since we store it as the diameter, calculating the radius
                // can be done by halving the original diameter and adjusting
                // the prose in the message box.
                if (keyword == "radius" || keyword == "radii")
                {
                    keyword = "radius";
                    size = size / 2;
                } else if (keyword != "diameter" || keyword != "radius" || keyword != "radii" || keyword != "size")
                {
                    keyword = "diameter";
                }

                ReplyMessage(originalInput, $"The {keyword} of {planetName} is {size:n0}km");
                didReply = true;

                return true;
            });

            AnswerPrompt(input, new string[] { "from sun", "from the sun", "distance from sun", "distance sun", "distance", "far away" }, keyword =>
            {
                // Limitations with JSON deserialisation:
                // We're unable to store the full distance from the sun in KM
                // as the number exceeds the allowed JSON max interger for the C#
                // JSON serialiser.
                //
                // Workaround this by storing a value at a lower power and then
                // multiplying by 1000 once deserialised into a long.
                long dist = planet.metadata.distance_from_sun_km * 1000;

                ReplyMessage(originalInput, $"{planetName} is {dist:n0}km away from the sun.");
                didReply = true;

                return true;
            });

            AnswerPrompt(input, new string[] { "orbital period", "year", "period", "orbital" }, keyword =>
            {
                int period = planet.metadata.orbital_period_days;

                ReplyMessage(originalInput, $"The orbital period of {planetName} is {period:n0} days.");
                didReply = true;

                return true;
            });

            AnswerPrompt(input, new string[] { "moons", "planets" }, keyword =>
            {
                int moons = planet.metadata.number_of_moons;

                ReplyMessage(originalInput, $"{planetName} has {moons:n0} moons.");
                didReply = true;

                return true;
            });

            AnswerPrompt(input, new string[] { "a planet", "is a planet", "is not a planet", "is dwarf", "planet", "dwarf" }, keyword =>
            {
                bool isDwarf = planet.dwarf;
                string dwarfSince = planet.dwarf_since;

                if (isDwarf)
                {
                    ReplyMessage(originalInput, $"{planetName} is not considered to be a planet, as it has been designated as a dwarf planet since {dwarfSince}.");
                } else
                {
                    ReplyMessage(originalInput, $"{planetName} is considered to be a planet.");
                }

                didReply = true;

                return true;
            });

            // If we never received a reply from the prompt handlers
            // we can display a message alerting the user that their question
            // was not handled at all.
            if (!didReply)
            {
                ReplyMessage(originalInput, $"That question for {planetName} was unrecognised, please try again.");
            }
        }

        // Public facing method used by the question window to pass questions
        // to the main window. This allows cross communication between these
        // windows easier.
        public void OnQuestionReceived(string input, string matchedPlanet)
        {
            PlanetsComboBox.SelectedValue = matchedPlanet;

            // Sanitise the input and remove all whitespace so
            // we have a clean string to work with
            string sanitisedInput = Regex.Replace(input.Trim().ToLower(), @"\s", string.Empty);

            AnswerCommonFact(input, sanitisedInput, matchedPlanet);
        }

        private void ReplyMessage(string input, string output)
        {
            QuestionInput.Text = "";

            Response.Items.Add($"{input}:\n    {output}");
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string input = QuestionInput.Text;

            if (input.Trim().Length <= 0)
            {
                return;
            }

            lastQuestion = input;

            // Get all the planet names
            List<string> planets = this.store.data.planets.Keys.ToList();

            // This attempts to infer a planet from the prompt, using string processing
            // to find a potential planet substring match in the input.
            // If no match is found, this will be < 0
            int planetMatchIndex = planets.FindIndex(p => input.ToLower().Trim().Contains(p.ToLower()));

            if (planetMatchIndex >= 0)
            {
                string matchedPlanet = planets[planetMatchIndex];

                OnQuestionReceived(input, matchedPlanet);
            }
            else
            {
                ReplyMessage(input, $"No planet could be inferred from your question, please try again.");
            }
        }


        private void Response_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Response.Items.MoveCurrentToLast();
            Response.ScrollIntoView(Response.Items.CurrentItem);
        }

        private void Response_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}