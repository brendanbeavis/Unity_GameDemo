using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dudes
{
    internal class DudeShield
    {
        private float maxShield = 100f;
        private float shield = 0f;

        public float Shield
        {
            get { return shield; }
            set { shield = value; }
        }


        //Shield will take whatever damage it can, and return the remainder to the dude to incur from their health
        public float Hurt(float dmg)
        {
            if (shield > 0f)
            {
                shield -= dmg;
                if (shield < 0)
                {
                    float remainder = shield * -1;
                    shield = 0;
                    return remainder;
                }
            }
            else
            {
                return dmg;
            }
            return 0;
        }

        //increase shields current health
        public void Heal(float hp)
        {
            shield += hp;
            if (shield > GetMaxShield())
            {
                shield = GetMaxShield();
            }
        }

        private float GetMaxShield()
        {
            //TODO add a way to buff Shield here.
            return maxShield;
        }

    }
}
