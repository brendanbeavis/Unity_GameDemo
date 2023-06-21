using Assets.Scripts.Dudes;
using Assets.Scripts.Pickups;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


namespace Assets.Scripts.Pickups
{
    public class TokenState : MonoBehaviour
    {
        private float moveSpeed = 2f;
        private Rigidbody2D tokenBody;
        private CircleCollider2D tokenColliderMover;
        private BoxCollider2D tokenColliderCollected;

        private Assets.Scripts.Pickups.Token token = new Assets.Scripts.Pickups.Token();
        [SerializeField] private DudeData dude;

        void Start()
        {
            tokenBody = GetComponent<Rigidbody2D>();
            tokenColliderMover = GetComponent<CircleCollider2D>();
            tokenColliderCollected = GetComponent<BoxCollider2D>();
            tokenColliderCollected.enabled = false;
        }

        void FixedUpdate()
        {
            MoveToken();

        }

        void MoveToken()
        {
            if (dude.IsAlive() && token.moveToDude)
            {

                //calcuate where the Token should go toward the dude
                Vector2 direction = dude.position - tokenBody.transform.position;

                //determine how far the Token is from the dude, if we are  close we speed up.        
                float distance = Vector2.Distance(dude.position, tokenBody.transform.position);
                float thisMoveSpeed = moveSpeed + distance;

                //move the Token
                tokenBody.velocity = direction.normalized * thisMoveSpeed;
            }
            else
            {
                //stop movement, dude is dead
                tokenBody.velocity = new Vector2(0, 0);
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.tag.Equals("Dude"))
            {
                token.moveToDude = true;
                tokenColliderCollected.enabled = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag.Equals("Dude") && tokenColliderCollected.enabled && token.alive)
            {
                switch (token.tokenType)
                {
                    case eTokenType.xp:
                        StartCoroutine(PayDudeAndKillSelf());
                        break;
                    case eTokenType.health:

                        break;
                    case eTokenType.pickup:

                        break;
                    case eTokenType.mana:

                        break;
                }

            }
        }
        private void OnTriggerExit2D(Collider2D collider)
        {

            if (collider.tag.Equals("Dude") && token.alive)
            {
                token.moveToDude = false;
                tokenColliderCollected.enabled = false;
            }
        }

        IEnumerator PayDudeAndKillSelf()
        {
            transform.localScale -= new Vector3(0.3f, 0.3f, 0);
            token.alive = false;
            moveSpeed = 5f;
            tokenColliderCollected.enabled = false;
            tokenColliderMover.enabled = true;
            dude.AddXP(token.xpValue);
            yield return new WaitForSecondsRealtime(0.5f);
            Destroy(gameObject);
        }

    }
}