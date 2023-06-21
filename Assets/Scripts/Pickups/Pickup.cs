using Assets.Scripts.Dudes;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Timeline;

namespace Assets.Scripts.Pickups
{
    public class Pickup : MonoBehaviour
    {
        private Rigidbody2D pickupBody;
        private CircleCollider2D pickupColliderMover;
        private BoxCollider2D pickupColliderCollected;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private DudeData dude;
        [SerializeField] private PickupData pd;

        private void Start()
        {
            pickupBody = GetComponent<Rigidbody2D>();
            pickupColliderMover = GetComponent<CircleCollider2D>();
            pickupColliderCollected = GetComponent<BoxCollider2D>();
            spriteRenderer.sprite = pd.Icon;
        }

        private void Update()
        {
            if (pd.Expired)
            {
                KillSelf();
            }
        }

        private void FixedUpdate()
        {
            MovePickup();
        }

        //If the pickup is within range of the Dude, we enable the 'pickup-is-collectable' collider.
        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.CompareTag("Dude"))
            {
                if (pd.MoveType != MoveType.NONE) {
                    pd.MoveToDude = true;
                }
            }
        }


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Dude") && !pd.Expired && pickupColliderCollected.IsTouching(collider))
            {
                switch (pd.PickupType)
                {
                    case PickupType.XP:
                    case PickupType.HP:
                    case PickupType.SHIELD:
                        ShrinkAndMagnetToDude();
                        UsePickupAndKillSelf();
                        break;
                    default:
                        Debug.Log("OnTriggerEnter2D doesn't know what to do with this pickup.");
                        KillSelf();
                        break;
                }
            }
        }

        //if the dude wanders away from the pickup, we "disabled" the 'pickup-is-collectable' collider.
        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("Dude") && !pd.Expired)
            {
                pd.MoveToDude = false;
                pickupColliderCollected.enabled = false;
            }
        }

        //Move the pickup (toward the dude)
        private void MovePickup()
        {
            if (dude.IsAlive() && (pd.MoveToDude || pd.MoveType == MoveType.MOVE))
            {
                //calcuate where the Pickup should go toward the dude
                Vector2 direction = dude.position - pickupBody.transform.position;

                //determine how far the Pickup is from the dude, if we are  close we speed up.        
                float distance = Vector2.Distance(dude.position, pickupBody.transform.position);

                //move the Pickup
                pickupBody.velocity = direction.normalized * (pd.MoveSpeed + distance);
            }
            else
            {
                //stop movement, dude is dead, or the Pickup isn't supposed to move anymore
                pickupBody.velocity = new Vector2(0, 0);
            }
        }

        private void ShrinkAndMagnetToDude()
        {
            transform.localScale -= new Vector3(0.3f, 0.3f, 0);
            pd.MoveSpeed = 6f;
            pickupColliderCollected.enabled = false;
        }

        private void UsePickupAndKillSelf()
        {
            if (pd.Usable && dude.GetLevel() >= pd.MinDudeLevel)
            {
                switch (pd.PickupType)
                {
                    case PickupType.XP:
                        dude.AddXP(pd.PickupValue);
                        pd.PickupValue = 0f;
                        pd.Usable = false;
                        pd.Expired = true;
                        break;
                    case PickupType.HP:
                        dude.Heal(pd.PickupValue);
                        pd.PickupValue = 0f;
                        pd.Usable = false;
                        pd.Expired = true;
                        break;
                    case PickupType.SHIELD:
                        dude.HealShield(pd.PickupValue);
                        pd.PickupValue = 0f;
                        pd.Usable = false;
                        pd.Expired = true;
                        break;
                }
                KillSelf(0.5f);
            }
        }

        private void KillSelf(float destroyDelay = 0f)
        {
            Destroy(gameObject, destroyDelay);
        }

    }
}