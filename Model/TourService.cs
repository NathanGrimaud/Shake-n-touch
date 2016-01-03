using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeNTouch.Model
{
    class TourService
    {
        public Random alea = new Random();
        public Tour newTour()
        {
            Tour encours = new Tour() {
                X = alea.Next(0, 490),
                Y = alea.Next(0, 283),
                time = alea.Next(500,5000),//temps de vibration
                timeshow = alea.Next(1000, 3000)//temps pour appuyer
            };

            return encours;
        }


    }
}
