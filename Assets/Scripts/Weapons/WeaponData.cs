using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [CreateAssetMenu]
    public class WeaponData : ScriptableObject
    {
        public GameObject model;
        public Sprite icon;
        string weaponName;
        string description;
        public float weaponSpeed = 5f; //travelSpeed
        float weaponDamage = 5f; //damage per hit
        int weaponHits = 1; //hits the weapon takes before death
        int weaponLife = 10; //seconds the weapon will exist before death
        int weaponLevel = 1;

        public WeaponData() { }
        public WeaponData(float _weaponSpeed, float _weaponDamage, int _weaponHits, int _weaponLife, int _weaponLevel)
        {
            weaponSpeed = _weaponSpeed;
            weaponDamage = _weaponDamage;
            weaponHits = _weaponHits;
            weaponLife = _weaponLife;
            weaponLevel = _weaponLevel;
        }

    }
}
