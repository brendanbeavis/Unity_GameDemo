using Assets.Scripts.Dudes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Enemy
{
    public class EnemyMover : MonoBehaviour
    {

        [SerializeField] private DudeData dudeData;
        [SerializeField] private float moveSpeed = 1f;
        private Dude dude;
        private Rigidbody2D enemyBody;
        private float dudeCloseSpeedMultiplier = 0.1f;

        CircleCollider2D enemyCollider;
        CircleCollider2D dudeCollider;



        private void Start()
        {
            dude = FindObjectOfType<Dude>();
            enemyBody = GetComponent<Rigidbody2D>();
            enemyCollider = GetComponent<CircleCollider2D>();
            dudeCollider = dude.GetComponent<CircleCollider2D>();
        }

        void FixedUpdate()
        {
            MoveEnemy();

        }

        void MoveEnemy()
        {
            if (dudeData.IsAlive())
            {
                float thisMoveSpeed = moveSpeed;

                //calcuate where the enemy should go toward the dude
                Vector2 direction = dudeData.position - enemyBody.transform.position;

                //determine how far the enemy is from the dude, if we are too close we slow down.        
                float distance = Vector2.Distance(dudeData.position, enemyBody.transform.position);

                //get the size of the collider of the two objects, so we know the "minimum possible distance" that can be achieved between them.
                float rad1 = enemyCollider.radius;
                float rad2 = dudeCollider.radius;

                //alter the moveSpeed multiplier if the enemy is touching the dude
                if (distance < (rad1 + rad2) * 1.1f) { thisMoveSpeed = thisMoveSpeed * dudeCloseSpeedMultiplier; }

                //move the enemy
                enemyBody.velocity = direction.normalized * thisMoveSpeed;


                //rotate our enemyBody toward dude
                Vector2 lookDir = enemyBody.transform.position - dudeData.position;
                float angle = (Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg) + 90f;
                enemyBody.rotation = angle;
            }
            else
            {
                enemyBody.velocity = new Vector2(0, 0);
            }
        }

    }
}
