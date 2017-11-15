using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using CYKAlgorithmLibrary;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Strenuous_Automata.CYK
{
    public partial class CYKInputWindow : Window
    {
        //
        bool hiddenStacksExists = false; // That's for optimization
        //
        public CYKInputWindow()
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


        private void DeleteValues_Click(object sender, RoutedEventArgs e)
        {
            List<Production> productions = GetProductionValuesFromUI();
            foreach (var production in productions)
            {
                string message = production.key + " -> ";
                foreach (var prodValue in production.values)
                {
                    message += prodValue + " | ";
                }
                MessageBox.Show(message);
            }
        }

        private List<Production> GetProductionValuesFromUI()
        {
            List<Production> productions = new List<Production>();
            //
            List<StackPanel> childStacks = stackProductions.Children.OfType<StackPanel>().Where(x => x.Visibility != Visibility.Collapsed).ToList<StackPanel>();
            foreach (StackPanel stackPanel in childStacks)
            {
                Production production = new Production();
                //
                List<TextBox> textBoxes = stackPanel.Children.OfType<TextBox>().ToList<TextBox>();
                TextBox txtNonTerminal = textBoxes.Where(x => x.Tag.ToString() == "txtNonTerminal").FirstOrDefault();
                if (txtNonTerminal != null)
                {
                    Regex regexForTerminal = new Regex(@"[A-W]");
                    Match matchNonTerminal = regexForTerminal.Match(txtNonTerminal.Text);
                    if (matchNonTerminal.Value == txtNonTerminal.Text)
                    {
                        production.key = txtNonTerminal.Text;
                        //
                        List<TextBox> lstTextProdValues = textBoxes.Where(x => x.Tag.ToString() == "txtProdValue").ToList<TextBox>();
                        //
                        if (lstTextProdValues != null)
                        {
                            List<string> productionValues = new List<string>();
                            //
                            foreach (TextBox txtProdValue in lstTextProdValues)
                            {
                                txtProdValue.Text = RemoveSpaces(txtProdValue.Text);
                                Regex regexForProductionValues = new Regex(@"[a-zA-Z]+");
                                Match matchProdValue = regexForProductionValues.Match(txtProdValue.Text);
                                if (matchProdValue.Value == txtProdValue.Text)
                                {
                                    productionValues.Add(txtProdValue.Text);
                                }
                            }
                            production.values = productionValues.ToArray<string>();
                        }
                    }
                }
                //
                if (production.values.Count() > 0)
                {
                    productions.Add(production);    // Empty Values are ignored
                }
            }
            //
            return productions;
        }

        private void AddNewProduction(Production production = null)
        {
            //
            if (hiddenStacksExists == true)
            {
                RemoveHiddenProductionStackPanels();
            }
            //
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Margin = new Thickness(0, 20, 0, 0);
            //
            TextBox txtNonTerminal = new TextBox();
            txtNonTerminal.MaxLength = 1;
            txtNonTerminal.CharacterCasing = CharacterCasing.Upper;
            txtNonTerminal.Tag = "txtNonTerminal";
            if (production != null)
            {
                txtNonTerminal.Text = production.key;
            }
            //
            txtNonTerminal.TextChanged += txtNonTerminal_Click;
            //
            BitmapImage bmpArrow = new BitmapImage();
            bmpArrow.BeginInit();
            bmpArrow.UriSource = new Uri("../Images/red-arrow.png", UriKind.Relative);
            bmpArrow.EndInit();
            //
            Image imgGoesTo = new Image();
            imgGoesTo.Source = bmpArrow;
            //
            var goesToStyle = Application.Current.FindResource("imgRedArrowStyle");
            if (goesToStyle != null && goesToStyle.GetType() == typeof(Style))
            {
                imgGoesTo.Style = (Style)goesToStyle;
            }
            //
            stackPanel.Children.Add(txtNonTerminal);
            stackPanel.Children.Add(imgGoesTo);
            //
            if (production != null)
            {
                for (int i = 0; i < production.values.Count(); i++)
                {
                    TextBox txtProdValue = new TextBox();
                    txtProdValue.TextChanged += txtProdValues_Click;
                    txtProdValue.Tag = "txtProdValue";
                    txtProdValue.Text = production.values[i];
                    //
                    BitmapImage bmpSeperator = new BitmapImage();
                    bmpSeperator.BeginInit();
                    bmpSeperator.UriSource = new Uri("../Images/blue-seperate.png", UriKind.Relative);
                    bmpSeperator.EndInit();
                    //
                    Image imgSeperator = new Image();
                    imgSeperator.Source = bmpSeperator;
                    //
                    var barStyle = Application.Current.FindResource("imgBlueSeperatorStyle");
                    if (barStyle != null && barStyle.GetType() == typeof(Style))
                    {
                        imgSeperator.Style = (Style)barStyle;
                    }
                    //
                    stackPanel.Children.Add(txtProdValue);
                    stackPanel.Children.Add(imgSeperator);
                }
                //
                for (int i = 1; i < 4 - production.values.Count(); i++)
                {
                    TextBox txtProdValue = new TextBox();
                    txtProdValue.TextChanged += txtProdValues_Click;
                    txtProdValue.Tag = "txtProdValue";
                    txtProdValue.Text = "";
                    //
                    BitmapImage bmpSeperator = new BitmapImage();
                    bmpSeperator.BeginInit();
                    bmpSeperator.UriSource = new Uri("../Images/blue-seperate.png", UriKind.Relative);
                    bmpSeperator.EndInit();
                    //
                    Image imgSeperator = new Image();
                    imgSeperator.Source = bmpSeperator;
                    //
                    var barStyle = Application.Current.FindResource("imgBlueSeperatorStyle");
                    if (barStyle != null && barStyle.GetType() == typeof(Style))
                    {
                        imgSeperator.Style = (Style)barStyle;
                    }
                    //
                    stackPanel.Children.Add(txtProdValue);
                    stackPanel.Children.Add(imgSeperator);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    TextBox txtProdValue = new TextBox();
                    txtProdValue.TextChanged += txtProdValues_Click;
                    txtProdValue.Tag = "txtProdValue";
                    //
                    BitmapImage bmpSeperator = new BitmapImage();
                    bmpSeperator.BeginInit();
                    bmpSeperator.UriSource = new Uri("../Images/blue-seperate.png", UriKind.Relative);
                    bmpSeperator.EndInit();
                    //
                    Image imgSeperator = new Image();
                    imgSeperator.Source = bmpSeperator;
                    //
                    var barStyle = Application.Current.FindResource("imgBlueSeperatorStyle");
                    if (barStyle != null && barStyle.GetType() == typeof(Style))
                    {
                        imgSeperator.Style = (Style)barStyle;
                    }
                    //
                    stackPanel.Children.Add(txtProdValue);
                    stackPanel.Children.Add(imgSeperator);
                }
            }
            TextBox txtProdValueLast = new TextBox();
            txtProdValueLast.TextChanged += txtProdValues_Click;
            txtProdValueLast.Tag = "txtProdValue";
            //
            stackPanel.Children.Add(txtProdValueLast);
            //
            Button btnDelete = new Button();
            btnDelete.Click += btnDeleteProduction_Click;
            //
            var deleteBtnTemplate = Application.Current.FindResource("btnDeleteProductionTemplate");
            if (deleteBtnTemplate != null && deleteBtnTemplate.GetType() == typeof(ControlTemplate))
            {
                btnDelete.Template = (ControlTemplate)deleteBtnTemplate;
            }
            //
            stackPanel.Children.Add(btnDelete);
            //
            stackProductions.Children.Add(stackPanel);
        }

        private void RemoveHiddenProductionStackPanels()
        {
            try
            {
                List<StackPanel> childStackPanels = stackProductions.Children.OfType<StackPanel>().ToList<StackPanel>();
                List<StackPanel> hiddenStackPanels = childStackPanels.Where(x => x.Visibility == Visibility.Collapsed).ToList();
                //
                for (int i = 0; i < hiddenStackPanels.Count; i++)
                {
                    stackProductions.Children.Remove(childStackPanels[i]);
                }
                //
                hiddenStacksExists = false;
            }
            catch (Exception exp)
            {
                hiddenStacksExists = true;
            }
        }

        private void btnAddNewProduction_Click(object sender, RoutedEventArgs e)
        {
            AddNewProduction();
        }

        private void btnNewProd_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            stackProductions.Children.Clear();
            //
            AddNewProduction();
            AddNewProduction();
        }

        private void btnLoadProd_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            Nullable<bool> dialogResult = fileDialog.ShowDialog();
            if (dialogResult != null)
            {
                if (dialogResult == true)
                {
                    string path = fileDialog.FileName;
                    List<Production> productions = LoadSavedProductions(path);
                    //
                    stackProductions.Children.Clear();
                    //
                    foreach (var production in productions)
                    {
                        AddNewProduction(production);
                    }
                }
            }
        }

        private void btnSaveProd_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            Nullable<bool> dialogResult = saveDialog.ShowDialog();
            if (dialogResult != null)
            {
                if (dialogResult == true)
                {
                    string path = saveDialog.FileName;
                    List<Production> productions = GetProductionValuesFromUI();
                    bool result = WriteToFile(path, productions);
                    if (result == true)
                    {
                        MessageBox.Show("Productions Successfully Written");
                        return;
                    }
                    MessageBox.Show("Error in Writing Productions");
                }
            }
        }

        private void btnClose_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private List<Production> LoadSavedProductions(string path)
        {
            List<Production> productions = new List<Production>();
            try
            {
                //
                StreamReader streamReader = new StreamReader(path);
                string line = string.Empty;
                while ((line = streamReader.ReadLine()) != null)
                {
                    Production production = ConvertLineToProduction(line);
                    if (production != null)
                    {
                        productions.Add(production);
                    }
                }
            }
            catch (Exception exp)
            {
                return null;
            }
            return productions;
        }

        private Production ConvertLineToProduction(string line)
        {
            // Test These Regular Expressions
            //
            line = RemoveSpaces(line);
            Regex regexForTerminal = new Regex("@[A-W]");
            Regex regexForProductionValues = new Regex(@"[a-zA-Z]+");
            Regex regexForProduction = new Regex(@"[a-wA-W]+->[a-zA-Z]+(\|[a-zA-Z]+)*");
            Match match = regexForProduction.Match(line);
            if (match != null)
            {
                if (line == match.Value)
                {
                    int indexOfGoesTo = line.IndexOf("->");
                    string productionKey = line.Substring(0, 1);
                    //
                    Match mProdKey = regexForProductionValues.Match(productionKey);
                    if (mProdKey.Value == productionKey)
                    {
                        string productionValueString = line.Substring(indexOfGoesTo + 2);
                        string[] productionValues = productionValueString.Split('|');
                        //
                        foreach (var prodValue in productionValues)
                        {
                            Match mProdValue = regexForProductionValues.Match(prodValue);
                            if (mProdValue != null)
                            {
                                if (mProdValue.Value != prodValue)
                                {
                                    return null;
                                }
                            }
                        }
                        //
                        Production production = new Production();
                        production.key = productionKey;
                        production.values = productionValues;
                        return production;
                    }
                }
            }
            return null;
        }

        private string RemoveSpaces(string text)
        {
            return text.Replace(" ", string.Empty);
        }

        private bool WriteToFile(string path, List<Production> productions)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path);
                foreach (var production in productions)
                {
                    string line = production.key + "->";
                    foreach (string prodValue in production.values)
                    {
                        line += prodValue + "|";
                    }
                    line = line.Substring(0, line.Length - 1);
                    //
                    writer.WriteLine(line);
                }
                //
                writer.Flush();
                writer.Close();
                //
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        private void btnAddPlotCYK_Click(object sender, RoutedEventArgs e)
        {
            List<Production> Productions = GetProductionValuesFromUI();
            //
            CYKPlotWindow plotWindow = new CYKPlotWindow();
            plotWindow.Show();
            //
            plotWindow.PlotChart(txtInput.Text, Productions);
        }

        private void btnDeleteProduction_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null)
                {
                    StackPanel stackPanel = (StackPanel)button.Parent;
                    stackPanel.Visibility = Visibility.Collapsed;
                    //
                    hiddenStacksExists = true;
                }
            }
            catch (Exception exp)
            {

            }
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("[^a-z]");
            txtInput.Text = regex.Replace(txtInput.Text, "");
        }

        private void txtProdValues_Click(object sender, TextChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox != null)
            {
                Regex regex = new Regex("[^a-zA-Z]");
                txtBox.Text = regex.Replace(txtBox.Text, "");
            }
        }

        private void txtNonTerminal_Click(object sender, TextChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox != null)
            {
                Regex regex = new Regex("[^A-W]");
                txtBox.Text = regex.Replace(txtBox.Text, "");
            }
        }

    }
}
