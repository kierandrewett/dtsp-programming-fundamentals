using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace progress_check_task
{
    /// <summary>
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        public StorageController store;

        public QuestionWindow()
        {
            this.store = new StorageController();

            InitializeComponent();

            QuestionInput.Focus();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string input = QuestionInput.Text;

            List<string> planets = this.store.data.planets.Keys.ToList();

            int planetMatchIndex = planets.FindIndex(p => input.ToLower().Trim().Contains(p.ToLower()));

            if (planetMatchIndex >= 0)
            {
                string matchedPlanet = planets[planetMatchIndex];

                ((MainWindow)Application.Current.MainWindow).OnQuestionReceived(input, matchedPlanet);
            } else
            {
                MessageBox.Show($"No planet could be inferred from your question, please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
