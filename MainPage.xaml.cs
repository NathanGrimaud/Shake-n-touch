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

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=391641

namespace ShakeNTouch
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Viber_button.Click += Viber_button_Click;
            accel();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        public void accel()
        {
            var acc = Accelerometer.GetDefault();
            acc.ReadingChanged += Acc_ReadingChanged; ;
            
        }
        public bool shake()
        {

        }
        async private void Acc_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            var precx;
            var precy;
            var precz;

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var x = Math.Round(args.Reading.AccelerationX, 5);
                var y = Math.Round(args.Reading.AccelerationY, 5);
                var z = Math.Round(args.Reading.AccelerationZ, 5);
                
                xstate.Text = String.Format("{0,5:0.0000}", x);
                ystate.Text = String.Format("{0,5:0.0000}", y);
                zstate.Text = String.Format("{0,5:0.0000}", z);
            });
        }

        private void Viber_button_Click(object sender, RoutedEventArgs e)
        {
            VibrationDevice testVibrationDevice = VibrationDevice.GetDefault();
            testVibrationDevice.Vibrate(TimeSpan.FromMilliseconds(500));             
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: préparer la page pour affichage ici.

            // TODO: si votre application comporte plusieurs pages, assurez-vous que vous
            // gérez le bouton Retour physique en vous inscrivant à l’événement
            // Événement Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Si vous utilisez le NavigationHelper fourni par certains modèles,
            // cet événement est géré automatiquement.
        }
    }
}
