using Assets.Scripts.Dudes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Assets.Scripts.Dudes
{
    [CreateAssetMenu]
    public class DudeData : ScriptableObject
    {
        private DudeHealth dudeHealth;
        private DudeSpecial dudeSpecial;
        private readonly DudeLevel dudeLevel;
        private readonly DudeShield dudeShield;

        public Vector3 position { get; set; } = Vector3.zero;

        
        public delegate void DudeInjuredDelegate(float amt);
        public static event DudeInjuredDelegate OnDudeInjured;
        public delegate void DudeDiedDelegate();
        public static event DudeDiedDelegate OnDudeDied;        
        public delegate void DudeXPDelegate();
        public static event DudeXPDelegate OnDudeGetXP;
        public delegate void DudeUseSpecialDelegate();
        public static event DudeUseSpecialDelegate OnDudeUseSpecial;

        public DudeData()
        {
            dudeLevel = new DudeLevel();
            dudeHealth = new DudeHealth();
            dudeShield = new DudeShield();
            dudeSpecial = new DudeSpecial();
        }
        public void Init(float healthMax, float healthRegen, float specialMax, float specialRegen)
        {
            dudeHealth = new DudeHealth(healthMax, healthRegen);
            dudeSpecial = new DudeSpecial(specialMax, specialRegen);
        }

        public bool IsAlive()
        {
            return dudeHealth.IsAlive;
        }

        public void TakeDamage(float amt)
        {
            float remainingDamage = dudeShield.Hurt(amt);
            dudeHealth.Hurt(remainingDamage);
            if (OnDudeInjured != null)
            {
                OnDudeInjured(amt);
            }
            if (!IsAlive() && OnDudeDied != null)
            {
                OnDudeDied();
            }
        }

        public void Heal(float amt)
        {
            dudeHealth.Heal(amt);
        }
        public void HealShield(float amt)
        {
            dudeShield.Heal(amt);
        }

        public float GetHealth()
        {
            return dudeHealth.Health;
        }
        public float GetShield()
        {
            return dudeShield.Shield;
        }

        public float GetHealthPercent()
        {
            return dudeHealth.GetHealthPercent();
        }

        public void AddXP(float xpValue)
        {
            if (dudeLevel.AddXP(xpValue, GetXPBuffs())) //true if we've level'd up, false if we didn't.
            {
                //TODO show level up screen?
            }
            if (dudeHealth.IsAlive && OnDudeGetXP != null)
            {
                OnDudeGetXP();
            }
        }

        public int GetLevel()
        {
            return dudeLevel.level;
        }
        public float GetXP()
        {
            return dudeLevel.xp;
        }
        public float GetLevelProgress()
        {
            return dudeLevel.LevelProgress();
        }

        private List<float> GetXPBuffs()
        {
            //TODO make and manage the bufflist properly
            List<float> bufflist = new List<float>();
            return bufflist;
        }
        public float GetSpecial()
        {
            return dudeSpecial.Special;
        }
        public bool UseSpecial(float time)
        {
            if(dudeSpecial.Use(time))
            {
                if (OnDudeUseSpecial != null)
                {
                    OnDudeUseSpecial();
                }
                return true;
            }
            return false;
        }

        public void TriggerRegen()
        {
            dudeSpecial.RegenSP();
            dudeHealth.RegenHP();
        }
    }
}