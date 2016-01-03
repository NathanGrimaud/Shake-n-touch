using ShakeNTouch.Model;
using ShakeNTouch.ViewModel.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Phone.Devices.Notification;
using Windows.UI.Xaml;

namespace ShakeNTouch.ViewModel
{
    class PartieViewModel : ViewModelBase
    {
        #region Propriété


        private Thickness _MarginRect;
        public Thickness MarginRect
        {
            get { return _MarginRect; }
            set { NotifyPropertyChanged(ref _MarginRect, value); }
        }

        private string _Action;
        public string Action
        {
            get { return _Action; }
            set { NotifyPropertyChanged(ref _Action, value); }
        }
        
        private Visibility _VisibilityRect;
        public Visibility VisibilityRect
        {
            get { return _VisibilityRect; }
            set { NotifyPropertyChanged(ref _VisibilityRect, value); }
        }

        private Visibility _GoVisibility;
        public Visibility GoVisibility
        {
            get { return _GoVisibility; }
            set { NotifyPropertyChanged(ref _GoVisibility, value); }
        }

        private string _Score;
        public string Score
        {
            get { return _Score; }
            set { NotifyPropertyChanged(ref _Score, value); }
        }


        public Accelerometre accelero;
        Partie partieencours;
        Tour tourencours;


        #endregion

        #region Constructeur

        public PartieViewModel()
        {

            VisibilityRect = Visibility.Collapsed;
            partieencours = new Partie();
            this.GoCommand = new RelayCommand(tour);
            this.ValidCommand = new RelayCommand(Valid);
        }     

        #endregion

        #region Command

        public ICommand GoCommand { get; private set; }
        public ICommand ValidCommand { get; private set; }

        #endregion



        #region Private Methode

        // Un tour correspond à un push du go. La partie elle ne fait que comptabilisé les score
        private void tour(){

            GoVisibility = Visibility.Collapsed;
            TourService tourneur = new TourService();
            tourencours = tourneur.newTour();
            DateTime finshake = DateTime.Now.AddMilliseconds(tourencours.time);
            this.Score = "Score :" + partieencours.score;

            // Ici faire vibrer le vibreur jusqu'a finshake

            VibrationDevice viber = VibrationDevice.GetDefault();
            viber.Vibrate(TimeSpan.FromMilliseconds(tourencours.time));

            // Ici, il est demander à l'utilisateur de shake le téléphone jusqu'a finshake. Si l'utilisateur à bien réagi, il se retrouve dans la fonction continuerpartie(). Sinon finpartie()
            shaker(finshake);
            // La suite se trouve dans finpartie() ou continuerpartie()          

        }

        #region Verification du shake


        public void shaker(DateTime finshake)
        {
            bool ok = true;
            accelero = new Accelerometre();
            this.Action = "Shake !";
            var messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler<object>((s, e) => messageTimer_Tick(s, e, ref ok, finshake));
            messageTimer.Interval = TimeSpan.FromMilliseconds(500);
            messageTimer.Start();


            // messageTimer.Stop();

        }


        private void messageTimer_Tick(object sender, object e, ref bool ok, DateTime finshake)
        {
            // Je vérifie que l'accélérometre a bien bouger

            if (DateTime.Now < finshake && ok)
            {
                if (accelero.x == accelero.precx && accelero.y == accelero.precy && accelero.z == accelero.precz)
                    ok = false;

                accelero.precx = accelero.x;
                accelero.precy = accelero.y;
                accelero.precz = accelero.z;
                if (finshake.Second - DateTime.Now.Second < 0)
                { }
                this.Action = "Shake encore" + (finshake - DateTime.Now).Seconds + "secondes";
            }

            else if (!ok)
            {
                Finpartie();
                var send = (DispatcherTimer)sender;
                send.Stop();
            }
            else if (DateTime.Now > finshake)
            {
                var send = (DispatcherTimer)sender;
                send.Stop();
                ContinuerPartie();
            }

        }

        #endregion

        // S'éxecute sur l'utilisateur a correctement passé l'étape de l'accéléromètre
        private void ContinuerPartie()
        {
            this.Action = "Tap sur le bouton ! vite !!";
            VisibilityRect = Visibility.Visible;
            MarginRect = new Thickness()
            {
                Top = tourencours.X,
                Left = tourencours.Y,
                
            };
            // Intervalle de temps que possède le joueur pour taper sur le bouton !! 
            var messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler<object>((s, e) => hideRect(s, e));
            messageTimer.Interval = TimeSpan.FromMilliseconds(tourencours.timeshow);
            messageTimer.Start();
           
        }

        private void Valid()  // Validation du tour
        {

            // Hide rectangle
            VisibilityRect = Visibility.Collapsed;

            // Validation du tour
            tourencours.valide = true;

            //Augementation du score
            partieencours.score++;

            // Mise a jour de la vue
            this.Action = "Bravo !! + 1 points ! ";
            this.Score = "Score :" + partieencours.score;

            // Posibilité de recommencer 
            GoVisibility = Visibility.Visible;
        }


        // S'éxecute après le temps d'affichage maximum du rectangle 
        private void hideRect(object sender, object e)
        {
            var send = (DispatcherTimer)sender;
            send.Stop();
            if (!tourencours.valide)
            {
                Finpartie();  
            }
        }

        private void Finpartie()  // S'éxecute quand l'utilisateur à perdu ! 
        {
            VisibilityRect = Visibility.Collapsed;
            this.Action = "Perdu :/";
            partieencours.termine = true;
        }

      

        #endregion
    }
}
