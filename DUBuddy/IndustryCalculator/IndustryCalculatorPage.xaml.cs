using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DUBuddy.IndustryCalculator
{
    public partial class IndustryCalculatorPage : Page
    {
        public List<IndustryResult> WantedProducts;

        public IndustryProductList ProductList;
        public IndustryCalculatorPage()
        {
            InitializeComponent();

            ProductList = IndustryProductList.GetInstance();
            WantedProducts = new List<IndustryResult>();
            WantedResultsDataGrid.ItemsSource = WantedProducts;
        }

        private void CommitEdits()
        {
            //Don't ask me why this needs to be called twice....It will commit all pending edits
            WantedResultsDataGrid.CommitEdit();
            WantedResultsDataGrid.CommitEdit();
            WantedResultsDataGrid.Items.Refresh();
        }

        private void AddProductButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CommitEdits();
            //Adding a new product.
            WantedProducts.Add(new IndustryResult(typeof(DUBuddy.Items.AssemblyLine_M), 1));
            WantedResultsDataGrid.Items.Refresh();
        }

        public static T FindParentControl<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParentControl<T>(parentObject);
        }

        private void DeleteWantedProduct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var parent = FindParentControl<DataGrid>(sender as DependencyObject);
            var row = ItemsControl.ContainerFromElement((DataGrid)parent, (e.OriginalSource as DependencyObject)) as DataGridRow;
            if (row == null)
            {
                return;
            }
            IndustryResult item = row.Item as IndustryResult;
            CommitEdits();
            WantedProducts.Remove(item);
            WantedResultsDataGrid.Items.Refresh();
        }

        private void CalculateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CommitEdits();
            IndustryCalculator ic = new IndustryCalculator(WantedProducts);
            IndustryCalculatorResult result = ic.BuildIndustryTree(IndustryTreeCalculationType.Exact);

            NeededBuildingDataGrid.ItemsSource = result.BuildingList;
            NeededBuildingDataGrid.Items.Refresh();

            NeededOreDataGrid.ItemsSource = result.OreList;
            NeededOreDataGrid.Items.Refresh();

            RecommendedOrePriceText.Text = result.RecommendedMaxOreBuyPrice.ToString();

            ProductionOverview.ItemsSource = result.ItemList;
            ProductionOverview.Items.Refresh();

            MinSellMoneyPerMinText.Text = result.MinSellPriceSum.ToString();
            MinSellMoneyPerHourText.Text = (result.MinSellPriceSum * 60).ToString();
            MinSellMoneyPerDayText.Text = (result.MinSellPriceSum * 60 * 24).ToString();

            MaxSellMoneyPerMinText.Text = result.MaxSellPriceSum.ToString();
            MaxSellMoneyPerHourText.Text = (result.MaxSellPriceSum * 60).ToString();
            MaxSellMoneyPerDayText.Text = (result.MaxSellPriceSum * 60 * 24).ToString();

            Color profitColor;
            double profitMargin = result.MaxSellPriceSum / (result.RecommendedMaxOreBuyPrice * result.OreAmountPerMinNeeded);
            if (profitMargin < 1.0)
            {
                profitColor = Color.FromRgb(255, 0, 0);
            }
            else if (profitMargin < 1.33)
            {
                profitColor = Color.FromRgb(250, 150, 0);
            }
            else if (profitMargin < 1.66)
            {
                profitColor = Color.FromRgb(170, 250, 0);
            }
            else if (profitMargin < 2)
            {
                profitColor = Color.FromRgb(0, 255, 100);
            }
            else
            {
                profitColor = Color.FromRgb(0, 255, 255);
            }
            SolidColorBrush profitAmountForegroundBrush = new SolidColorBrush(profitColor);

            ProfitMargin.Text = profitMargin.ToString();
            ProfitMoneyPerMinText.Text = result.ProfitBuyingOreRecommendedSellingMax.ToString();
            ProfitMoneyPerMinText.Foreground = profitAmountForegroundBrush;
            ProfitMoneyPerHourText.Text = (result.ProfitBuyingOreRecommendedSellingMax * 60).ToString();
            ProfitMoneyPerHourText.Foreground = profitAmountForegroundBrush;
            ProfitMoneyPerDayText.Text = (result.ProfitBuyingOreRecommendedSellingMax * 60 * 24).ToString();
            ProfitMoneyPerDayText.Foreground = profitAmountForegroundBrush;

            IndustryResultDescription.Visibility = Visibility.Visible;
        }
    }
}
