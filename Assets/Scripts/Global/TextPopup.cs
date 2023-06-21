using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// TextPopup can be used to show a text value for a short period of time, such as when the player incurs damage and loses HP.
/// </summary>

namespace Assets.Scripts.Global
{
    public class TextPopup : MonoBehaviour
    {
        private readonly float moveInt = 1f;
        private readonly float moveSpeed = 2f;
        private readonly float minShowTime = 0.25f;
        private Vector3 MoveDir;


        private void Start()
        {
            MoveDir = transform.up;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + MoveDir, moveInt * (moveSpeed * Time.deltaTime));
        }

        //this does the grunt work of showing, colouring, and moving, the popup text.
        public void SetTextAndMove(string textStr, Color colour, float age = 1f)
        {
            TMP_Text myText = GetComponentInChildren<TMP_Text>();
            myText.text = textStr;
            myText.color = colour;
            StartCoroutine(Timer(myText, age));
        }
        private IEnumerator Timer(TMP_Text myText, float age)
        {
            yield return new WaitForSeconds(minShowTime);
            myText.CrossFadeAlpha(0, age, true);
            Destroy(gameObject, age);
        }
    }
}