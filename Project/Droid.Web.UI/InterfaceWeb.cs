// Log code : 00 01

using Droid.Web.UI.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Tools.Utilities;
using Tools.Utilities.UI;

namespace Droid.Web.UI
{
    public class InterfaceWeb : GPInterfaceUI
    {
        #region Attributes
        public new event Tools.Utilities.UI.InterfaceEventHandler SheetDisplayRequested;

        //public readonly int TOP_OFFSET = 175;
        private int _topOffset = 150;

        private static InterfaceWeb _this;

        private new ToolStripMenuWEB _tsm;
        private string _workingDirectory;

        private ViewNetwork _viewNetwork;
        private ViewIAWebBrowser _viewBrowser;
        private ViewBrowser _viewWebBrowser;
        private Random _rand;
        private string _randEMail;
        #endregion

        #region Properties
        public string WorkingDirectory
        {
            get { return _workingDirectory; }
            set { _workingDirectory = value; }
        }
        public ToolStripMenuWEB Tsm
        {
            get { return _tsm; }
            set { _tsm = value; }
        }
        public string RandomEmail
        {
            get { return _randEMail; }
            set { _randEMail = value; }
        }
        #endregion

        #region Constructor
        static InterfaceWeb()
        {
            _this = new InterfaceWeb();
        }
        public InterfaceWeb()
        {
            Init();
        }
        #endregion

        #region Methods public
        public override bool Open(object o)
        {
            return false;
        }
        public override void GoAction(string action)
        {
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "Open":
                        LaunchOpen();
                        break;
                    case "WebBrowser":
                        LaunchBrowser();
                        break;
                }
            }
        }
        public RibbonTab BuildToolBar()
        {
            _tsm = new ToolStripMenuWEB(this);
            _tsm.ActionAppened += GlobalAction;
            return _tsm;
        }
        #endregion

        #region Methods private
        private void Init()
        {
            _workingDirectory = Params.ConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Servodroid\";

            _rand = new Random((int) DateTime.Now.Ticks);

            _sheet = new PanelScrollableCustom();
            _sheet.Name = "SheetManager";
            _sheet.BackgroundImage = Properties.Resource.ShieldTileBg;
            _sheet.BackgroundImageLayout = ImageLayout.Tile;
            _sheet.BackColor = System.Drawing.Color.FromArgb(37, 37, 37);
            _sheet.Dock = DockStyle.Fill;
            _sheet.Resize += _sheet_Resize;

            BuildToolBar();

            LaunchHome();
        }

        private void LaunchOpen()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Open(ofd.FileName);
            }
            SheetDisplayRequested?.Invoke(null);
        }
        private void LaunchFrench()
        {
            GetText.CurrentLanguage = GetText.Language.FRENCH;
            Properties.Settings.Default.language = GetText.Language.FRENCH.ToString();
            _tsm.UpdateLanguage();
            Properties.Settings.Default.Save();
            SheetDisplayRequested?.Invoke(null);
        }
        private void LaunchEnglish()
        {
            GetText.CurrentLanguage = GetText.Language.ENGLISH;
            Properties.Settings.Default.language = GetText.Language.ENGLISH.ToString();
            _tsm.UpdateLanguage();
            Properties.Settings.Default.Save();
            SheetDisplayRequested?.Invoke(null);
        }
        private void LaunchRandEmail()
        {
            _randEMail = string.Format("{0}_{1}@{2}.com", _rand.Next(), _rand.Next(), _rand.Next());
        }

        private void LaunchHome()
        {
            if (_viewNetwork == null) { _viewNetwork = new ViewNetwork(this); }
            LaunchSheet(_viewNetwork, fullSize: true, offset: _topOffset);
        }
        private void LaunchBrowserIA()
        {
            if (_viewNetwork == null) { _viewBrowser = new ViewIAWebBrowser(); }
            LaunchSheet(_viewNetwork, offset: _topOffset);
        }
        private void LaunchBrowser()
        {
            if (_viewWebBrowser == null) { _viewWebBrowser = new ViewBrowser(); }
            LaunchSheet(_viewWebBrowser, fullSize:true, offset: _topOffset);
        }
        #endregion

        #region Event
        private void _sheet_Resize(object sender, EventArgs e)
        {
            Resize();
        }
        #endregion
    }
}
