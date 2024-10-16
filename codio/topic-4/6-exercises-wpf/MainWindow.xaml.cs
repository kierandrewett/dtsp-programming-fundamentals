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

namespace ShoppingList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateDeleteDisabled();

        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            string addItemInput = AddItemInput.Text;

            ItemList.Items.Add(addItemInput);
            UpdateDeleteDisabled();
        }

        private void AddItemClearBtn_Click(object sender, RoutedEventArgs e)
        {
            AddItemInput.Text = "";
            UpdateDeleteDisabled();
        }

        private void UpdateDeleteDisabled()
        {
            DeleteItemBtn.IsEnabled = ItemList.SelectedIndex != -1;
        }

        private void DeleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            ItemList.Items.RemoveAt(ItemList.SelectedIndex);

            if (ItemList.SelectedIndex + 1 <= ItemList.Items.Count)
            {
                ItemList.SelectedIndex += 1;
            }

            UpdateDeleteDisabled();

        }

        private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDeleteDisabled();

        }
    }
}