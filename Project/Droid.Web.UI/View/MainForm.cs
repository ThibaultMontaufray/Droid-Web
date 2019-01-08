using EasyTabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Droid.Web.UI.View
{
    public partial class MainForm : Form
    {
        #region Attributes
        private WebBrowserForm _browser;
        #endregion

        #region Properties
        public WebBrowserForm Browser
        {
            get { return _browser; }
            set { _browser = value; }
        }
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            Init();
        }
        #endregion

        #region Methods public
        #endregion

        #region Methods private
        private void Init()
        {
        }
        #endregion

        #region Event
        #endregion
    }
}
