using System.Windows;

namespace Strenuous_Automata
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSubtractor_Click(object sender, RoutedEventArgs e)
        {
            new Subtractor.SubtractorWindow().Show();
        }

        private void btnCYK_Click(object sender, RoutedEventArgs e)
        {
            new CYK.CYKInputWindow().Show();
        }
    }
}
