using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyTabs;
using Tools.Utilities.UI;

namespace Droid.Web.UI.View
{
    public partial class AppContainer : TitleBarTabs
    {
        private DroidWebBrowserPage _webPage;
        private string _file;
        private string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public string File
        {
            get { return _file; }
            set { _file = value; }
        }


        public AppContainer()
        {
            InitializeComponent();

            AeroPeekEnabled = true;
            TabRenderer = new ChromeTabRenderer(this);
            //Icon = ((ChromeTabRenderer)TabRenderer).Favicon Properties.Resource.DefaultIcon;
            this.Load += AppContainer_Load;
        }

        // Handle the method CreateTab that allows the user to create a new Tab
        // on your app when clicking
        public override TitleBarTab CreateTab()
        {
            TitleBarTab tbt = new TitleBarTab(this)
            {
                // The content will be an instance of another Form
                // In our example, we will create a new instance of the Form1
                Content = new Form
                {
                }
            };
            
            return tbt;
        }

        public static explicit operator AppContainer(TitleBarTab v)
        {
            return (AppContainer)v;
        }
        private void Init()
        {
            _webPage = new DroidWebBrowserPage();
            _webPage.Dock = DockStyle.None;
            _webPage.Top = this.TabRenderer.TabHeight;
            _webPage.Left = 1;
            _webPage.Width = this.Width - 10;
            _webPage.Height = this.Height - _webPage.Top - 30;
            _webPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            _webPage.DefaultUrl = this.Text;
            _webPage.File = _file;
            _webPage.DefaultUrl = _url;
            this.Icon = _webPage.Favicon;
            this.Controls.Add(_webPage);
        }
        private void AppContainer_Load(object sender, EventArgs e)
        {
            Init();
        }

        //DroidWebBrowserPage webPage = new DroidWebBrowserPage();
        //webPage.Dock = DockStyle.Fill;
        //webPage.ChangeUrlPanel(false);
        //webPage.DefaultUrl = "www.bimandco.com";

        // The rest of the events in your app here if you need to .....
    }
}
