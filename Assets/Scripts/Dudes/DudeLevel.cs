using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dudes
{
    internal class DudeLevel
    {
        private float XPForLasttLevel = 0;
        private float XPForNextLevel = 10;
        private float XPIteration = 10;
        private float levelUpMult = 1.3f;

        public int level { get; set; } = 1;
        public float xp { get; set; } = 0;

        public bool AddXP(float xpValue, List<float> xpBuffs)
        {            
            xp += xpValue * BuffMultiplier(xpBuffs);
            if (xp > XPForNextLevel)
            {
                level++;
                XPForLasttLevel = XPForNextLevel;
                XPIteration = XPIteration * levelUpMult;
                XPForNextLevel += XPIteration;
                return true;
            }
            return false;
        }

        public int LevelProgress()
        {
            //returns our progress to the next level as a percentage integer.
            // if we are at level 3,
            //      and we achieved level three at 40XP, 
            //      and level 4 commences at 100XP
            //      then we should return 0% when at 50XP, 50% at 75XP and 100% at 100XP
            return (int)((xp - XPForLasttLevel) / (XPForNextLevel - XPForLasttLevel) * 100);
        }

        private float BuffMultiplier(List<float> xpBuffs)
        {
            float mult=1.0f; //we must have a base multiplier of 1
            for (int i = 0; i < xpBuffs.Count; i++)
            {
                mult = mult * xpBuffs[i];
            }
            return mult;
        }
    }
}
