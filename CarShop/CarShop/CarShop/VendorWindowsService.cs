using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CarShop
{
    public partial class VendorWindowsService : ServiceBase
    {
        public VendorWindowsService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            FileWatcher watcher = new FileWatcher();
            watcher.StartWork();
        }

        protected override void OnStop()
        {

        }
    }
}
