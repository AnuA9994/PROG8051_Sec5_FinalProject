using System.Windows;

namespace Movie_Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string uname = txt_uname.Text;
            string password = txt_pswd.Password;
            if (uname.ToLower() == "admin" && password.ToLower() == "admin")
            {
                this.Hide();
                Window home = new Home();
                home.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                home.WindowState = WindowState.Maximized;
                home.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password!");
            }
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
