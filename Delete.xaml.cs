using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Movie_Database
{    
    public partial class Delete : Page
    {
        Movies moviesDel;
        ObservableCollection<Movies> selectedList = new ObservableCollection<Movies>();
        public Delete()
        {
            InitializeComponent();
            Home hm = new Home();
            lbdelMovies.ItemsSource = hm.LoadData();
            btn_delete.IsEnabled = false;
        }
        private void lbdelMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedList.Clear();
            if (this.lbdelMovies.SelectedItem is Movies)
            {
                selectedList.Add(((Movies)this.lbdelMovies.SelectedItem));
                moviesDel = (Movies)this.lbdelMovies.SelectedItem;
            }            
            btn_delete.IsEnabled = true;
        }
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Home();
            newWindow.Show();
            newWindow.WindowState = WindowState.Maximized;
            Window.GetWindow(this)?.Close();
        }
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (moviesDel != null)
            {
                if (System.Windows.MessageBox.Show("Do you Really Want To Delete This Movie ?", "Delete Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string isUpdate = "Delete";
                    Add add = new Add();
                    add.WriteToXmlFile(moviesDel, isUpdate, moviesDel);
                    Home hm = new Home();
                    hm.LoadData();
                    moviesDel = null;
                    lbdelMovies.SelectedIndex = -1;
                    btn_delete.IsEnabled = false;
                    lbdelMovies.ItemsSource = hm.LoadData();
                }
                else
                {
                    moviesDel = null;
                    lbdelMovies.SelectedIndex = -1;
                    btn_delete.IsEnabled = false;
                }
            }
        }
    }
}
