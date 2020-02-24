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
            FillContentAllCarBrend();
        }

        public void FillContentAllCarBrend()
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


                    itemCarBrand.MouseLeftButtonUp += ItemCarBrand_MouseLeftButtonUp;
                    carName.MouseEnter += CarName_MouseEnter;
                    carName.MouseLeave += CarName_MouseLeave;

                    itemCarBrand.Children.Add(carImage);
                    itemCarBrand.Children.Add(carName);
                    gCarBrand.Children.Add(itemCarBrand);
                }
            }


        }

        public void FillContentOneCarBrend(Image image)
        {

            StackPanel brandLogo = new StackPanel();
            StackPanel searchCarByVin = new StackPanel();
            StackPanel searchCarByBody = new StackPanel();
            StackPanel searchCarByParams = new StackPanel();

            Grid.SetRowSpan(brandLogo, 6);
            image.Width = 100;
            image.Height = 100;
            brandLogo.Children.Add(image);
            gCarBrand.Children.Add(brandLogo);

            // 
            searchCarByVin = GetGroupBlock();
            Grid.SetRow(searchCarByVin, 0);
            Grid.SetColumn(searchCarByVin, 1);
            Grid.SetRowSpan(searchCarByVin, 6);

            TextBlock header = new TextBlock() { Width = 500, Height = 200, Background = Brushes.LightGray};
            header.Text = "Поиск автомобиля по VIN";

            gCarBrand.Children.Add(searchCarByVin); 
        }

        private StackPanel GetGroupBlock()
        {
            StackPanel block = new StackPanel();
            Border border = new Border() { BorderThickness = new Thickness(1),
                                           BorderBrush = Brushes.Gray,
                                           CornerRadius = new CornerRadius(3), Name = "border" };

            block.Children.Add(border);

            return block;
        }



        private void CarName_MouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = Brushes.Black;
        }

        private void CarName_MouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = Brushes.Blue;
        }

        private void ItemCarBrand_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            gCarBrand.Children.Clear();
            //MessageBox.Show((string)((TextBlock)sender).Text); 

            var obj = ((StackPanel)sender).Children;
            Image image = (Image)obj[0];
            obj.Clear();
            FillContentOneCarBrend(image);
        }
    }
}
