using System.Windows;

namespace Strenuous_Automata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Do you want to Quit ?", "Strenuous Automata", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(dialogResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            new CYK.CYKInputWindow().Show();
        }
    }
}
