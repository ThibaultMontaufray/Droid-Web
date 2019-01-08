using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.Utilities.UI;

namespace Droid.Web.UI.View
{
    public partial class ViewBrowser : UserControlCustom
    {
        #region Attributes
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public ViewBrowser()
        {
            InitializeComponent();
            Init();
        }
        #endregion

        #region Methods public
        public void AddPage(string page)
        {
            tabControlCustom1.TabPages.Add(CreateTab(page));
        }
        #endregion

        #region Methods private
        private void Init()
        {
            tabControlCustom1.TabPages.Add(CreateTab("www.bimandco.com"));
            tabControlCustom1.TabPages.Add(CreateTab("www.google.fr"));
            tabControlCustom1.TabPages.Add(CreateTab("www.servodroid.online"));
        }
        private TabPage CreateTab(string url)
        {
            DroidWebBrowserPage page = new DroidWebBrowserPage();
            page.DefaultUrl = url;
            page.Dock = DockStyle.Fill;
            page.Refresh += Page_Refresh;
            page.Name = DateTime.Now.Ticks.ToString();
            if (!imageListFavicon.Images.ContainsKey(page.Title))
            {
                imageListFavicon.Images.Add(page.Title, page.Favicon);
            }

            TabPage tab = new TabPage();
            tab.Controls.Add(page);
            tab.Text = page.Title.Length > 20 ? page.Title.Substring(0, 17) + ".." : page.Title;
            tab.ImageKey = page.Title;
            tab.ToolTipText = page.Title;
            tab.Name = page.Name;
            return tab;
        }
        private void RefreshPage(DroidWebBrowserPage browser)
        {
            if (browser != null)
            { 
                foreach (TabPage page in tabControlCustom1.TabPages)
                {
                    if (page.Name == browser.Name)
                    {
                        page.Text = browser.Title.Length > 20 ? browser.Title.Substring(0, 17) + ".." : browser.Title;
                        imageListFavicon.Images.Add(browser.Title, browser.Favicon);
                        page.ImageKey = browser.Title;
                        page.ToolTipText = browser.Title;
                        break;
                    }
                }
            }
        }
        #endregion

        #region Event
        private void Page_Refresh(object o)
        {
            var browser = (DroidWebBrowserPage)o;
            RefreshPage(browser);
        }
        private void WebBrowserControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }
        #endregion
    }
}
