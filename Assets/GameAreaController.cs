using Assets.Scripts.Dudes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaController : MonoBehaviour
{

    //our dude data object
    [SerializeField]
    private Dude dude;

    [SerializeField] private float gameAreaX = 0;
    [SerializeField] private float gameAreaY = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dude"))
        {
            float x = dude.transform.position.x;
            float y = dude.transform.position.y;

            if (x < 0)
            {
                dude.transform.position += new Vector3(gameAreaX, 0, 0);
            }
            else if (x > 0)
            {
                dude.transform.position -= new Vector3(gameAreaX, 0, 0);
            }

            if (y < 0)
            {
                dude.transform.position += new Vector3(0, gameAreaY, 0);
            }
            else if (y > 0)
            {
                dude.transform.position -= new Vector3(0, gameAreaY, 0);
            }


        }
    }

}
