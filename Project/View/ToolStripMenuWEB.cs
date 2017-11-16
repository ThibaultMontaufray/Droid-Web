using System;
using System.Windows.Forms;
using Tools4Libraries;

namespace Droid_web
{
    public class ToolStripMenuWEB : RibbonTab
    {
        #region Attributes
        public event EventHandlerAction ActionAppened;

        private RibbonPanel _panelMain;
        private RibbonButton _ts_main_home;
        private RibbonButton _ts_main_webbrowser;

        private InterfaceWeb _intWeb;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public ToolStripMenuWEB(InterfaceWeb intWeb)
        {
            _intWeb = intWeb;
            Init();
        }
        #endregion

        #region Methods public
        public void OnAction(EventArgs e)
        {
            if (ActionAppened != null) ActionAppened(this, e);
        }
        public void UpdateLanguage()
        {
            this.Text = GetText.Text("Manager");

            switch (Properties.Settings.Default.language.ToString())
            {
                case "ENGLISH":
                    //_ts_language.Image = Tools4Libraries.Resources.ResourceIconSet32Default.flag_great_britain;
                    break;
                case "FRENCH":
                    //_ts_language.Image = Tools4Libraries.Resources.ResourceIconSet32Default.flag_france;
                    break;
                default:
                    //_ts_language.Image = Tools4Libraries.Resources.ResourceIconSet32Default.flag_orange;
                    break;
            }
            _panelMain.Text = GetText.Text(_panelMain.Name);
        }
        #endregion

        #region Methods private
        private void Init()
        {
            BuildPanelMain();
            this.Text = GetText.Text("Web");
        }
        private void BuildPanelMain()
        {
            _ts_main_home = new RibbonButton();
            _ts_main_home.Name = "Home";
            _ts_main_home.Text = GetText.Text(_ts_main_home.Name);
            _ts_main_home.ToolTip = GetText.Text(_ts_main_home.Name);
            _ts_main_home.Click += new EventHandler(tsb_Click);
            _ts_main_home.Image = Tools4Libraries.Resources.ResourceIconSet32Default.open_folder;
            _ts_main_home.SmallImage = Tools4Libraries.Resources.ResourceIconSet16Default.open_folder;
            _ts_main_home.MinSizeMode = RibbonElementSizeMode.Large;

            _ts_main_webbrowser = new RibbonButton();
            _ts_main_webbrowser.Name = "WebBrowser";
            _ts_main_webbrowser.Text = GetText.Text(_ts_main_webbrowser.Name);
            _ts_main_webbrowser.ToolTip = GetText.Text(_ts_main_webbrowser.Name);
            _ts_main_webbrowser.Click += new EventHandler(tsb_Click);
            _ts_main_webbrowser.Image = Tools4Libraries.Resources.ResourceIconSet32Default.open_folder;
            _ts_main_webbrowser.SmallImage = Tools4Libraries.Resources.ResourceIconSet16Default.open_folder;
            _ts_main_webbrowser.MinSizeMode = RibbonElementSizeMode.Large;
            
            _panelMain = new RibbonPanel();
            _panelMain.Image = Tools4Libraries.Resources.ResourceIconSet16Default.open;
            _panelMain.Name = "Main";
            _panelMain.Text = GetText.Text(_panelMain.Name);
            _panelMain.Items.Add(_ts_main_home);
            _panelMain.Items.Add(_ts_main_webbrowser);
            this.Panels.Add(_panelMain);
        }
        #endregion

        #region Event
        private void tsb_Click(object sender, EventArgs e)
        {
            if (sender is RibbonButton)
            { 
                RibbonButton rb = sender as RibbonButton;
                ToolBarEventArgs action = new ToolBarEventArgs(rb.Name);
                OnAction(action);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
