using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.Devices.Notification;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ShakeNTouch.Model
{
    class Accelerometre
    {
        public double x;
        public double y;
        public double z;

        public double precx = 0;
        public double precy = 0;
        public double precz = 0;

        public Accelerometre()
        {
            accel();
        }

        public void accel()
        {
            var acc = Accelerometer.GetDefault();
            acc.ReadingChanged += Acc_ReadingChanged; ;

        }
        async private void Acc_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {

            await View.Partie.DispatcherPartie.RunAsync(CoreDispatcherPriority.Normal, () =>
            {              

                x = Math.Round(args.Reading.AccelerationX, 5);
                y = Math.Round(args.Reading.AccelerationY, 5);
                z = Math.Round(args.Reading.AccelerationZ, 5);

            
            });
        }



    }
}
