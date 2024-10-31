using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace progress_check_task
{
    public class PlanetData
    {
        public Dictionary<string, Planet> planets { get; set; }
    }

    public class Planet
    {
        public bool dwarf { get; set; }
        public string dwarf_since { get; set; }
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

    public class StorageController
    {
        public PlanetData data;
        
        public StorageController() 
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

            if (this.data?.planets == null)
            {
                MessageBox.Show("Planet data could not be read or is missing.");
            }
        }
    }
}
