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
using System.Windows.Shapes;

namespace SA.Irrigation.Desktop.Forms
{
    /// <summary>
    /// Interaction logic for frmCronEditor.xaml
    /// </summary>
    public partial class frmCronEditor : Window
    {
        public string CrontabString
        {
            get { return (string)GetValue(CrontabStringProperty); }
            set { SetValue(CrontabStringProperty, value); }
        }


        public static readonly DependencyProperty CrontabStringProperty = DependencyProperty.Register("CrontabString", typeof(string), typeof(frmCronEditor), new PropertyMetadata(""));
        public frmCronEditor(string cron)
        {
            InitializeComponent();
            CrontabString = cron;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
