using MotorolaRadio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radio.Infrastructure
{
    public interface IRadio : IDisposable
    {
        void Call(User sender, User recipient);
        bool IsOn { get; }
        void SetVolume(int value);
    }

    public class MotorolaRadioAdapter : IRadio
    {
        // Adaptee
        private DeviceLibrary device;

        public MotorolaRadioAdapter()
        {
            device = new DeviceLibrary();

            device.Init();
        }

        public bool IsOn => device.GetStatus();

        public void Call(User sender, User recipient)
        {
            device.Call(sender.Name, recipient.Name);
        }

        public void Dispose()
        {
            device.Release();
        }

        public void SetVolume(int value)
        {
            throw new NotSupportedException();
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }

}
