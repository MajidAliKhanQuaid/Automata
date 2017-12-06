using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TuringLibrary;

namespace Strenuous_Automata.Subtractor
{
    public partial class SubtractorWindow : Window
    {
        public SubtractorWindow()
        {
            InitializeComponent();
        }

        private void btnCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            hintPopUp.IsOpen = false;
            //
            MessageBoxResult dialogResult = MessageBox.Show("Do you want to Quit ?", "Strenuous Automata", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                Regex regex = new Regex("[^0-9]");
                textBox.Text = regex.Replace(textBox.Text, "");
                if (textBox.Text.Trim().Length == 0)
                {
                    textBox.Text = "0";
                }
            }
        }

        private void btnClosePopUp_Click(object sender, RoutedEventArgs e)
        {
            hintPopUp.IsOpen = false;
        }

        private void btnShowResult_Click(object sender, RoutedEventArgs e)
        {
            dgResults.Items.Clear();
            //
            try
            {
                if (txtFirstNum.Text.Trim().Length == 0 || txtSecondNum.Text.Trim().Length == 0)
                {
                    MessageBox.Show(" Operand can't be empty ", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
                    dgResults.Items.Refresh();
                    return;
                }

                int firstNum = int.Parse(txtFirstNum.Text);
                int secondNum = int.Parse(txtSecondNum.Text);

                if (firstNum == 0 || secondNum == 0)
                {
                    MessageBox.Show(" '0' is not allowed as an operand", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
                    dgResults.Items.Refresh();
                    return;
                }
                //
                List<string> results = TuringLibrary.Subtractor.GetResult(firstNum, secondNum);
                foreach (var result in results)
                {
                    dgResults.Items.Add(result);
                }
                dgResults.Items.Refresh();
                //
            }
            catch (Exception ex)
            {
            }

        }

    }
}
