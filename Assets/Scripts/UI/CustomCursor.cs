using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CustomCursor : MonoBehaviour
    {
        [SerializeField] private Transform mCursorVisual;
        private Vector3 mDisplacement;
        void Start()
        {
            // this sets the base cursor as invisible
            Cursor.visible = false;
        }

        void Update()
        {
            mCursorVisual.position = Input.mousePosition + mDisplacement;
        }

        void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                Debug.Log("Application is focussed");
            }
            else
            {
                Debug.Log("Application lost focus");
            }
        }
    }
}