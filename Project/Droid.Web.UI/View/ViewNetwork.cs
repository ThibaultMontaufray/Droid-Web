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
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Droid.Web.UI
{
    public partial class ViewNetwork : UserControlCustom
    {
        #region Attributes
        private InterfaceWeb _intWeb;
        private Timer _timer;
        private List<PanelNetwork> _panels;
        private NetworkInterface[] _interfaces;
        private int _offset;
        private bool _left;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public ViewNetwork(InterfaceWeb intWeb)
        {
            _intWeb = intWeb;
            InitializeComponent();
            Init();
        }
        #endregion

        #region Methods public
        #endregion

        #region Methods private
        private void Init()
        {
            InitNetworkInterface();
            InitTimer();
        }
        private void InitNetworkInterface()
        {
            _left = true;
            _offset = 0;
            _interfaces = NetworkInterface.GetAllNetworkInterfaces();
            _panels = new List<PanelNetwork>();

            if (!NetworkInterface.GetIsNetworkAvailable()) { return; }

            foreach (NetworkInterface ni in _interfaces)
            {
                if (ni.Speed != -1)
                { 
                    BuildNetworkPanels(ni);
                }
            }
        }
        private void InitTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }
        private void BuildNetworkPanels(NetworkInterface ni)
        {
            PanelNetwork pn = new PanelNetwork();
            pn.NetworkInterface = ni;
            pn.UpdateData();

            pn.Left = _left ? 5 : this.Width / 2;
            pn.Width = (this.Width / 2) - 25;
            pn.Top = _offset + 5;

            _left = !_left;
            if (_left) _offset = pn.Top + pn.Height;
            _panels.Add(pn);
            panelScrollable.Controls.Add(pn);
        }
        private void ScanNetwork()
        {
            if (!NetworkInterface.GetIsNetworkAvailable()) { return; }
            
            foreach (NetworkInterface ni in _interfaces)
            {
                var plst = _panels.Where(p => p.NetworkInterface.Name.Equals(ni.Name));
                if (plst.Count() > 0)
                { 
                    plst.First().UpdateData(ni);
                }
            }
        }
        #endregion

        #region Event
        private void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            ScanNetwork();
            _timer.Start();
        }
        #endregion
    }
}
