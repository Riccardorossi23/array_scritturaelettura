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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;


namespace array_scritturaelettura
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Max_Array_Size = 10000;
        private const int Max_Array_Value = 10000;
        private const string File_Name = "Array.txt";
        private readonly Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            Loadarray();
        }
        private void txtDimensioni_TextChanged(Object sender, TextChangedEventArgs e)
        {
            string text = txtDimensione.Text;
            try
            {
                int n = int.Parse(text);
                if (n <= 0 || n > Max_Array_Size)
                    throw new Exception();
                txtDimensione.Foreground = Brushes.Black;
                txtStatus.Text = "";
            }
            catch
            {
                txtDimensione.Foreground = Brushes.Red;
                txtStatus.Text = "Numero non valido";
            }
        }
        private void btnGenera_Click(object sender, RoutedEventArgs e)
        {
            int dim = int.Parse(txtDimensione.Text);
            int[] array = new int[dim];
            for (int i = 0; i < dim; i++)
                array[i] = random.Next(1, Max_Array_Value);
            ShowArray(array);
            SaveArray(array);
        }

        private void ShowArray(int[] array)
        {
            string s = "[";
            for (int i = 0; i < array.Length; i++)
            {
                s += array[i];
                if (i < array.Length - 1)
                    s += ",";
            }
            s += "]";
            txtOutput.Text = s;
        }
        private void Loadarray()
        {
            if (File.Exists(File_Name))
                try
                {
                    using (StreamReader r = new StreamReader(File_Name, Encoding.UTF8))
                    {
                        int dim = int.Parse(r.ReadLine());
                        int[] array = new int[dim];
                        string line;
                        int i = 0;
                        while ((line = r.ReadLine()) != null)
                            array[i++] = int.Parse(line);
                        ShowArray(array);
                        txtStatus.Text = $"Caricato array di{dim} elementi da file";

                    }

                }
                catch { }

        }
        private void SaveArray(int[] array)
        {

            try
            {
                using (StreamWriter w = new StreamWriter(File_Name, false, Encoding.UTF8))
                {
                    w.WriteLine(array.Length);
                    for (int i = 0; i < array.Length; i++)
                        w.WriteLine(array[i]);
                    w.Flush();


                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(File_Name))
                File.Delete(File_Name);
            txtDimensione.Text = "";
            txtStatus.Text = "";
               txtOutput.Text="";

        }
    }
}
            

        

        
       
        
           