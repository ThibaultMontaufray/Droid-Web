using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyTabs;
using System.IO;

namespace Droid.Web.UI.View
{
    public partial class WebBrowserForm : TitleBarTabs
    {
        protected TitleBarTabs ParentTabs
        {
            get
            {
                try
                {
                    return (ParentForm as TitleBarTabs);
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    return null;
                }
            }
        }

        public WebBrowserForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            AeroPeekEnabled = true;
            TabRenderer = new ChromeTabRenderer(this);
            Icon = Properties.Resource.DefaultIcon;

            this.Tabs.Add(CreateTab("www.bimandco.com"));
            this.Tabs.Add(CreateTab("www.google.fr"));
            this.Tabs.Add(CreateTab("www.servodroid.online"));

            TitleBarTab tbt = CreateTab(string.Empty, "D:/Github/POC-DAD/demo.html");
            this.Tabs.Add(tbt);

            this.Load += FormMain_Load;
        }

        public override TitleBarTab CreateTab()
        {
            return new TitleBarTab(this)
            {
                Content = new AppContainer
                {
                }
            };
        }

        public TitleBarTab CreateTab(string text)
        {
            return new TitleBarTab(this)
            {
                Content = new AppContainer
                {
                    Text = text
                }
            };
        }
        public TitleBarTab CreateTab(string text, string file)
        {
            AppContainer ac = new AppContainer()
            {
                Text = text,
                File = file
            };
            return new TitleBarTab(this) { Content = ac };
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void userControlCustom1_Load(object sender, EventArgs e)
        {

        }
    }
}
