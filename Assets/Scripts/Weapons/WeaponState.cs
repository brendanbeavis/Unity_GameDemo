using Assets.Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class WeaponState : MonoBehaviour
    {

        private float weaponDamage = 5f; //damage per hit
        private int weaponHits = 1; //hits the weapon takes before death
        private float weaponLifeSecs = 10f; //seconds the weapon will exist before death


        private void Start()
        {
            //if there is an age limit on the weapon, destroy it
            if (weaponLifeSecs > 0)
            {
                Destroy(gameObject, weaponLifeSecs);
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            //weapon has hit an enemy?
            if (collision.CompareTag("Enemy") && weaponHits > 0)
            {
                //enemy takes damage
                EnemyData enemy = collision.GameObject().GetComponent<EnemyData>();
                if (enemy) enemy.TakeDamage(weaponDamage);

                //check if the weapon has run out of steam...
                weaponHits--;
                if (weaponHits <= 0)
                {
                    Die();
                }
            }
        }

        //kill the weapon, but first we hide it, wait 2 secs for any sounds to complete, then destroy
        private void Die()
        {
            transform.localScale -= new Vector3(0.2f, 0.2f, 0);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 2f);
        }

    }
}