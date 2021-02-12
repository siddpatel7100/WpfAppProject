using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        DataClasses1DataContext dc = new DataClasses1DataContext();




        public Window1()
        {
            InitializeComponent();

            //Assign value to drop-down list
            firstname.Items.Add("Admin");
            firstname.Items.Add("Manager");
            firstname.Items.Add("Employee");


        }

        // Null for checking purpose
        private void firstname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        // user login button
        private void first_Click(object sender, RoutedEventArgs e)
        {
            //validate data from user-input
            if (username.Text.Length == 0 )
            {
                errormessage.Text = "Enter an username.";
                username.Focus();
            }
            else if (pd.Password.Length == 0)
            {
                errormessage1.Text = "Enter an password.";
                pd.Focus();
            }
            //   MessageBox.Show(password.ToString());
            dc = new DataClasses1DataContext();
            Table tb = new Table();


            //fetching value from database
            var q1 = from a in dc.logins where a.type == firstname.Text && a.username == username.Text && a.password == pd.Password.ToString() select a;

            foreach (var b in q1)
            {

                if (b.type == "Admin" && b.username == username.Text && b.password == pd.Password.ToString())
                {
                    MessageBox.Show("Welcome " + username.Text);
                    string value = username.Text;
                    Window3 w2 = new Window3();
                    w2.fetch(value.ToString());
                    w2.Show();
                    this.Close();

                }
                else if (b.type == "Manager" && b.username == username.Text && b.password == pd.Password.ToString())
                {
                    //MessageBox.Show("Welcome Manager");
                    MessageBox.Show("Welcome " + username.Text);
                    string value = username.Text;
                    MainWindow w2 = new MainWindow();
                    w2.fetch(value.ToString());
                    w2.Show();
                    this.Close();
                }
                else if (b.type == "Employee" && b.username == username.Text && b.password == pd.Password.ToString())
                {
                    MessageBox.Show("Welcome " + username.Text);
                    string value = username.Text;
                    Window2 w2 = new Window2();
                    w2.fetch(value.ToString());
                    w2.Show();
                    this.Close();
                }
            }

        }
    }

}





