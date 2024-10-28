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

namespace progress_check_task
{
    public class PlanetData
    {
        public Dictionary<string, Planet> planets { get; set; }
    }

    public class Planet
    {
        public Metadata metadata { get; set; }
        public List<string> facts { get; set; }
    }

    public class Metadata
    {
        public int diameter_km { get; set; }
        public int distance_from_sun_km { get; set; }
        public int orbital_period_days { get; set; }
        public int number_of_moons { get; set; }
    }

    public static class PlanetsJSONReader
    {
        public static T Read<T>(string filePath)
        { 
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text)!;

        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlanetData data;

        public void InitPlanets()
        {
            string planetsPath = System.IO.Path.Combine(
                Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName,
                "assets",
                "planets.json"
            );

            string json = File.ReadAllText(planetsPath);

            Debug.WriteLine(json);

            try
            {
                this.data = JsonSerializer.Deserialize<PlanetData>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserialising JSON: {ex.Message}");
            }

            if (this.data?.planets != null)
            {
                PlanetsComboBox.Items.Clear();

                foreach (var planet in this.data.planets)
                {
                    PlanetsComboBox.Items.Add(planet.Key);
                }

                PlanetsComboBox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Planet data could not be read or is missing.");
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
            InitializeComponent();
            InitPlanets();
            UpdatePlanetRockerButtons();
        }

        private void PlanetsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePlanetRockerButtons();

            string planetSelection = (string)PlanetsComboBox.SelectedValue;
            Planet planet;

            if (this.data.planets.TryGetValue(planetSelection, out planet))
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
    }
}