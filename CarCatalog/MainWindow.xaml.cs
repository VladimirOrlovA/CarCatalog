using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillContentCarBrend();
        }

        public void FillContentCarBrend()
        {
            int column = gCarBrand.ColumnDefinitions.Count();
            int row = gCarBrand.RowDefinitions.Count();

            string directoryPath = @"C:\Users\Vladimir\source\repos\CarCatalog\CarCatalog\Images";
            string[] pathFileName = Directory.GetFiles(directoryPath);
            string[] fileName = new string[pathFileName.Length];
            int cutPath = directoryPath.Length + 1;

            for (int i = 0; i < fileName.Count(); i++)
            {

                fileName[i] = pathFileName[i].Remove(0, cutPath);
                fileName[i] = fileName[i].Substring(0, fileName[i].IndexOf(".")).ToUpper();
            }

            int countFiles = 0;
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    StackPanel itemCarBrand = new StackPanel()
                    { Orientation = Orientation.Horizontal, Margin = new Thickness(20) };

                    Grid.SetColumn(itemCarBrand, j);
                    Grid.SetRow(itemCarBrand, i);

                    Image carImage = new Image();
                    carImage.Width = 35;
                    carImage.Height = 35;
                    BitmapImage bi = new BitmapImage();

                    TextBlock carName = new TextBlock()
                    {   VerticalAlignment = VerticalAlignment.Center,
                        TextDecorations = TextDecorations.Underline,
                        Margin = new Thickness(10, 0 , 0, 0)
                    };

                    if (countFiles < fileName.Length)
                    {
                        carImage.Source = new BitmapImage(new Uri(pathFileName[countFiles]));
                        carName.Text = fileName[countFiles];
                        countFiles++;
                    }

                    
                    carName.MouseLeftButtonUp += CarName_MouseLeftButtonUp;
                    carName.MouseEnter += CarName_MouseEnter;
                    carName.MouseLeave += CarName_MouseLeave;

                    itemCarBrand.Children.Add(carImage);
                    itemCarBrand.Children.Add(carName);
                    gCarBrand.Children.Add(itemCarBrand);
                }
            }


        }

        private void CarName_MouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = Brushes.Black;
        }

        private void CarName_MouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = Brushes.Blue;
        }

        private void CarName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            MessageBox.Show((string)((TextBlock)sender).Text);
        }
    }
}
