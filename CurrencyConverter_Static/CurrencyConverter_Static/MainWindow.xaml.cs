using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CurrencyConverter_Static
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindCurreny();
        }

        private void NumberValidationText(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled=regex.IsMatch(e.Text);
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            double ConvertedValue;

            if(txtCurrency.Text==null||txtCurrency.Text.Trim()=="")
            {
                MessageBox.Show("Please enter currency","Information",MessageBoxButton.OK, MessageBoxImage.Information);
                txtCurrency.Focus();
                return;
            }
            else if(cmbFromCurrency.SelectedValue==null||cmbFromCurrency.SelectedIndex==0)
            {
                MessageBox.Show("Please select currency from","Information",MessageBoxButton.OK,MessageBoxImage.Information);
                cmbFromCurrency.Focus();
                return;
            }
            else if(cmbToCurrency.SelectedValue==null || cmbToCurrency.SelectedIndex==0)
            {
                MessageBox.Show("Please select currency to", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbFromCurrency.Focus();
                return;
            }

            if(cmbFromCurrency.Text==cmbToCurrency.Text)
            {
                ConvertedValue = double.Parse(txtCurrency.Text);
                lblCurrency.Content=cmbToCurrency.Text + " " + ConvertedValue.ToString();
            }
            else
            {
                ConvertedValue = (double.Parse(cmbFromCurrency.SelectedValue.ToString()) * double.Parse(txtCurrency.Text)) / double.Parse(cmbToCurrency.SelectedValue.ToString());


                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N2");
            }
        }

        private void ClearControls()
        {
            txtCurrency.Text = String.Empty;
            if (cmbFromCurrency.Items.Count > 0)
            {
                cmbFromCurrency.SelectedIndex = 0;
            }
            if (cmbToCurrency.Items.Count > 0)
            {
                cmbToCurrency.SelectedIndex = 0;
            }
            lblCurrency.Content = "";
            txtCurrency.Focus();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        private void BindCurreny()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");
            dtCurrency.Rows.Add("--SELECT--",0);
            dtCurrency.Rows.Add("USD",32);
            dtCurrency.Rows.Add("EUR",34);
            dtCurrency.Rows.Add("TL",1);
            dtCurrency.Rows.Add("GBP",40);

            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0;

            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath= "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;

        }
        private void txtCurrency_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
