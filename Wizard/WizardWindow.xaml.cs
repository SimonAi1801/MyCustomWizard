using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace Wizard
{
    /// <summary>
    /// Interaction logic for WizardWindow.xaml
    /// </summary>
    public partial class WizardWindow : Window
    {
        private bool _isTextValid = false;
        private string _serverUrl;
        private string _userId;
        private string _userDirectory;
        private string _headName;
        private string _proxyPath;
        private string _appId;
        public WizardWindow()
        {
            InitializeComponent();
            LicensePage.CanSelectNextPage = false;
            CustomInformationPage.CanSelectNextPage = false;
            txtLicense.TextChanged += TxtLicensePage_TextChanged;
            txtAppId.TextChanged += TxtCIPage_TextChanged;
            txtProxyPath.TextChanged += TxtCIPage_TextChanged;
            txtUrl.TextChanged += TxtCIPage_TextChanged;
            txtUserDirectory.TextChanged += TxtCIPage_TextChanged;
            txtUserId.TextChanged += TxtCIPage_TextChanged;
            txtxHeadName.TextChanged += TxtCIPage_TextChanged;
            btnLicenseSubmit.Click += BtnLicense_Click;
            WizardWindow wizardWindow = new WizardWindow();

        }


        #region Helpers
        private bool IsAllFilled(string serverUrl, string appId, string headName, string userId, string userDirectory, string proxyPath)
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(_serverUrl) && !string.IsNullOrEmpty(_appId) && !string.IsNullOrEmpty(_headName) &&
                !string.IsNullOrEmpty(_userId) && !string.IsNullOrEmpty(_userDirectory) && !string.IsNullOrEmpty(_proxyPath))
            {
                isValid = true;
            }
            return isValid;
        }

        private async Task<bool> IsLiceneValid()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44357/");

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = await client.GetAsync(client.BaseAddress + "api/License/" + $"{txtLicense.Text}");

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Events
        private async void BtnLicense_Click(object sender, RoutedEventArgs e)
        {
            if (_isTextValid && await IsLiceneValid())
            {
                txtLicenseValidationFail.Visibility = Visibility.Hidden;
                txtLicenseValidationSucc.Visibility = Visibility.Visible;
                LicensePage.CanSelectNextPage = true;
            }
            else
            {
                txtLicenseValidationFail.Visibility = Visibility.Visible;
                txtLicenseValidationSucc.Visibility = Visibility.Hidden;
            }
        }


        private void TxtLicensePage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLicense.Text) && txtLicense.Text.Length >= 1)
            {
                _isTextValid = true;
            }
            else
            {
                LicensePage.CanSelectNextPage = false;
                _isTextValid = false;
            }
        }
        private void TxtCIPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            switch (textBox.Name)
            {
                case "txtUrl":
                    {
                        _serverUrl = textBox.Text;
                        if (IsAllFilled(_serverUrl, _appId, _headName, _userId, _userDirectory, _proxyPath))
                        {
                            CustomInformationPage.CanSelectNextPage = true;
                        }
                        else
                        {
                            CustomInformationPage.CanSelectNextPage = false;
                        }
                        break;
                    }
                case "txtUserId":
                    {
                        _userId = textBox.Text;
                        if (IsAllFilled(_serverUrl, _appId, _headName, _userId, _userDirectory, _proxyPath))
                        {
                            CustomInformationPage.CanSelectNextPage = true;
                        }
                        else
                        {
                            CustomInformationPage.CanSelectNextPage = false;
                        }
                        break;
                    }
                case "txtUserDirectory":
                    {
                        _userDirectory = textBox.Text;
                        if (IsAllFilled(_serverUrl, _appId, _headName, _userId, _userDirectory, _proxyPath))
                        {
                            CustomInformationPage.CanSelectNextPage = true;
                        }
                        else
                        {
                            CustomInformationPage.CanSelectNextPage = false;
                        }
                        break;
                    }
                case "txtxHeadName":
                    {
                        _headName = textBox.Text;
                        if (IsAllFilled(_serverUrl, _appId, _headName, _userId, _userDirectory, _proxyPath))
                        {
                            CustomInformationPage.CanSelectNextPage = true;
                        }
                        else
                        {
                            CustomInformationPage.CanSelectNextPage = false;
                        }
                        break;
                    }
                case "txtProxyPath":
                    {
                        _proxyPath = textBox.Text;
                        if (IsAllFilled(_serverUrl, _appId, _headName, _userId, _userDirectory, _proxyPath))
                        {
                            CustomInformationPage.CanSelectNextPage = true;
                        }
                        else
                        {
                            CustomInformationPage.CanSelectNextPage = false;
                        }
                        break;
                    }
                case "txtAppId":
                    {
                        _appId = textBox.Text;
                        if (IsAllFilled(_serverUrl, _appId, _headName, _userId, _userDirectory, _proxyPath))
                        {
                            CustomInformationPage.CanSelectNextPage = true;
                        }
                        else
                        {
                            CustomInformationPage.CanSelectNextPage = false;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        #endregion
    }
}
