using Assets.Scripts.Dudes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class WeaponFire : MonoBehaviour
    {
        public GameObject weapon;
        private float travelAngle;
        private Vector2 travelDirection;

        private float lastFireTime = 0.0f;
        private float fireRate = 2f;

        [SerializeField] private DudeData dude;

        private void Update()
        {
            if (Input.GetMouseButton(0) && ((Time.time - lastFireTime) > (1 / fireRate)) && dude.IsAlive())
            {
                lastFireTime = Time.time;
                DetermineTravelPath();
                FireWeapon();
            }

        }
        private void DetermineTravelPath()
        {
            travelDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - dude.position;
            travelAngle = Mathf.Atan2(travelDirection.y, travelDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, travelAngle - 90f);
        }

        private void FireWeapon()
        {
            GameObject firedWeapon = Instantiate(weapon, transform.position, transform.rotation);
            firedWeapon.GetComponent<Rigidbody2D>().velocity = transform.up * 10f;
        }

    }
}