using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dudes
{
    internal class DudeSpecial
    {
        private float special = 100f;
        private readonly float specialMax = 100f;
        private readonly float specialRegenRate = 1f; //added SP per sec
        private readonly float specialDrainRate = 20f; //consumed SP per sec.
        private bool coolDownActive = false;

        public DudeSpecial() { }
        public DudeSpecial(float max, float regen)
        {
            specialMax = max;
            specialRegenRate = regen;
        }

        public float Special
        {
            get { return special; }
        }

        public float GetMaxSpecial()
        {
            //TODO add special buffs here
            return specialMax;
        }

        public void AddSP(float sp)
        {
            special += sp;
            if (special > GetMaxSpecial())
            {
                special = GetMaxSpecial();
            }
        }

        public void RegenSP()
        {
            if (coolDownActive)
            {
                coolDownActive = false;
            }
            else
            {
                //TODO add a way to alter regen rate?
                AddSP(specialRegenRate);
            }
        }

        public bool Use(float time)
        {
            if (special > 0)
            {
                special -= specialDrainRate * time;
                if (special <= 0) {
                    //we have run out of special, set cool down to true
                    coolDownActive = true;
                }
                return true;
            }
            return false;
        }
    }
}