using Assets.Scripts.Dudes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.UI
{
    public class ScreenMoveShake : MonoBehaviour
    {
        //the player we want to follow
        [SerializeField] private DudeData dude;

        // Transform of the GameObject you want to shake
        new private Transform transform;

        // Desired duration of the shake effect
        private float shakeDuration = 0f;

        // A measure of magnitude for the shake. Tweak based on your preference
        private readonly float shakeMagnitude = 0.05f;

        // A measure of how quickly the shake effect should evaporate
        private readonly float dampingSpeed = 3.0f;

        // The initial position of the GameObject
        Vector3 initialPosition;

        private readonly float standardCameraOffset = -10f;

        void Awake()
        {
            if (transform == null)
            {
                transform = GetComponent(typeof(Transform)) as Transform;
            }
        }

        void OnEnable()
        {
            initialPosition = transform.localPosition;
            DudeData.OnDudeInjured += TriggerShake;
        }
        void OnDisable()
        {
            DudeData.OnDudeInjured -= TriggerShake;
        }

        void FixedUpdate()
        {
            if (shakeDuration > 0)
            {

                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                initialPosition = transform.localPosition;
                transform.localPosition = dude.position + new Vector3(0, 0, standardCameraOffset);
            }

        }
        public void TriggerShake(float amt)
        {
            shakeDuration = 0.4f;
        }
    }
}