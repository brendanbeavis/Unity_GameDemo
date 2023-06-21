using Assets.Scripts.Dudes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Assets.Scripts.UI
{
    public class DrawGameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text Text_Health;
        [SerializeField] private TMP_Text Text_Shield;
        [SerializeField] private TMP_Text Text_XP;
        [SerializeField] private TMP_Text Text_Level;
        [SerializeField] private TMP_Text Text_Super;
        [SerializeField] private TMP_Text Text_Time;
        [SerializeField] private TMP_Text Text_Died;
        [SerializeField] private DudeData dude;

        private float gameTime = 0.0f;

        void Update()
        {
            if (dude.IsAlive())
            {
                gameTime += Time.deltaTime;
                int minutes = Mathf.FloorToInt(gameTime / 60F);
                int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
                Text_Time.text = string.Format("Time: {0:0}:{1:00}", minutes, seconds);
                Text_Super.text = "Special: " + (int)dude.GetSpecial();
                Text_Health.text = "Health: " + (int)dude.GetHealth();
                Text_Shield.text = "Shield: " + dude.GetShield();
                Text_XP.text = "XP: " + (int)dude.GetXP() + " %" + (int)dude.GetLevelProgress();
                Text_Level.text = "Level: " + dude.GetLevel();
            }
            else
            {
                Text_Died.text = "YOU DIED\n\nPress SPACE to restart...";
            }
        }
    }
}