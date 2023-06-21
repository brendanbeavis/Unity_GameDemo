using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dudes
{
    internal class DudeHealth
    {
        private float health = 100f;
        private readonly float healthMax = 100f;
        private readonly float healthRegenRate = 0f; //added HP per sec
        public bool IsAlive { get; set; } = true;

        public DudeHealth() { }
        public DudeHealth(float max, float regen)
        {
            healthMax = max;
            healthRegenRate = regen;
        }

        public float Health
        {
            get { return health; }
        }
        

        public bool Hurt(float dmg)
        {
            health -= dmg;
            if (health <= 0)
            {
                IsAlive = false;
                health = 0;
            }
            return IsAlive;
        }
        public void Heal(float hp)
        {
            health += hp;
            if (health > GetMaxHealth())
            {
                health = GetMaxHealth();
            }
        }

        public float GetHealthPercent()
        {
            return health/ healthMax * 100;
        }
        public float GetMaxHealth()
        {
            //TODO add a way to buff health here?
            return healthMax;
        }

        public void RegenHP()
        {
            //TODO add a way to alter regen rate
            Heal(healthRegenRate);

        }

        //TODO add a way to increase max health.
    }
}
