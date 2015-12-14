using ShakeNTouch.Model;
using ShakeNTouch.ViewModel.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ShakeNTouch.ViewModel
{
    class PartieViewModel : ViewModelBase
    {
        #region Propriété
        private string _xposition;
        public string xPosition
        {
            get { return _xposition; }
            set { NotifyPropertyChanged(ref _xposition, value); }
        }

        private string _yvosition;
        public string yPosition
        {
            get { return _yvosition; }
            set { NotifyPropertyChanged(ref _yvosition, value); }
        }

        private string _timevibrate;
        public string TimeVibrate
        {
            get { return _yvosition; }
            set { NotifyPropertyChanged(ref _yvosition, value); }
        }

        #endregion

        #region Constructeur

        public PartieViewModel()
        {

            this.newpartie();
        }


        #endregion


        #region Command


        #endregion

        #region Private Methode
        private void newpartie()
        {
            Partie partieencours = new Partie();
            tour(partieencours);
           
        }

        private void tour(Partie partieencours){

            TourService tourneur = new TourService();

            while (partieencours.termine == false)
            {
                Tour tourencours = tourneur.newTour();
                DateTime finshake = DateTime.Now.AddMilliseconds(tourencours.time);
                // Ici demander au vibreur de vibre pendant : tourencours.time

 

               
                bool tourvalide = false;

                //Je calcul le temps qu'il a pour appuyer
                finshake = finshake.AddMilliseconds(500);

                // ICI placer le rectangle au bon endroit
                while (finshake != DateTime.Now && tourvalide == false)
                {
                    // Ici mettre le rectangle en visible 
                    // Ici mettre la command qui renvoie lorsque le bouton est appuyé
                }

                if (tourvalide == true)
                {
                    tour(partieencours);
                }
                else {
                    partieencours.termine == true;
                }
               

            }
          
    }
        public bool shaker(DateTime finshake)
        {
            bool ok = false;
            while (finshake != DateTime.Now && !ok)
            {
                var messageTimer = new DispatcherTimer();
                messageTimer.Tick += new EventHandler<object>((s, e) => messageTimer_Tick(s, e, ref ok));
                messageTimer.Interval = TimeSpan.FromMilliseconds(500);
                messageTimer.Start();

                bool shake = false; // Ici renvoyer le résultat d'une fonction "shake" qui vérifie si le téléphone est secoué au moins une fois toutes les 500ms
                if (shake == false)
                    ok = true;
            };

            return ok;

        }

        private void messageTimer_Tick(object sender, object e, ref bool ok)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
