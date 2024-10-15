using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string previousOperation = "";
        public string operation = "";

        public string previousOperands = "";
        public string operands = "";

        public MainWindow()
        {
            InitializeComponent();

            Output.Text = "";
        }

        private bool IsStrIntOrFloat(string str)
        {
            return int.TryParse(str, out int parsedInt) || float.TryParse(str, out float parsedFloat);
        }

        public void CalculatedButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            string operation = button.Content.ToString();
            string output = Output.Text;

            // Equals
            if (operation == "=")
            {
                string[] outputParsed = Regex.Split(output, "([+\\-×÷])");

                double sum = 0;
                string currOp = "+";

                for (int i = 0; i < outputParsed.Length; i++)
                {
                    string op = outputParsed[i];

                    if (this.IsStrIntOrFloat(op))
                    {
                        if (currOp == "+")
                        {
                            sum += double.Parse(op);
                        }
                        else if (currOp == "-")
                        {
                            sum -= double.Parse(op);
                        }
                        else if (currOp == "×")
                        {
                            sum *= double.Parse(op);
                        }
                        else if (currOp == "÷")
                        {
                            sum /= double.Parse(op);
                        }
                    }
                    else
                    {
                        currOp = op;
                    }

                    Trace.WriteLine(i + ": " + outputParsed[i]);
                }

                Output.Text = sum.ToString();

                return;
            }

            if (operation == "Backspace")
            {
                if (Output.Text.Length >= 1)
                {
                    Output.Text = Output.Text.Substring(0, Output.Text.Length - 1);
                }
                return;
            }

            if (operation == "C")
            {
                Output.Text = "";
                return;
            }

            string lastChar = Output.Text.Length > 0 ? Output.Text.Substring(Output.Text.Length - 1) : "";
            if (Output.Text.Length <= 0 || this.IsStrIntOrFloat(lastChar) || this.IsStrIntOrFloat(operation))
            {
                Output.Text += operation;
            }
        }
    }
}
