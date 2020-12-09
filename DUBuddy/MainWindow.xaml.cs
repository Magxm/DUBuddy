using System.Windows;

using DUBuddy.IndustryCalculator;

namespace DUBuddy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IndustryCalculatorPage industryCalculatorPage;
        public MainWindow()
        {
            Statics.Init();
            InitializeComponent();
            industryCalculatorPage = new IndustryCalculatorPage();
        }

        private void IndustryCalculator_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ContentFrame.Content = industryCalculatorPage;
            ModuleText.Text = "Industry Calculator";
        }
    }
}
