using SA.Irrigation.Desktop.ViewModels;
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
    /// Interaction logic for frmModel.xaml
    /// </summary>
    public partial class frmModel : Window
    {

        public ModelViewModel? ModelData { get; set; }
        public frmModel()
        {
            InitializeComponent();

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (ModelData == null) ModelData = new ModelViewModel();
            ModelData.Name = tbName.Text;
            ModelData.Description = tbDescription.Text;
            if (cbModelType.SelectedIndex == 0)
            {
                ModelData.ModelDeviceType = RestApiClient.DeviceType.Valve;
                ModelData.CloseCommandText = tbCloseCommand.Text;
                ModelData.OpenCommandText = tbOpenCommand.Text;
                ModelData.GetDataCommandText = null;
            }
            else
            {
                ModelData.ModelDeviceType = RestApiClient.DeviceType.Sensor;
                ModelData.CloseCommandText = null;
                ModelData.OpenCommandText = null;
                ModelData.GetDataCommandText = tbGetDataCommand.Text;
            }
            
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void cbModelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbModelType.SelectedIndex == 0)
            {
                tbGetDataCommand.IsEnabled = false;
                tbOpenCommand.IsEnabled = true;
                tbCloseCommand.IsEnabled = true;
            }
            if (cbModelType.SelectedIndex == 1)
            {
                tbGetDataCommand.IsEnabled = true;
                tbOpenCommand.IsEnabled = false;
                tbCloseCommand.IsEnabled = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ModelData != null)
            {
                tbName.Text = ModelData.Name;
                tbDescription.Text = ModelData.Description;
                tbCloseCommand.Text = ModelData.CloseCommandText;
                tbOpenCommand.Text = ModelData.OpenCommandText;
                tbGetDataCommand.Text = ModelData.GetDataCommandText;
                cbModelType.SelectedIndex = ModelData.ModelDeviceType == RestApiClient.DeviceType.Valve ? 0 : 1;
            }
            else
                cbModelType.SelectedIndex = 0;
        }
    }
}
