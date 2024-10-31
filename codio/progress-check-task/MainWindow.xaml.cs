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

namespace progress_check_task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public StorageController store;

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

        public void UpdatePlanetRockerButtons()
        {
            int planetSelectionIndex = PlanetsComboBox.SelectedIndex;
            string planetSelection = (string)PlanetsComboBox.SelectedValue;

            PrevPlanet.IsEnabled = planetSelectionIndex > 0;
            NextPlanet.IsEnabled = planetSelectionIndex < PlanetsComboBox.Items.Count - 1;
        }

        public MainWindow()
        {
            this.store = new StorageController();

            InitializeComponent();
            InitPlanets();
            UpdatePlanetRockerButtons();
        }

        private void PlanetsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePlanetRockerButtons();

            string planetSelection = (string)PlanetsComboBox.SelectedValue;
            Planet planet;

            if (this.store.data.planets.TryGetValue(planetSelection, out planet))
            {
                Debug.WriteLine($"got planet {planetSelection}");

                PlanetName.Content = planetSelection;
                FactsTitle.Content = "Facts about " + planetSelection;
                FactsLabel.Content = "";

                long distanceFromSun = planet.metadata.distance_from_sun_km * 1000;

                MetadataLabel.Content =
                    $"Diameter (km): {planet.metadata.diameter_km:n0}km\n" +
                    $"Distance from sun (km): {distanceFromSun:n0}km\n" +
                    $"Orbital period in days: {planet.metadata.orbital_period_days:n0}\n" +
                    $"Number of moons: {planet.metadata.number_of_moons:n0}\n";

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

        private void AskQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionWindow win = new QuestionWindow();
            win.ShowDialog();
        }

        private void AnswerPrompt(string input, string[] keywords, Func<string, bool> callback)
        {
            foreach (string keyword in keywords)
            {
                if (input.Contains(Regex.Replace(keyword.Trim().ToLower(), @"\s", string.Empty)))
                {
                    if (callback(keyword))
                    {
                        break;
                    }
                }
            }
        }

        private void AnswerCommonFact(string input, string planetName)
        {
            bool didReply = false;
            Planet planet = this.store.data.planets[planetName];

            AnswerPrompt(input, new string[] { "diameter", "big", "size", "large", "small", "radius", "radii", "length", "massive" }, keyword =>
            {
                int size = planet.metadata.diameter_km;

                if (keyword == "radius" || keyword == "radii")
                {
                    keyword = "radius";
                    size = size / 2;
                } else if (keyword != "diameter" || keyword != "radius" || keyword != "radii" || keyword != "size")
                {
                    keyword = "diameter";
                }

                MessageBox.Show($"The {keyword} of {planetName} is " + size + "km", planetName, MessageBoxButton.OK, MessageBoxImage.Information);
                didReply = true;

                return true;
            });

            AnswerPrompt(input, new string[] { "from sun", "from the sun", "distance from sun", "distance sun", "distance", "far away" }, keyword =>
            {
                int dist = planet.metadata.distance_from_sun_km;

                MessageBox.Show($"{planetName} is " + dist + "km away from the sun.", planetName, MessageBoxButton.OK, MessageBoxImage.Information);
                didReply = true;

                return true;
            });

            AnswerPrompt(input, new string[] { "orbital period", "year", "period", "orbital" }, keyword =>
            {
                int period = planet.metadata.orbital_period_days;

                MessageBox.Show($"The orbital period of {planetName} is " + period + " days.", planetName, MessageBoxButton.OK, MessageBoxImage.Information);
                didReply = true;

                return true;
            });

            AnswerPrompt(input, new string[] { "moons", "planets" }, keyword =>
            {
                int moons = planet.metadata.number_of_moons;

                MessageBox.Show($"{planetName} has " + moons + " moons.", planetName, MessageBoxButton.OK, MessageBoxImage.Information);
                didReply = true;

                return true;
            });

            AnswerPrompt(input, new string[] { "a planet", "is a planet", "is not a planet", "is dwarf", "planet", "dwarf" }, keyword =>
            {
                bool isDwarf = planet.dwarf;
                string dwarfSince = planet.dwarf_since;

                if (isDwarf)
                {
                    MessageBox.Show($"{planetName} is not considered to be a planet, as it has been designated as a dwarf planet since {dwarfSince}.", planetName, MessageBoxButton.OK, MessageBoxImage.Information);
                } else
                {
                    MessageBox.Show($"{planetName} is considered to be a planet.", planetName, MessageBoxButton.OK, MessageBoxImage.Information);
                }

                didReply = true;

                return true;
            });

            if (!didReply)
            {
                MessageBox.Show($"That question for {planetName} was unrecognised, please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void OnQuestionReceived(string input, string matchedPlanet)
        {
            PlanetsComboBox.SelectedValue = matchedPlanet;

            string sanitisedInput = Regex.Replace(input.Trim().ToLower(), @"\s", string.Empty);

            AnswerCommonFact(sanitisedInput, matchedPlanet);
        }
    }
}