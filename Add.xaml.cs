using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml;
using System.Reflection;

namespace Movie_Database
{

    public partial class Add : Page
    {
        public List<string> 
        yearList = new List<string> {"2023", "2022", "2021", "2020", "2019", "2018", "2017", "2016", "2015", "2014", "2013", "2012", "2011", "2010", "2009", "2008", "2007", "2006", "2005", "2004", "2003" };
        public Add()
        {
            InitializeComponent();
            cmbYear.ItemsSource = yearList;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Home();
            newWindow.Show();
            newWindow.WindowState = WindowState.Maximized;
            Window.GetWindow(this)?.Close();            
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string year;
            if (cmbYear.SelectedIndex != -1)
            {
                year = cmbYear.SelectedValue.ToString();
            }
            else
            {
                year = "2003";
            }
            string genre = txtGenre.Text.Trim();
            string cast = txtCast.Text.Trim();
            string isUpdate = "Add";
            if (name.Equals(string.Empty) || year.Equals(string.Empty) || genre.Equals(string.Empty) || cast.Equals(string.Empty))
            {
                MessageBox.Show("Enter Valid Details");
            }            
            else
            {
                Movies newMovie = new Movies
                {
                    Name = name,
                    Year = year,
                    Genre = genre,
                    Cast = cast,
                };

                Home hm = new Home();
                WriteToXmlFile(newMovie, isUpdate);
                txtName.Text = string.Empty;
                txtGenre.Text = string.Empty;
                txtCast.Text = string.Empty;
                cmbYear.SelectedIndex = -1;
                hm.LoadData();
            }
        }
        public void WriteToXmlFile(Movies movies, string isUpdate, Movies moviesDel=null)
        {
            string file_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Data\MovieDB.xml";
            if (isUpdate == "Add")
            {
                FileStream fs = new FileStream(file_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fs);
                fs.Close();

                XmlElement newMovie = xmlDoc.CreateElement("Movie");
                XmlElement name = xmlDoc.CreateElement("Name");
                name.InnerText = movies.Name;
                newMovie.AppendChild(name);

                XmlElement genre = xmlDoc.CreateElement("Genre");
                genre.InnerText = movies.Genre;
                newMovie.AppendChild(genre);

                XmlElement year = xmlDoc.CreateElement("Year");
                year.InnerText = movies.Year;
                newMovie.AppendChild(year);

                XmlElement cast = xmlDoc.CreateElement("Cast");
                cast.InnerText = movies.Cast;
                newMovie.AppendChild(cast);

                xmlDoc.DocumentElement.InsertAfter(newMovie,
                xmlDoc.DocumentElement.LastChild);

                FileStream fsxml = new FileStream(file_path, FileMode.Truncate,
                                      FileAccess.Write,
                                      FileShare.ReadWrite);

                xmlDoc.Save(fsxml);
                fsxml.Close();
            }
            else if(isUpdate=="Update")
            {
                XDocument doc = XDocument.Load(file_path);

                foreach (var item in doc.Descendants("Movie"))
                {
                    if (item.Element("Name").Value == moviesDel.Name)
                    {
                        item.Element("Name").SetValue(movies.Name);
                        item.Element("Genre").SetValue(movies.Genre);
                        item.Element("Year").SetValue(movies.Year);
                        item.Element("Cast").SetValue(movies.Cast);
                        doc.Save(file_path);
                        break;
                    }
                }
            }
            else if (isUpdate=="Delete")
            {
                XDocument doc = XDocument.Load(file_path);

                foreach (var item in doc.Descendants("Movie"))
                {
                    if (item.Element("Name").Value == moviesDel.Name)
                    {
                        ((XElement)item.Element("Name")).Parent.Remove();
                        doc.Save(file_path);
                        break;
                    }
                }
            }
        }
    }
}
