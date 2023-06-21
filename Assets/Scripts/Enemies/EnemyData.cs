using Assets.Scripts.Dudes;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Enemy
{
    public class EnemyData : MonoBehaviour
    {
        //Data about this enemy
        [SerializeField] private string enemyName = "";
        [TextArea][SerializeField] private string description = "";
        [SerializeField] private float health = 10f;
        [SerializeField] private float dps = 6f;
        [SerializeField] private float moveSpeed = 2f;

        [SerializeField] private float timePeriod = 1f; //time (seconds) between hits, this is normally 1, because enemy injures as a 'DPS' per sec


        //the token this enemy drops
        [SerializeField] private GameObject tokenPrefab;

        //the text_popup object for when the dude is injured or collects XP.
        [SerializeField]
        private GameObject Text_Popup;

        private float nextActionTime = 0.0f;
        
        //trigger when we collide with the dude so we can hurt them.
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Dude"))
            {
                Dude dude1 = collision.GetComponent<Dude>();
                if (dude1 != null && Time.time > nextActionTime && dude1.IsAlive())
                {
                    dude1.TakeDamage(dps);
                    nextActionTime = Time.time + timePeriod;
                }
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Instantiate(tokenPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}