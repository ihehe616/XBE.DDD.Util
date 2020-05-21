using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CyUSB;


namespace XBE.DDD.Util
{
    public class HookDemo
    {
        private USBDeviceList usbDevices;
        private USBDevice device;

        public delegate int OnUsbDeviceChanges(string alias, bool remove);

        public OnUsbDeviceChanges DeviceChanged;
        public int Init(OnUsbDeviceChanges deviceChangesCallBack)
        {
            try
            {
                DeviceChanged = deviceChangesCallBack;
                usbDevices = new USBDeviceList(CyConst.DEVICES_CYUSB | CyConst.DEVICES_HID | CyConst.DEVICES_MSC);
                //usbDevices = new USBDeviceList(0xFF);
                usbDevices.DeviceAttached += new EventHandler(usbDevices_DeviceAttached);
                usbDevices.DeviceRemoved += new EventHandler(usbDevices_DeviceRemoved);
            }
            catch (Exception e)
            {
                return -1;
            }

            return 0;
        }

        private void usbDevices_DeviceRemoved(object sender, EventArgs e)
        {
            USBEventArgs usbEvent = e as USBEventArgs;
            DeviceChanged?.Invoke(usbEvent.FriendlyName, true);
            //toolStripStatusLabel1.Text = usbEvent.FriendlyName + " removed.";
        }

        private void usbDevices_DeviceAttached(object sender, EventArgs e)
        {
            USBEventArgs usbEvent = e as USBEventArgs;
            DeviceChanged?.Invoke(usbEvent.FriendlyName, false);
            //toolStripStatusLabel1.Text = usbEvent.Device.FriendlyName + " connected.";

        }
    }
}
