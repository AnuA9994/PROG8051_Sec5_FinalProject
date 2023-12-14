using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace Movie_Database
{
    public partial class Home : Window
    {
        List<Movies> movieList;
        public Home()
        {
            InitializeComponent();
            LoadData();
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update u = new Update();
            NavigationWindow navigationWindow = new NavigationWindow();
            navigationWindow.Content = u;
            navigationWindow.WindowState = WindowState.Maximized;
            navigationWindow.Show();
            Close();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Add a = new Add();
            NavigationWindow navigationWindow = new NavigationWindow();
            navigationWindow.Content = a;
            navigationWindow.WindowState = WindowState.Maximized;
            navigationWindow.Show();
            Close();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete d = new Delete();
            NavigationWindow navigationWindow = new NavigationWindow();
            navigationWindow.Content = d;
            navigationWindow.WindowState = WindowState.Maximized;
            navigationWindow.Show();
            Close();
        }
        public List<Movies> LoadData()
        {
            try
            {
                string file_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Data\MovieDB.xml";
                XDocument doc = XDocument.Load(file_path);
                movieList = doc.Descendants("Movie")
                               .OrderBy(movie => movie.Element("Name").Value)
                               .Select(movie => new Movies
                               {
                                   Name = movie.Element("Name").Value,
                                   Genre = movie.Element("Genre").Value,
                                   Year = movie.Element("Year").Value,
                                   Cast = movie.Element("Cast").Value,
                               })
                               .ToList();
                ViewDataGrid.ItemsSource = movieList;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"File not found: {ex.FileName}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return movieList;
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window login = new Login();
            login.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            login.Show();
        }
    }
}
