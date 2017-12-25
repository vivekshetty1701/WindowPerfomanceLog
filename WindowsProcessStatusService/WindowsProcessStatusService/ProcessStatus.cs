using System.IO;
using System.ServiceProcess;

namespace WindowsProcessStatusService
{
    public partial class ProcessStatus : ServiceBase
    {
        System.Timers.Timer timeDelay;
        int count;

        public ProcessStatus()
        {
            InitializeComponent();
            timeDelay = new System.Timers.Timer(10);
            timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(WorkProcess);
        }

        public void WorkProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            string process = "Timer Tick " + count;
            LogService(process);
            count++;
        }

        protected override void OnStart(string[] args)
        {
            LogService("Service is Started");
            timeDelay.Enabled = true;
        }

        protected override void OnStop()
        {
            LogService("Service Stoped");
            timeDelay.Enabled = false;
        }

        private void LogService(string content)
        {
            FileStream fs = new FileStream(@"D:\TestServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
