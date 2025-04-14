using Popitka2.DB;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Popitka2.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageList.xaml
    /// </summary>
    public partial class PageList : Page
    {
        List<string>listFilter = new List<string>() { "По возрастанию", "По убыванию"};
        public List<Product> products { get; set; }
        public List<ProductType> productTypes { get; set; }
        public PageList()
        {
            InitializeComponent();
            this.DataContext = this;
            listProd.ItemsSource = ClassDB.connection.Product.ToList();
            //атрибуты для фильтрации по цене
            cmbFilter.ItemsSource = listFilter;
            this.DataContext = this;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearch.Text != null)
            {
                listProd.ItemsSource = ClassDB.connection.Product.Where(z => z.Title.Contains(txbSearch.Text)).ToList();
            }
        }

        //проблемы с объектами комбобокса
        private void SelChSortObjects(object sender, SelectionChangedEventArgs e)
        {
            //сортировка по наименованию
            if (txbSearch.Text != "")
            {
                products = products.Where(p => p.Title.ToLower().StartsWith(txbSearch.Text.ToLower().Trim())).ToList();
            }
            //сортировка по типу товара
            if (cmbSort.SelectedItem != null)
            {
                var typeProduct = cmbSort.SelectedItem as ProductType;
                products = products.Where(i => i.ProductTypeID == typeProduct.ID).ToList();
            }
        }

        //не работает
        private void SelChFolterObjects(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFilter.SelectedItem != null)
            {
                if (cmbFilter.SelectedIndex == 0)
                {
                    products.Sort((p1, p2) => p1.MinCostForAgent.CompareTo(p2.MinCostForAgent));
                }
                else if (cmbFilter.SelectedIndex == 1)
                {
                    products.Sort((p1, p2) => p2.MinCostForAgent.CompareTo(p1.MinCostForAgent));
                }
                else
                {
                    products = new List<Product>(ClassDB.connection.Product);
                }
            }
        }

        private void ClEventEditElement(object sender, RoutedEventArgs e)
        {

        }

        private void ClEventDeleteElement(object sender, RoutedEventArgs e)
        {

        }
    }
}
