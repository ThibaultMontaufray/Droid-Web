namespace Droid_web
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using Tools4Libraries;

    public partial class ViewIAWebBrowser : UserControlCustom
    {
        #region Attribute
        private WebSeeker _ws;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public ViewIAWebBrowser()
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
            WebSeeker.ResultFound += WebSeeker_ResultFound;

            comboBox1.Items.Clear();
            foreach (var item in WebSeeker.Components)
            {
                comboBox1.Items.Add(item);
            }
        }
        private void LookingFor()
        {
            WebSeeker.Search(comboBox1.Text, textBox1.Text);
        }
        private void DisplayResultWeb()
        {
            if (WebSeeker.Result == null)
            {
                _webBrowser.DocumentText = "<center>Aucun résultat pour cette recherche.</center>";
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                Type parserType = Type.GetType("Droid_web.Parser" + WebSeeker.Result.GetType().Name);
                MethodInfo parseMethod = parserType.GetMethod("HtmlFormat");
                string html = "<html><head></head><body style='background-color:#000;'><center>" + ObjectToHtml.GetHtml(WebSeeker.Result) + "</center></body></html>";
                _webBrowser.ScriptErrorsSuppressed = true;
                _webBrowser.DocumentText = html;
                _webBrowser.Invalidate();
                textBoxPreview.Text = html;
                Cursor = Cursors.Arrow;
            }
        }
        #endregion

        #region Event
        private void button1_Click(object sender, EventArgs e)
        {
            _webBrowser.DocumentText = string.Empty;
            LookingFor();
        }
        private void WebSeeker_ResultFound(string htmlDisplay, object result)
        {
            DisplayResultWeb();
        }
        #endregion
    }
}
