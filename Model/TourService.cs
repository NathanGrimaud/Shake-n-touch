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
            Tour encours = new Tour(){
                X = alea.Next(0, 200),
                Y = alea.Next(0, 200),
                time = alea.Next(0, 1000)
            };

            return encours;
        }
    }
}
