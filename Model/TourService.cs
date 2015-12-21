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
                time = alea.Next(3000, 30000),
                timeshow = alea.Next(1000, 3000)
            };

            return encours;
        }


    }
}
