using SA.Irrigation.Desktop.RestApiClient;
using SA.Irrigation.Desktop.ViewModels;
using System.Windows;
using System.Windows.Data;

namespace SA.Irrigation.Desktop.Forms
{
    /// <summary>
    /// Interaction logic for frmDevice.xaml
    /// </summary>
    public partial class frmDevice : Window
    {
        private readonly ISAIrrigationAPI _client;
        public DeviceViewModel? DeviceData { get; set; }

        private CollectionView _models;

        public CollectionView ModelsView { get { return _models; } }

        public frmDevice(ISAIrrigationAPI client)
        {
            ArgumentNullException.ThrowIfNull(client, nameof(client));
            _client = client;
            _models = new CollectionView(_client.GetAllModelsAsync().Result.ToList());
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbModels.ItemsSource = ModelsView;
            cbModels.DisplayMemberPath = "Name";
            cbModels.SelectedValuePath = "Id";
            if (DeviceData != null)
            {
                cbModels.SelectedItem = cbModels.Items.OfType<DeviceModelDto>().FirstOrDefault(f => f.Id == DeviceData.ModelId);
                tbAddress.Text = DeviceData.Address.ToString();
                tbName.Text = DeviceData.Name;
                tbDescription.Text = DeviceData.Description;
            }
            else
            {
                cbModels.SelectedIndex = 0;
                DeviceData = new DeviceViewModel();
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (DeviceData == null) DeviceData = new DeviceViewModel();
            DeviceData.ModelId = (Guid)cbModels.SelectedValue;
            DeviceData.Address = Int32.Parse(tbAddress.Text);
            DeviceData.Name = tbName.Text;
            DeviceData.Description = tbDescription.Text;
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
