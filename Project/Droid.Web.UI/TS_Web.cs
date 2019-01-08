namespace Droid.Web.UI
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.ServiceProcess;

    public partial class TS_Web : ServiceBase
    {
        #region Enum
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }
        public enum ServiceAction
        {
            WIKI_ENRICH_PEOPLE = 130,
            WIKI_NOM_DESC = 131,
            GOOGLE_ACTU = 132
        }
        #endregion

        #region Struct
        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public long dwServiceType;
            public ServiceState dwCurrentState;
            public long dwControlsAccepted;
            public long dwWin32ExitCode;
            public long dwServiceSpecificExitCode;
            public long dwCheckPoint;
            public long dwWaitHint;
        };
        #endregion

        #region Attribute
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public TS_Web()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods protected
        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            
            _eventLog.WriteEntry("In OnStart");
            // Set up a timer to trigger every minute.
            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Interval = 60000; // 60 seconds
            //timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            //timer.Start();

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }
        protected override void OnStop()
        {
            _eventLog.WriteEntry("In onStop.");
        }
        protected override void OnCustomCommand(int command)
        {
            try
            {
                base.OnCustomCommand(command);
                switch (command)
                {
                    case (int)ServiceAction.WIKI_ENRICH_PEOPLE:
                        _eventLog.WriteEntry("Wiki enrich people.");
                        break;
                    case (int)ServiceAction.WIKI_NOM_DESC:
                        _eventLog.WriteEntry("Wiki nom desc.");
                        break;
                    case (int)ServiceAction.GOOGLE_ACTU:
                        _eventLog.WriteEntry("Google actu.");
                        break;
                    default:
                        _eventLog.WriteEntry("Default action : " + command);
                        break;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
        #endregion
    }
}
