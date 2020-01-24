using System;
using System.Diagnostics;

namespace MotorolaRadio
{
    // https://developers.redhat.com/blog/2019/09/06/interacting-with-native-libraries-in-net-core-3-0/
    public class DeviceLibrary
    {
        private bool isEnabled;

        public void Init()
        {
            isEnabled = true;
        }

        public bool GetStatus()
        {
            return isEnabled;
        }

        public void Call(string from, string to)
        {
            if (isEnabled)
            {
                Trace.WriteLine($"Calling from {from} to {to}");
            }
        }

        public void Release()
        {
            isEnabled = false;
        }
    }
}
