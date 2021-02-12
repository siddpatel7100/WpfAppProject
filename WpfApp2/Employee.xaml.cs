using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        string value2;
        double itemNum;
        double total;


        //to get current time and date
        DateTime dt = DateTime.Now;
        DataClasses1DataContext dc;
        public Window2()
        {
            dc = new DataClasses1DataContext();
            InitializeComponent();
            detailupdate();
            dc = new DataClasses1DataContext();

            //getting data from table
            var get = from a in dc.InsertStocks select a.itemType;
            fetchType.ItemsSource = get.Distinct();


            //  Windows2_Load();
        }

        //function for updation of data
        private void detailupdate()
        {
            dc = new DataClasses1DataContext();
            var que = from a in dc.InsertStocks
                      select a;
                    
            mygridview1.ItemsSource = que.ToList();
            this.datetime.Content = dt.ToString();
        }

        //user double click to log-out from window
        private void label1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Do you want to Log-Out?",
"Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Window1 w1 = new Window1();
                w1.Show();
                this.Close();
            }
            else
            {
                //  not close the window  
            }
        }

        //to set and get value from login 
        public void fetch(string label)
        {
            //use the value here:
            value2 = label.ToString();
            label1.Content = value2; ;
            updateStockTable();
        }

        public void updateStockTable()
        {
            dc = new DataClasses1DataContext();
            var qu = from a in dc.WithdrawItemTables
                     where a.empName == value2
                     select new
                     {
                         a.Id,
                         a.itemType,
                         a.itemName,
                         a.oldQuantity,
                         a.updateQuantity,
                         a.quantityWithdraw,
                         a.empName,
                         a.datetime
                     };
            mygridview.ItemsSource = qu.ToList();

        }

        // to get data from drop-down list
        private void fetchType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fetchType.SelectedItem == null)
            {

            }
            else
            {
                fetchName.IsEnabled = true;
                dc = new DataClasses1DataContext();
                var query2 = from a in dc.InsertStocks where a.itemType == fetchType.SelectedItem.ToString() select a.itemName;
                fetchName.ItemsSource = query2.Distinct();
            }
        }
        //to validate integer number
        private void numberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //to get item name from database
        private void fetchName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fetchName.SelectedItem == null)
            {

            }
            else
            {
                dc = new DataClasses1DataContext();
                var que = from a in dc.InsertStocks where a.itemName == fetchName.SelectedItem.ToString() select a.itemQuantity;
                foreach (var v in que)
                {
                    quantity.Content = v;
                    itemNum = Convert.ToDouble(quantity.Content);
                }
                noitem.IsEnabled = true;
            }
            // MessageBox.Show(itemNum.ToString());

        }

        //this will sub data and insert into database when user try to withdraw data
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (noitem.Text.Length != 0 )
            {
                total = itemNum - Convert.ToDouble(noitem.Text);
                // MessageBox.Show(total.ToString());
                dc = new DataClasses1DataContext();
                WithdrawItemTable wit = new WithdrawItemTable();
                wit.itemType = fetchType.Text;
                wit.itemName = fetchName.Text;
                wit.oldQuantity = itemNum.ToString();
                wit.quantityWithdraw = noitem.Text;
                wit.updateQuantity = total.ToString();
                wit.empName = value2;
                wit.datetime = dt;
                dc.WithdrawItemTables.InsertOnSubmit(wit);
                dc.SubmitChanges();
                updateInsertStock();
                MessageBox.Show("Withdraw Done Successfully!!");
            }

        }

        private void updateInsertStock()
        {

            DataClasses1DataContext dt = new DataClasses1DataContext();
            // InsertStock tb = new InsertStock();

            var selectQuery = from rows in dt.InsertStocks where rows.itemType == fetchType.SelectedItem.ToString() && rows.itemName == fetchName.SelectedItem.ToString() && rows.itemQuantity == quantity.Content.ToString() select rows;
            foreach (var c in selectQuery)
            {
                c.itemQuantity = total.ToString();


                dt.SubmitChanges();

            }

            fetchType.Text = "";
            fetchName.Text = "";
            quantity.Content = "";
            noitem.Text = "";
            detailupdate();
            updateStockTable();
        }

        private void noitem_TextChanged(object sender, TextChangedEventArgs e)
        {
            withdraw.IsEnabled = true;
        }
    }
}
