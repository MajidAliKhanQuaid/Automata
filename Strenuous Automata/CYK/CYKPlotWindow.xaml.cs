using CYKAlgorithmLibrary;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Strenuous_Automata.CYK
{
    public partial class CYKPlotWindow : Window
    {
        public CYKPlotWindow()
        {
            InitializeComponent();
        }

        private void btnCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Do you want to Quit ?", "Strenuous Automata", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }


        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            //List<string> formattedInput = Input.ForwardPermute("ababa", new List<string>(), 1);
            //List<Production> productions = Production.GetHardcodedProductions();
            //List<Result> results = Result.GetResults(formattedInput, productions);
            ////
            //Plot(formattedInput, productions, results);
        }

        private void Plotter(List<string> formattedInput, List<Production> productions, List<Result> results)
        {
            int row = 1;
            int maxY = 0;
            int maxX = 0;
            //
            for (int i = results.Count - 1; i >= 0;)
            {
                for (int j = row - 1; j > 0 && i >= 0; j--)
                {
                    Border border = null;
                    Result result = results[i];
                    //
                    string values = string.Empty;
                    if (result.value.Count == 0)
                    {
                        values = "-";
                    }
                    else
                    {
                        foreach (var value in result.value)
                        {
                            values += value;
                        }
                    }
                    border = GetBorder(result.key, values);
                    //
                    maxY = (row - 1) * 100;
                    if(j == row - 1)
                    {
                        maxX = j * 100;
                    }
                    //
                    Canvas.SetTop(border, (row - 1) * 100);
                    Canvas.SetLeft(border, j * 100);
                    canvas.Children.Add(border);
                    //
                    i--;
                }
                row++;
            }
            //
            canvas.Height = maxY + 100;
            canvas.Width = maxX + 100;
        }

        public void PlotChart(string InputString, List<Production> Productions)
        {
            if (!string.IsNullOrEmpty(InputString) && Productions != null)
            {
                try
                {
                    List<string> formattedInput = Input.ForwardPermute(InputString, new List<string>());
                    List<Result> results = Result.GetResults(formattedInput, Productions);
                    //
                    Plotter(formattedInput, Productions, results);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Could not plot the Graph");
                    //
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid Input Parameters");
            }
        }

        public Border GetBorder(string input, string value)
        {
            int dimension = 100;
            //
            Border border = new Border();
            border.Height = border.Width = dimension;
            border.BorderBrush = Brushes.Transparent;
            border.BorderThickness = new Thickness(1);
            border.Visibility = System.Windows.Visibility.Visible;
            //
            StackPanel stackPanel = new StackPanel();
            Label labelValue = new Label();
            labelValue.Height = dimension / 2;
            //
            labelValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            labelValue.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            //
            labelValue.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#3498db ")); ;
            labelValue.Content = value;
            //
            Label labelInput = new Label();
            labelInput.Height = dimension / 2;
            //
            labelInput.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            labelInput.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            //
            labelInput.Background = Brushes.White;
            labelInput.Content = input;
            //
            stackPanel.Children.Add(labelValue);
            stackPanel.Children.Add(labelInput);
            //
            border.Child = stackPanel;
            //
            return border;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
