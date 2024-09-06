using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System;
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Part> PartList = new List<Part>();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void betoltesBTN_Click(object sender, RoutedEventArgs e)
        {
            string filename = "";

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".bsx";
            dlg.Filter = "BSX Files (*.bsx)|*.bsx";
            if (dlg.ShowDialog() == true)
            {
                // Open document 
                filename = dlg.FileName;
            }

            if (filename.Length > 0)
            {
                XDocument xaml = XDocument.Load(filename);
                foreach (var elem in xaml.Descendants("Item"))
                {
                    PartList.Add(new Part(elem.Element("ItemID").Value, elem.Element("ItemName").Value, elem.Element("CategoryName").Value, elem.Element("ColorName").Value, Convert.ToInt32(elem.Element("Qty").Value)));
                }

                mainDG.ItemsSource = PartList;
            }
            else
            {
                MessageBox.Show("Hiba a fájl kiválasztása közben!");
            }

            
            
        }

        private void itemIDTXTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            mainDG.ItemsSource = PartList.Where(x=> x.ItemID.StartsWith(itemIDTXTB.Text) && x.ItemName.StartsWith(itemNameTXTB.Text) && x.CategoryName.StartsWith(itemCategoryTXTB.Text));
        }
    }
}