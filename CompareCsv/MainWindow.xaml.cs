using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace CompareCsv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModelView modelView = new ModelView();

        public MainWindow()
        {
            DataContext = modelView;

  
            InitializeComponent();

            textBoxFile1.Text =
            modelView.File1 = @"C:\Users\u7ashtuk\Desktop\ITK Test Demo Server\Test1\Tunnel A\Working 2\";
            textBoxFile2.Text =
            modelView.File2 = @"C:\Users\u7ashtuk\Desktop\ITK Test Demo Server\Test1\Tunnel A\Working 9\";

            textBoxSortOn.Text =
            modelView.SortOptions = " Field of View, Location Description, Defect Type/Property";
        }

        private void textBoxFile1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!File.Exists(textBoxFile1.Text))
            {
                textBoxFile1.Background = System.Windows.Media.Brushes.Red;
            }
            else
            {
                textBoxFile1.Background = System.Windows.Media.Brushes.White;
            }
        }

        private void textBoxFile2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!File.Exists(textBoxFile2.Text))
            {
                textBoxFile2.Background = System.Windows.Media.Brushes.Red;
            }
            else
            {
                textBoxFile2.Background = System.Windows.Media.Brushes.White;
            }
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(modelView.File1) && File.Exists(modelView.File2))
            {
                textBlockStatus.Foreground = System.Windows.Media.Brushes.White;

                if (modelView.SortFiles())
                {
                    if (!modelView.Merge())
                    {
                        textBlockStatus.Foreground = System.Windows.Media.Brushes.Red;
                    }
                }
            }
            else
            {
                modelView.Status = "Files Do not exist";
                textBlockStatus.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
    }
}
