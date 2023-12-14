using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Movie_Database
{
    public partial class Update : Page
    {        
        Movies moviesDel;        
        ObservableCollection<Movies> selectedList = new ObservableCollection<Movies>();
        public Update()
        {
            InitializeComponent();
            btn_update.IsEnabled = false;
            Add ad = new Add();
            cmbYearUpdate.ItemsSource = ad.yearList;
            Home hm = new Home();            
            lbMovies.ItemsSource = hm.LoadData();
        }
        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            if (lbMovies.SelectedIndex != -1)
            {
                string name = txtNameUpdate.Text;
                string year = cmbYearUpdate.SelectedValue.ToString();
                string genre = txtGenreUpdate.Text;
                string cast = txtCastUpdate.Text;

                string isUpdate = "Update";
                if (name.Equals(string.Empty) || year.Equals(string.Empty) || genre.Equals(string.Empty) || cast.Equals(string.Empty))
                {
                    MessageBox.Show("Enter Valid Details");
                }
                else
                {
                    Movies updateMovie = new Movies
                    {
                        Name = name,
                        Year = year,
                        Genre = genre,
                        Cast = cast,
                    };

                    Add add = new Add();
                    add.WriteToXmlFile(updateMovie, isUpdate, moviesDel);
                    lbMovies.SelectedIndex = -1;
                    btn_update.IsEnabled = false;
                    //btnDelete.IsEnabled = false;
                    Home hm = new Home();
                    lbMovies.ItemsSource = hm.LoadData();
                }
            }
        }
        private void lbMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedList.Clear();
            if (this.lbMovies.SelectedItem is Movies)
            {
                selectedList.Add(((Movies)this.lbMovies.SelectedItem));
                moviesDel = (Movies)this.lbMovies.SelectedItem;
            }
            foreach (var item in selectedList)
            {
                txtNameUpdate.Text = item.Name;
                cmbYearUpdate.SelectedValue = item.Year;
                txtGenreUpdate.Text = item.Genre;
                txtCastUpdate.Text = item.Cast;
            }
            btn_update.IsEnabled = true;
        }
        private readonly Home hom;
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {

            var newWindow = new Home();
            newWindow.Show();
            newWindow.WindowState = WindowState.Maximized;
            Window.GetWindow(this)?.Close();
        }
    }
}
