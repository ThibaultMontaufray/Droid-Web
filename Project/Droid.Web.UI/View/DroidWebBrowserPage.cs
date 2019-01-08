using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using Tools.Utilities.UI;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace Droid.Web.UI
{
    public delegate void DroidWebBrowserPageEventHandler(object o);
    public partial class DroidWebBrowserPage : UserControlCustom
    {
        #region Attributes
        private const string HOME = "www.servodroid.online";

        public event DroidWebBrowserPageEventHandler Refresh;
        private string _defaultUrl; // requested address
        private string _file;
        private ChromiumWebBrowser _chromeBrowser;
        private string _currentAddress; // browser address
        #endregion

        #region Properties
        public string CurrentAddress
        {
            get { return _currentAddress; }
            set { _currentAddress = value; }
        }
        public string File
        {
            get { return _file; }
            set
            {
                _file = value;
                if (!string.IsNullOrEmpty(value))
                {
                    _defaultUrl = string.Empty;
                    RefreshData();
                }
            }
        }
        public string DefaultUrl
        {
            get { return _defaultUrl; }
            set
            {
                _defaultUrl = value;
                if (!string.IsNullOrEmpty(value))
                {
                    _file = string.Empty;
                    RefreshData();
                }
            }
        }
        public Icon Favicon
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(_currentAddress))
                    {
                        WebClient wc = new WebClient();
                        Uri uri = new Uri(_defaultUrl.StartsWith("http") ? _defaultUrl : "http://" + _defaultUrl);
                        MemoryStream memorystream = new MemoryStream(wc.DownloadData(string.Format(@"http://www.google.com/s2/favicons?domain={0}", uri.Host)));
                        Bitmap bitmap = new Bitmap(System.Drawing.Image.FromStream(memorystream));
                        bitmap.SetResolution(72, 72);
                        return System.Drawing.Icon.FromHandle(bitmap.GetHicon());
                    }
                    else
                    {
                        return Properties.Resource.DefaultIcon;
                    }
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    return Properties.Resource.DefaultIcon;
                }
            }
        }
        public string Title
        {
            get
            {
                WebResponse response = null;
                string line = "WEB";

                try
                {
                    if (string.IsNullOrEmpty(_defaultUrl)) return line;
                    WebRequest request = WebRequest.Create(_defaultUrl.StartsWith("http") ? _defaultUrl : "http://" + _defaultUrl);
                    request.Timeout = 1000;

                    response = request.GetResponse();
                    Stream streamReceive = response.GetResponseStream();
                    Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader streamRead = new System.IO.StreamReader(streamReceive, encoding);
                    while (streamRead.EndOfStream != true)
                    {
                        line = streamRead.ReadLine();
                        if (line.Contains("<title>"))
                        {
                            line = line.Split(new char[] { '<', '>' })[2];
                            break;
                        }
                    }
                    if ((string.IsNullOrEmpty(line) || line.Equals("WEB")) && (!string.IsNullOrEmpty(request.RequestUri.IdnHost) && request.RequestUri.IdnHost.Split('.').Count() > 1))
                    {
                        line = request.RequestUri.IdnHost.Split('.')[1];
                    }
                }
                catch (Exception exp) { Console.WriteLine(exp.Message); }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }

                return line;
            }
        }
        #endregion

        #region Constructor
        public DroidWebBrowserPage()
        {
            InitializeComponent();
            Init();
        }
        #endregion

        #region Methods public
        public override void RefreshData()
        {
            try
            {
                textBoxUrl.Text = string.IsNullOrEmpty(_defaultUrl) ? _file : _defaultUrl;
                if (!string.IsNullOrEmpty(_file) && System.IO.File.Exists(_file))
                {
                    var page = new Uri(string.Format("file:///{0}", _file));
                    _chromeBrowser?.Load(page.ToString());
                }
                else
                {
                    _chromeBrowser?.Load(_defaultUrl);
                }
                _currentAddress = _defaultUrl;
                _chromeBrowser?.Refresh();
                //_chromeBrowser.Invalidate();
                Refresh?.Invoke(this);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
        public void LoadFile(string file)
        {

        }
        public void ChangeUrlPanel(bool display)
        {
            if (display)
            {
                panelUrl.Height = 31;
            }
            else
            {
                panelUrl.Height = 0;
            }
        }
        #endregion

        #region Methods private
        private void Init()
        {
            _defaultUrl = HOME;
            this.Load += DroidWebBrowserPage_Load;
        }
        private void RebuildBrowser()
        {
            if (_chromeBrowser == null)
            {
                try
                {
                    _chromeBrowser = new ChromiumWebBrowser(_defaultUrl);
                    _chromeBrowser.Dock = DockStyle.None;
                    _chromeBrowser.Width = this.Width;
                    _chromeBrowser.Height = this.Height - panelUrl.Height;
                    _chromeBrowser.Left = 0;
                    _chromeBrowser.Top = panelUrl.Height;
                    _chromeBrowser.Name = "webbrowser";
                    _chromeBrowser.AddressChanged += _chromeBrowser_AddressChanged;
                    _chromeBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
                    this.Controls.RemoveByKey(_chromeBrowser.Name);
                    this.Controls.Add(_chromeBrowser);
                    //ChromeDevToolsSystemMenu.CreateSysMenu(this);
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }
        #endregion

        #region Event
        private void _chromeBrowser_AddressChanged(object sender, CefSharp.AddressChangedEventArgs e)
        {
            _currentAddress = e.Address;
        }
        private void DroidWebBrowserPage_Load(object sender, EventArgs e)
        {
            RebuildBrowser();
            RefreshData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _chromeBrowser.Load(textBoxUrl.Text);
            _chromeBrowser.Refresh();
        }
        private void backButton_MouseEnter(object sender, EventArgs e)
        {
            //backButton.BackgroundImage = Properties.Resource.ButtonHoverBackground;
        }
        private void backButton_MouseLeave(object sender, EventArgs e)
        {
            //backButton.BackgroundImage = null;
        }
        private void urlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string fullUrl = textBoxUrl.Text;
                if (fullUrl.Split(' ').Count() > 1)
                {
                    _defaultUrl = "https://www.google.fr/search?q=";
                    foreach (var item in fullUrl.Split(' '))
                    {
                        _defaultUrl += "+" + item;
                    }
                }
                else
                { 
                    if (!Regex.IsMatch(fullUrl, "^[a-zA-Z0-9]+\\://"))
                        fullUrl = "http://" + fullUrl;

                    Uri uri = new Uri(fullUrl);
                    _defaultUrl = fullUrl;
                }
                RefreshData();
                //_chromeBrowser.Navigate(uri);
            }
        }
        private void forwardButton_MouseEnter(object sender, EventArgs e)
        {
            //forwardButton.BackgroundImage = Resources.ButtonHoverBackground;
        }
        private void forwardButton_MouseLeave(object sender, EventArgs e)
        {
            //forwardButton.BackgroundImage = null;
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            //webBrowser.GoBack();
        }
        private void forwardButton_Click(object sender, EventArgs e)
        {
            //webBrowser.GoForward();
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void homeButton_Click(object sender, EventArgs e)
        {
            _defaultUrl = HOME;
            RefreshData();
        }
        private void DroidWebBrowserPage_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion
    }
}
