using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Management;
using IWshRuntimeLibrary;

namespace ProxyServer
{
    class SystemMeter
    {
        #region Importing Win32 DLL's
        //Importing library User32.dll from %windir%\windows\system32\user32.dll
        [DllImport("User32.dll")]
        //method in user32.dll to move a borderless form.
        public static extern bool ReleaseCapture();
        //again importing the same user32.dll.
        [DllImport("User32.dll")]
        //method in user32.dll to pass message to the windows when mouse is down over the form.
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        
        #endregion
        #region LocalVariables
        private int _ramUnit = 0;
        private int _cpuUnit = 0;
        private int _drives = 0;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        private Int64 _totalRamMemory = 0;
        private Int64 _freePhysicalMemory = 0;
       
        private bool _allowDrawSpace = true;
       // private Settings _set;
        
        private PerformanceCounter _perCpu;
        
        #endregion

        public event EventHandler<Event_Meter> SystemMeter_Event;
        public SystemMeter()
        {
            _perCpu = new PerformanceCounter();
            _perCpu.CategoryName = "Processor";
            _perCpu.CounterName = "% Processor Time";
            _perCpu.InstanceName = "_Total";
        }
        public void startMeter()
        {
            try
            {
                //Rounding off to the approximate value of the float.
             //   _cpuUnit = Convert.ToInt32(Math.Round(_perCpu.NextValue()));
                //sets the variables with ram usage.
                GetPhysicalMemory();
                //setting the current ram usage in % 
                _ramUnit = Convert.ToInt32(Math.Round(Convert.ToDecimal(((_totalRamMemory - _freePhysicalMemory) * 100 / _totalRamMemory))));
                
                DriveInfo[] drive = DriveInfo.GetDrives();
                List<DriveInfo> hddDrives = new List<DriveInfo>();
                foreach (DriveInfo f in drive)
                {
                    if (f.DriveType == DriveType.Fixed)
                    {
                        hddDrives.Add(f);
                    }
                }
                int percentOccupied = 0;
                int drvcnt = 0;
                foreach (DriveInfo dri in hddDrives)
                {
                    drvcnt++;
                    //getting the free space in logical disk through performance counters.
                    PerformanceCounter per = new PerformanceCounter();
                    per.CategoryName = "LogicalDisk";
                    per.CounterName = "% Free Space";
                    per.InstanceName = dri.Name.Substring(0, dri.Name.Length - 1);
                    long totalSize = dri.TotalSize;
                    long totalSpaceOccupied = totalSize - dri.TotalFreeSpace;
                  percentOccupied += Convert.ToInt32(100 - per.NextValue());
                }
               double temp =Convert.ToDouble( percentOccupied) / Convert.ToDouble(drvcnt * 100);
               percentOccupied = Convert.ToInt32(temp*100);

               EventHandler<Event_Meter> meter_r = SystemMeter_Event;
                if(meter_r!=null)
                {
                    this.SystemMeter_Event(this,new Event_Meter(_cpuUnit,_ramUnit,percentOccupied));
                }
            }
            catch { }
        }

        //Method which takes Management Object and returns a Int64 value of total Physical Memory.
        private Int64 GetTotalRamUsage(ManagementObject m)
        {
            return Convert.ToInt64(m["TotalVisibleMemorySize"]);
        }
        //Method which takes Management Object and returns a Int64 value of Free Physical Memory.
        private Int64 GetFreePhysicalMemory(ManagementObject m)
        {
            return Convert.ToInt64(m["FreePhysicalMemory"]);
        }
        //Uses WMI (Windows Management Instrumentation) to Initialize Variables for System Meter.
        private void GetPhysicalMemory()
        {
            ManagementObjectSearcher mgtObj = new ManagementObjectSearcher("root\\CIMV2", "Select * from Win32_OPeratingSystem");
            foreach (ManagementObject obj in mgtObj.Get())
            {
                _totalRamMemory = GetTotalRamUsage(obj);
                _freePhysicalMemory = GetFreePhysicalMemory(obj);
            }
        }
    }

    class Event_Meter : EventArgs
    {
        #region Propertise
        int processor = 0,ram=0,disk=0;
        public int ProcessorUsage
        {
            get
            {
                return processor;
            }
        }
        public int RamUsage
        {
            get
            {
                return ram;
            }
        }
        public int DiskUsage
        {
            get { return disk; }
        }
        #endregion

        public Event_Meter(int cpu, int ramp, int diskp)
        {
            processor = cpu;
            ram = ramp;
            disk = diskp;
        }
    }
}
