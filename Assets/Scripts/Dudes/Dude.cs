using Assets.Scripts.UI;
using Assets.Scripts.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Assets.Scripts.Dudes
{
    public class Dude : MonoBehaviour
    {
        //our dude data object
        [SerializeField]
        private DudeData dudeData;

        //the text_popup object for when the dude is injured or collects XP.
        [SerializeField]
        private GameObject Text_Popup;

        //define some dude stat fundumentals
        [SerializeField]
        private float healthMax;
        [SerializeField]
        private float healthRegen;
        [SerializeField]
        private float specialMax;
        [SerializeField]
        private float specialRegen;

        //we keep track of game time to execute things on a regular basis, such as health regen
        private double gameTime = 0d;
        private readonly KeyCode useSpecialKey = KeyCode.Space;

        private void Start()
        {
            DudeData.OnDudeGetXP += ShowXPMessage;
            DudeData.OnDudeInjured += ShowInjuredMessage;
            dudeData.Init(healthMax, healthRegen, specialMax, specialRegen);
        }
        void Update()
        {
            if (dudeData.IsAlive())
            {
                dudeData.position = transform.position;

                //keep a track of time, run this per second
                gameTime += Time.deltaTime;
                if (gameTime >= 1f)
                {
                    gameTime = gameTime % 1f;
                    dudeData.TriggerRegen();
                }

                if (Input.GetKey(useSpecialKey) && dudeData.UseSpecial(Time.deltaTime))
                {
                    //special usable

                }
            }
        }

        private void ShowXPMessage()
        {
            GameObject newMarker = Instantiate(Text_Popup, dudeData.position, Quaternion.identity);
            newMarker.SetActive(true);
            newMarker.GetComponent<TextPopup>().SetTextAndMove("+XP", UColor.blue, 0.5f);
        }
        private void ShowInjuredMessage(float amt)
        {
            //colour based on remaining health.
            Color color = Color.white;
            if (dudeData.GetHealthPercent() < 25) { color = UColor.red; }
            else if (dudeData.GetHealthPercent() < 50) { color = UColor.orange; }
            else if (dudeData.GetHealthPercent() < 75) { color = UColor.yellow; }

            GameObject newMarker = Instantiate(Text_Popup, dudeData.position, Quaternion.identity);
            newMarker.SetActive(true);
            newMarker.GetComponent<TextPopup>().SetTextAndMove("-" + amt.ToString(), color, 1f);
        }

        public bool IsAlive()
        {
            return dudeData.IsAlive();
        }

        public void TakeDamage(float amt)
        {
            dudeData.TakeDamage(amt);
        }
    }
}