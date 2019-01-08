using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace Droid.Web.UI
{
    public partial class PanelNetwork : UserControl
    {
        #region Attributes
        private long _lastDataSent;
        private long _lastDataReceived;
        private NetworkInterface _networkInterface;
        #endregion

        #region Properties
        public long lastDataSent
        {
            get { return _lastDataSent; }
        }
        public long lastDataReceived
        {
            get { return _lastDataReceived; }
        }
        public NetworkInterface NetworkInterface
        {
            get { return _networkInterface; }
            set { _networkInterface = value; }
        }
        #endregion

        #region Constructor
        public PanelNetwork()
        {
            InitializeComponent();
            Init();
        }
        public PanelNetwork(NetworkInterface netInt)
        {
            _networkInterface = netInt;
            InitializeComponent();
            Init();
        }
        #endregion

        #region Methods public
        public void UpdateData(NetworkInterface netInt = null)
        {
            long dataIn = 0;
            long dataOut = 0;

            if (netInt != null) _networkInterface = netInt;
            dataOut += _networkInterface.GetIPv4Statistics().BytesSent;
            dataIn += _networkInterface.GetIPv4Statistics().BytesReceived;

            labelName.Text = _networkInterface.Name;
            labelDescription.Text = _networkInterface.Description;
            labelType.Text = _networkInterface.NetworkInterfaceType.ToString();
            labelID.Text = _networkInterface.Id;
            labelPhysicalAddress.Text = _networkInterface.GetPhysicalAddress().ToString();
            //var v1 = _networkInterface.GetIPProperties();
            //var v2 = _networkInterface.GetIPv4Statistics();
            //var v3 = _networkInterface.GetIPStatistics();

            //Console.WriteLine("  => data in : {0} data out : {1}", dataIn, dataOut);

            if (_lastDataReceived != 0)
            {
                chart1.Series["Data received"].Points.AddY(dataIn - _lastDataReceived);
                chart1.Series["Data sent"].Points.AddY(dataOut - _lastDataSent);
            }
            _lastDataSent = dataOut;
            _lastDataReceived = dataIn;
        }
        #endregion

        #region Methods private
        private void Init()
        {
            _lastDataSent = 0;
            _lastDataReceived = 0;
        }
        #endregion

        #region Event
        #endregion
    }
}
