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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string value1;
        DataClasses1DataContext dc;
        public MainWindow()
        {
            dc = new DataClasses1DataContext();
            InitializeComponent();
            var que = from a in dc.InsertStocks
                      select a;


            mygridview2.ItemsSource = que.ToList();
            var qu = from a in dc.WithdrawItemTables
                     select a;


            mygridview.ItemsSource = qu.ToList();
        }

        public void fetch(string label)
        {
            //use the value here:
            value1 = label.ToString();
            label1.Content = value1;
        }
        private void mygridview_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            deleteRecord.IsEnabled = true;

        }
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
                // Do not close the window  
            }
        }
        private void deleteRecord_Click(object sender, RoutedEventArgs e)
        {
            int row;
            dc = new DataClasses1DataContext();
            WithdrawItemTable ins = new WithdrawItemTable();
            ins = mygridview.SelectedItem as WithdrawItemTable;
            row = Convert.ToInt32(ins.Id);

            if (MessageBox.Show("Do you want to delete data?",
 "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                
            }
            else
            {
                // Do not close the window  
            }
            var selectQuery = from rows in dc.WithdrawItemTables where rows.Id == row select rows;
            foreach (var c in selectQuery)
            {
                dc.WithdrawItemTables.DeleteOnSubmit(c);
                dc.SubmitChanges();
            }
            MessageBox.Show("Data Deleted Successfully!!");
            mygridview.ItemsSource = dc.WithdrawItemTables;

        }
        private void numberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //calling submit button function
        public void InsertStock()
        {
            //check if user input null value
            if (stockId.Text.Length == 0)
            {
                errorid.Text = "Enter stock Id.";
                stockId.Focus();
            }
            else if (itemType.Text.Length == 0)
            {
                errortype.Text = "Enter an item type.";
                itemType.Focus();
            }else if(itemname.Text.Length == 0)
            {
                errortype.Text = "Enter an item name.";
                itemname.Focus();
            }
            else if (itemQuantity.Text.Length == 0)
            {
                errorquantity.Text = "Enter an item type.";
                itemQuantity.Focus();
            }
            else if (shipmentReceived.Text.Length == 0)
            {
                errorshipment.Text = "Enter an item shipment d.";
                shipmentReceived.Focus();
            }
            else if (dealerName.Text.Length == 0)
            {
                errordname.Text = "Enter a dealer name.";
                dealerName.Focus();
            }
            else if (dealerEmail.Text.Length == 0)
            {
                errordemail.Text = "Enter an valid email.";
                dealerEmail.Focus();
            }
            else if (dealerPhoneNo.Text.Length == 0)
            {
                errorphone.Text = "Enter phone number .";
                dealerPhoneNo.Focus();
            }
            // validate null value before insertion of data
            if (stockId.Text.Length != 0 && itemType.Text.Length != 0 && itemname.Text.Length != 0 && shipmentReceived.Text.Length != 0 && dealerEmail.Text.Length != 0 && dealerName.Text.Length != 0 && dealerPhoneNo.Text.Length != 0)
            {
                dc = new DataClasses1DataContext();
                InsertStock add = new InsertStock();
                add.itemNumber = Convert.ToInt32(stockId.Text);
                add.itemType = itemType.Text;
                add.itemName = itemname.Text;
                add.itemQuantity = itemQuantity.Text;
                add.shipmentRecieved = shipmentReceived.Text;
                add.dName = dealerName.Text;
                add.dEmail = dealerEmail.Text;
                add.dPhone = dealerPhoneNo.Text;
                dc.InsertStocks.InsertOnSubmit(add);
                dc.SubmitChanges();
                MessageBox.Show("Data Saved Successfully!!");
                stockId.Text = "";
                itemType.Text = "";
                itemname.Text = "";
                shipmentReceived.Text = "";
                itemQuantity.Text = "";
                dealerName.Text = "";
                dealerEmail.Text = "";
                dealerPhoneNo.Text = "";
            }
        }
        // submit button data insertion
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InsertStock();
            mygridview2.ItemsSource = dc.InsertStocks;
        }
        //when user click row from data-gridview
        private void mygridview_SelectedCellsChanged1(object sender, SelectedCellsChangedEventArgs e)
        {
            deleteRecord1.IsEnabled = true;

        }
        // to delete data of selected row
        private void deleteRecord_Click1(object sender, RoutedEventArgs e)
        {

            int row;
            dc = new DataClasses1DataContext();
            InsertStock ins = new InsertStock();
            ins = mygridview2.SelectedItem as InsertStock;
            row = Convert.ToInt32(ins.Id);

            if (MessageBox.Show("Do you want to delete data?",
"Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
            else
            {
                // Do not close the window  
            }
            var selectQuery = from rows in dc.InsertStocks where rows.Id == row select rows;
            foreach (var c in selectQuery)
            {
                dc.InsertStocks.DeleteOnSubmit(c);
                dc.SubmitChanges();
            }
            MessageBox.Show("Data Deleted Successfully!!");
            mygridview2.ItemsSource = dc.InsertStocks;
        }

        //use of regex for getting numberic value from only.
        private void dealerPhoneNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void stockId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
