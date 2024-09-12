using SA.Irrigation.Desktop.Forms;
using SA.Irrigation.Desktop.RestApiClient;
using System.Windows;

namespace SA.Irrigation.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISAIrrigationAPI _client;
        private List<DeviceModelDto> _models;
        private List<DeviceDto> _devices;
        private List<ScheduleDto> _schedules;

        public MainWindow(ISAIrrigationAPI client)
        {
            _client = client;
            InitializeComponent();
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _models = _client.GetAllModelsAsync().Result.ToList();
            _devices = _client.GetAllDevicesAsync().Result.ToList();
            _schedules = new List<ScheduleDto>();
            dgModels.ItemsSource = _models;
            dgDevices.ItemsSource = _devices;
            dgSchedules.ItemsSource = _schedules;
        }

        private void btnAddDeviceModel_Click(object sender, RoutedEventArgs e)
        {
            var frm = new frmModel();
            frm.Owner = this;
            if (frm.ShowDialog()!.Value)
            {
                if (frm.ModelData != null)
                {
                    var model = new CreateDeviceModelRequest();
                    model.Description = frm.ModelData.Description;
                    model.GetDataCommand = frm.ModelData.GetDataCommandText;
                    model.Name = frm.ModelData.Name;
                    model.Type = frm.ModelData.ModelDeviceType!.Value;
                    model.CloseCommand = frm.ModelData.CloseCommandText;
                    model.OpenCommand = frm.ModelData.OpenCommandText;
                    var res = _client.CreateModelAsync(model).Result;
                    if (res != null)
                    {
                        _models.Add(res);
                        dgModels.Items.Refresh();
                    }

                }
            }
        }

        private void btnUpdateDeviceModel_Click(object sender, RoutedEventArgs e)
        {
            if (dgModels.SelectedItem != null)
            {
                var itemToEdit = (DeviceModelDto)dgModels.SelectedItem;
                var frm = new frmModel();
                frm.Owner = this;
                frm.ModelData = new ViewModels.ModelViewModel()
                {
                    Id = itemToEdit.Id,
                    Name = itemToEdit.Name,
                    Description = itemToEdit.Description,
                    ModelDeviceType = itemToEdit.Type,
                    CloseCommandText = itemToEdit.CloseCommand,
                    OpenCommandText = itemToEdit.OpenCommand,
                    GetDataCommandText = itemToEdit.GetDataCommand
                };
                if (frm.ShowDialog() == true)
                {
                    var request = new CreateDeviceModelRequest()
                    {
                        CloseCommand = frm.ModelData.CloseCommandText,
                        OpenCommand = frm.ModelData.OpenCommandText,
                        GetDataCommand = frm.ModelData.GetDataCommandText,
                        Type = frm.ModelData.ModelDeviceType.Value,
                        Name = frm.ModelData.Name,
                        Description = frm.ModelData.Description
                    };

                    var res = _client.ModelUpdateAsync(frm.ModelData.Id.Value, request).Result;
                    var modItem = _models.FirstOrDefault(i => i.Id == itemToEdit.Id);
                    _models.Remove(modItem);
                    _models.Add(res);
                    dgModels.Items.Refresh();
                }
            }
        }

        private void btnDeleteDeviceModel_Click(object sender, RoutedEventArgs e)
        {
            if (dgModels.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure to delete device model?", "", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                {
                    var itemToDelete = _models.FirstOrDefault(i => i.Id == ((DeviceModelDto)dgModels.SelectedItem).Id);
                    _client.DeleteModelAsync(itemToDelete!.Id).RunSynchronously();
                    _models.Remove(itemToDelete);
                    dgModels.Items.Refresh();
                }
            }
        }

        private void btnAddDevice_Click(object sender, RoutedEventArgs e)
        {
            var frm = new frmDevice(_client);
            frm.Owner = this;
            if (frm.ShowDialog() == true)
            {
                var request = new CreateOrUpdateDeviceRequest
                {
                    Name = frm.DeviceData.Name,
                    Description = frm.DeviceData.Description,
                    Address = frm.DeviceData.Address.Value,
                    ModelId = frm.DeviceData.ModelId
                };

                var addedDevice = _client.CreateDeviceAsync(request).Result;
                _devices.Add(addedDevice);
                dgDevices.Items.Refresh();
            }
        }

        private void btnUpdateDevice_Click(object sender, RoutedEventArgs e)
        {
            if (dgDevices.SelectedItem != null)
            {
                var frm = new frmDevice(_client);
                frm.Owner = this;
                frm.DeviceData = new ViewModels.DeviceViewModel
                {
                    Name = ((DeviceDto)dgDevices.SelectedItem).Name,
                    Description = ((DeviceDto)dgDevices.SelectedItem).Description,
                    Address = ((DeviceDto)dgDevices.SelectedItem).Address,
                    Id = ((DeviceDto)dgDevices.SelectedItem).Id,
                    ModelId = ((DeviceDto)dgDevices.SelectedItem).Model.Id
                };
                if (frm.ShowDialog() == true)
                {
                    var request = new CreateOrUpdateDeviceRequest
                    {
                        Name = frm.DeviceData.Name,
                        Description = frm.DeviceData.Description,
                        Address = frm.DeviceData.Address.Value,
                        ModelId = frm.DeviceData.ModelId
                    };

                    var updatedDevice = _client.UpdateDeviceAsync(frm.DeviceData.Id.Value, request).Result;
                    _devices.Remove((DeviceDto)dgDevices.SelectedItem);
                    _devices.Add(updatedDevice);
                    dgDevices.Items.Refresh();
                }
            }
        }

        private void btnDeleteDevice_Click(object sender, RoutedEventArgs e)
        {
            if (dgDevices.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure to delete device?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _client.DeleteDeviceAsync(((DeviceDto)dgDevices.SelectedItem).Id);
                    _devices.Remove((DeviceDto)dgDevices.SelectedItem);
                    dgDevices.Items.Refresh();
                }
            }
        }

       

        private void dgDevices_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgDevices.SelectedItem != null)
            {
                var deviceDto = (DeviceDto)dgDevices.SelectedItem;
                _schedules.Clear();
                _schedules.AddRange( _client.GetScheduleByParent(deviceDto.Id).Result.ToList());
                dgSchedules.Items.Refresh();
            }
        }

        private void btnAddSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (dgDevices.SelectedItem != null)
            {
                var frm = new frmSchedule(_client);
                frm.Owner = this;
                if (frm.ShowDialog() == true)
                {
                    var request = new CreateOrUpdateScheduleRequest
                    {
                        ParentId = ((DeviceDto)dgDevices.SelectedItem).Id,
                        Name = frm.ScheduleData.Name,
                        Description = frm.ScheduleData.Description,
                        StartCron = frm.ScheduleData.StartCron,
                        FinishCron = frm.ScheduleData.FinishCron,
                        FinishBy = frm.ScheduleData.FinishBy,
                        FinishDeviceId = frm.ScheduleData.FinishDeviceId,
                        FinishValue = frm.ScheduleData.FinishValue
                    };
                    var res = _client.CreateScheduleAsync(request.ParentId, request).Result;
                    _schedules.Add(res);
                    dgSchedules.Items.Refresh();
                }
            }
        }

        private void btnDeleteSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (dgSchedules.SelectedItem != null)
            {
                _client.DeleteScheduleAsync(((ScheduleDto)dgSchedules.SelectedItem).Id);
                _schedules.Remove((ScheduleDto)dgSchedules.SelectedItem);
                dgSchedules.Items.Refresh();
            }
        }

        private void btnUpdateSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (dgSchedules.SelectedItem != null)
            {
                var frm = new frmSchedule(_client);
                frm.Owner = this;
                var toEdit = (ScheduleDto)dgSchedules.SelectedItem;
                frm.ScheduleData = new ViewModels.ScheduleViewModel()
                {
                    Name = toEdit.Name,
                    Description = toEdit.Description,
                    StartCron = toEdit.StartCron,
                    FinishDeviceId=toEdit.FinishDeviceId,
                    FinishValue=toEdit.FinishValue,
                    FinishBy = toEdit.FinishBy,
                    FinishCron = toEdit.FinishCron
                };
                
                if (frm.ShowDialog() == true)
                {
                    var request = new CreateOrUpdateScheduleRequest
                    {
                        ParentId = ((DeviceDto)dgDevices.SelectedItem).Id,
                        Name = frm.ScheduleData.Name,
                        Description = frm.ScheduleData.Description,
                        StartCron = frm.ScheduleData.StartCron,
                        FinishCron = frm.ScheduleData.FinishCron,
                        FinishBy = frm.ScheduleData.FinishBy,
                        FinishDeviceId = frm.ScheduleData.FinishDeviceId,
                        FinishValue = frm.ScheduleData.FinishValue
                    };

                    var edited = _client.SchedulesPUT(toEdit.Id, request).Result;
                    _schedules.Remove(toEdit);
                    _schedules.Add(edited);
                    dgSchedules.Items.Refresh();
                }
            }
        }
    }
}