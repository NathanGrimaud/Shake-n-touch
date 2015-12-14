using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeNTouch.Model
{
    class ServicePartie
    {
        
        public Partie newpartie()
        {
            return new Partie()
            {
                score = 0,
                termine = false
            };
        }

      

    }
}
