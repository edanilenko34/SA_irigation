using SA.Irrigation.Desktop.RestApiClient;
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
    /// Interaction logic for frmSchedule.xaml
    /// </summary>
    public partial class frmSchedule : Window
    {
        private readonly ISAIrrigationAPI _client;
        private CollectionView _sensors;

        public CollectionView Sensors {  get { return _sensors; } }
        public ScheduleViewModel?  ScheduleData { get; set; }
        public frmSchedule(ISAIrrigationAPI client)
        {
            ArgumentNullException.ThrowIfNull(client, nameof(client));
            _client = client;
            _sensors = new CollectionView(_client.GetDevicesByTypeAsync(DeviceType.Sensor).Result.ToList());
            InitializeComponent();
        }

        private void btnCronEditor_Click(object sender, RoutedEventArgs e)
        {
            var frm = new frmCronEditor(tbCRON.Text);
            frm.Owner = this;
            if (frm.ShowDialog()== true)
            {
                tbCRON.Text = frm.CrontabString;
            }
        }

        private void btnCronEditorFinish_Click(object sender, RoutedEventArgs e)
        {
            var frm = new frmCronEditor(tbFinishCron.Text);
            frm.Owner = this;
            if (frm.ShowDialog() == true)
            {
                tbFinishCron.Text = frm.CrontabString;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbSensors.ItemsSource = Sensors;
            cbSensors.DisplayMemberPath = "Name";
            cbSensors.SelectedValuePath = "Id";
            if (ScheduleData!=null)
            {
                tbName.Text = ScheduleData.Name;
                tbDescription.Text = ScheduleData.Description;
                tbCRON.Text = ScheduleData.StartCron;
                
                if (ScheduleData.FinishBy == FinishByType.ByTime)
                {
                    tbFinishCron.Text = ScheduleData.FinishCron;
                    cbFinishBy.SelectedIndex = 0;
                }
                else
                {
                    tbValue.Text = ScheduleData.FinishValue.ToString();
                    cbFinishBy.SelectedIndex = 1;
                    cbSensors.SelectedItem = cbSensors.Items.OfType<DeviceDto>().FirstOrDefault(f => f.Id == ScheduleData.FinishDeviceId);
                }
                if (ScheduleData.StartDate != null)
                {
                    dpStartDate.DisplayDate = ScheduleData.StartDate.Value;
                    dpStartDate.SelectedDate = ScheduleData.StartDate.Value;

                }
                if (ScheduleData.FinishDate != null)
                {
                    dpEndDate.DisplayDate = ScheduleData.FinishDate.Value;
                    dpEndDate.SelectedDate = ScheduleData.FinishDate.Value;
                }
                cbxDisabled.IsChecked = ScheduleData.IsDisabled;
            }
            else
            {
                cbSensors.SelectedIndex = 0;
                cbFinishBy.SelectedIndex = 0;
                ScheduleData = new ScheduleViewModel();
            }
        }

        private void cbFinishBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFinishBy.SelectedIndex == 0)
            {
                cbSensors.IsEnabled = false;
                tbFinishCron.IsEnabled = true;
                tbValue.IsEnabled = false;
            }
            else
            {
                cbSensors.IsEnabled = true;
                tbFinishCron.IsEnabled = false;
                tbValue.IsEnabled = true;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (ScheduleData==null) ScheduleData = new ScheduleViewModel();
            ScheduleData.Name = tbName.Text;
            ScheduleData.Description = tbDescription.Text;
            ScheduleData.StartCron = tbCRON.Text;
            if (cbFinishBy.SelectedIndex == 0)
            {
                ScheduleData.FinishBy = FinishByType.ByTime;
                ScheduleData.FinishCron = tbFinishCron.Text;
            }
            else
            {
                ScheduleData.FinishBy = FinishByType.ByValue;
                ScheduleData.FinishValue = Double.Parse(tbValue.Text);
                ScheduleData.FinishDeviceId = (Guid)cbSensors.SelectedValue;
            }
            if (dpEndDate.SelectedDate!=null) ScheduleData.FinishDate = dpEndDate.SelectedDate;
            if (dpStartDate.SelectedDate!=null) ScheduleData.StartDate = dpStartDate.SelectedDate;
            ScheduleData.IsDisabled = cbxDisabled.IsChecked == null ? false : cbxDisabled.IsChecked.Value;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
