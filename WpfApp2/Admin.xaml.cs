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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        string value1;
        int row1;

        public Window3(int row1)
        {
            this.row1 = row1;
        }

        DataClasses1DataContext dc;
        public Window3()
        {
            InitializeComponent();
            dc = new DataClasses1DataContext();
            //for SHowing withraw data
            WithdrawItemTable wt = new WithdrawItemTable();
            var qu = from a in dc.WithdrawItemTables
                     select a;

            mygridview3.ItemsSource = qu.ToList();
            myadmingrid2.ItemsSource = dc.logins;

            combobox.Items.Add("Manager");
            combobox.Items.Add("Employee");
            // For Showing Stocks
            var que = from a in dc.InsertStocks
                      select a;

            mygridview1.ItemsSource = que.ToList();
        }
        //to get data which passed by user
        public void fetch(string label)
        {
            //use the value here:
            value1 = label.ToString();
            label1.Content = value1;

        }

        //when user click on row it will enable and give functionality to delete button
        private void mygridview_SelectedCellsChanged1(object sender, SelectedCellsChangedEventArgs e)
        {
            deleteRecord1.IsEnabled = true;
        }
        private void mygridview_SelectedCellsChanged3(object sender, SelectedCellsChangedEventArgs e)
        {
            deleteRecord3.IsEnabled = true;

        }

        //to delete selected data from user
        private void deleteRecord_Click3(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete data?",
"Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int row;
                dc = new DataClasses1DataContext();
                WithdrawItemTable ins = new WithdrawItemTable();
                ins = mygridview3.SelectedItem as WithdrawItemTable;
                if (ins == null)
                {
                    MessageBox.Show("Hii i am 3");
                }
                else
                {
                    row = Convert.ToInt32(ins.Id);

                    MessageBox.Show(row.ToString());
                    var selectQuery = from rows in dc.WithdrawItemTables where rows.Id == row select rows;
                    foreach (var c in selectQuery)
                    {
                        dc.WithdrawItemTables.DeleteOnSubmit(c);
                        dc.SubmitChanges();
                    }
                    MessageBox.Show("Data Deleted Successfully!!");
                    mygridview3.ItemsSource = dc.WithdrawItemTables;
                }
            }
            else
            {
                // Do not close the window  
            }
           
        }

        //when user try to enter data it will first verify and then allow user to insert data into database
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (username.Text.Length == 0)
            {
                errorusername.Text = "Enter username.";
                username.Focus();
            }
              else if (password.Password.Length == 0)
            {
                errorpassword.Text = "Enter a password.";
                password.Focus();
            }else //to validate email
            if (!Regex.IsMatch(email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                erroremail.Text = "Enter a valid email.";
                email.Select(0, email.Text.Length);
                email.Focus();
            }else
          
            if (username.Text.Length != 0 && password.Password.Length != 0 && email.Text.Length != 0)
            {
                dc = new DataClasses1DataContext();
                login lg = new login();
                lg.type = combobox.SelectedItem.ToString();
                lg.username = username.Text;
                lg.password = password.Password;
                lg.email = email.Text;
                dc.logins.InsertOnSubmit(lg);
                dc.SubmitChanges();
                MessageBox.Show("Data Saved Successfully!!");

                // lg.Id = Convert.ToInt32("");
                combobox.SelectedItem = "";
                username.Text = "";
                password.Password = "";
                email.Text = "";
                errorusername.Text = "";
                errorpassword.Text = "";
                erroremail.Text = "";

                myadmingrid2.ItemsSource = dc.logins;
            }
        }

        //this will populate data into user input so one can change data like update and delete 
        private void myadmingrid_SelectedCellsChanged2(object sender, SelectedCellsChangedEventArgs e)
        {
            delete2.IsEnabled = true;
            update.IsEnabled = true;
            int row;
            DataClasses1DataContext dt = new DataClasses1DataContext();
            login tb = new login();



            tb = myadmingrid2.SelectedItem as login;

            if (tb == null)
            {
                //  MessageBox.Show("Hii");
            }
            else
            {
                row = Convert.ToInt32(tb.Id);
                // MessageBox.Show(row.ToString());

                string row1 = Convert.ToString(tb.Id);


                var selectQuery = from rows in dt.logins where rows.Id == row select rows;

                foreach (var c in selectQuery)
                {
                    combobox.Text = c.type;
                    username.Text = c.username;
                    password.Password = c.password;
                    email.Text = c.email;


                }

                dt.SubmitChanges();
            }
        }


        private void calldelete()
        {

            int row;
            dc = new DataClasses1DataContext();
            login lg = new login();
            lg = myadmingrid2.SelectedItem as login;

            if (MessageBox.Show("Do you want to delete data?",
"Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (lg == null)
                {
                    //  MessageBox.Show("Hii");
                }
                else
                {
                    row = Convert.ToInt32(lg.Id);
                    var selectQuery = from rows in dc.logins where rows.Id == row select rows;
                    foreach (var c in selectQuery)
                    {
                        dc.logins.DeleteOnSubmit(c);
                        dc.SubmitChanges();
                    }
                    MessageBox.Show("Data Deleted Successfully!!");
                    delete2.IsEnabled = false;
                    myadmingrid2.ItemsSource = dc.logins;
                }
            }
            else
            {
                // Do not close the window  
            }
            //MessageBox.Show(row.ToString());
          
        }

        private void delete_Click2(object sender, RoutedEventArgs e)
        {
            calldelete();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            callupdate();
        }

        //to update data 
        private void callupdate()
        {
            DataClasses1DataContext dt = new DataClasses1DataContext();
            login tb = new login();
            tb = myadmingrid2.SelectedItem as login;
            int row = Convert.ToInt32(tb.Id);
            var selectQuery = from rows in dt.logins where rows.Id == row select rows;
            foreach (var c in selectQuery)
            {
                c.type = combobox.Text;
                c.username = username.Text;
                c.password = password.Password;
                c.email = email.Text;



                myadmingrid2.ItemsSource = dt.logins;
            }
            // loadgrid();
            dt.SubmitChanges();
            MessageBox.Show("Updated Data Successfully!!");
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

        private void deleteRecord_Click1(object sender, RoutedEventArgs e)
        {

            dc = new DataClasses1DataContext();
            InsertStock lg = new InsertStock();
            lg = mygridview1.SelectedItem as InsertStock;

            row1 = Convert.ToInt32(lg.Id);

            if (MessageBox.Show("Do you want to delete data?",
"Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectQuery = from rows in dc.InsertStocks where rows.Id == row1 select rows;
                foreach (var c in selectQuery)
                {
                    dc.InsertStocks.DeleteOnSubmit(c);
                    dc.SubmitChanges();
                }
                MessageBox.Show("Data Deleted Successfully!!");
                delete2.IsEnabled = false;
                mygridview1.ItemsSource = dc.InsertStocks;
                myadmingrid2.ItemsSource = dc.logins;

            }
            else
            {
                // Do not close the window  
            }
           
        }
    }
}



