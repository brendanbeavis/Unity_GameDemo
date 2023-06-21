using Assets.Scripts.Dudes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dudes
{
    public class DudeMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private Rigidbody2D dudeBody;
        [SerializeField] private DudeData dudeData;
        private Vector2 moveDirection;

        //point our dude toward the mouse position
        private Vector2 mousePos;

        private void Update()
        {
            ReadUserInput();
        }
        private void FixedUpdate()
        {
            if (dudeData.IsAlive()) // first we check we have a player
            {
                MoveDude();
            }
            else
            {
                dudeBody.velocity = new Vector2(0, 0);
            }
        }

        //Get inputs from the user so we know what to do with the dude.
        void ReadUserInput()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //move the dude
        void MoveDude()
        {
            //move our dude based on the arrow keys
            dudeBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

            //rotate our dude based on mouse cursor
            Vector2 lookDir = mousePos - dudeBody.position;
            float angle = (Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg) - 90f;
            dudeBody.rotation = angle;
        }
    }
}