using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

namespace Ru1t3rl.Audio
{
    [RequireComponent(typeof(StudioParameterTrigger)),
     RequireComponent(typeof(StudioEventEmitter))]
    public class AudioSource : MonoBehaviour
    {
        [Header("Trigger Settings")]
        [SerializeField] EmitterRef emitterRef = null;
        [SerializeField] EmitterGameEvent triggerEvent;

        [Header("Event Settings")]
        [SerializeField] EmitterGameEvent playEvent;
        [SerializeField] EmitterGameEvent stopEvent;
        [SerializeField] EventReference eventRef;
        [SerializeField] ParamRef[] paramRefs;

        public StudioParameterTrigger parameterTrigger { get; private set; }
        public StudioEventEmitter eventEmitter { get; private set; }

        void OnValidate()
        {
            #region Null Check
            if (!eventEmitter)
            {
                eventEmitter = GetComponent<StudioEventEmitter>();

                if (!eventEmitter)
                {
                    eventEmitter = gameObject.AddComponent<StudioEventEmitter>();
                }
            }

            if (!parameterTrigger)
            {
                parameterTrigger = GetComponent<StudioParameterTrigger>();

                if (!parameterTrigger)
                {
                    parameterTrigger = gameObject.AddComponent<StudioParameterTrigger>();
                }
            }
            #endregion

            eventEmitter.hideFlags = HideFlags.HideInInspector;
            parameterTrigger.hideFlags = HideFlags.HideInInspector;

            if (emitterRef == null)
            {
                emitterRef = new EmitterRef();
            }

            emitterRef.Target = eventEmitter;

            parameterTrigger.Emitters = new EmitterRef[1] { emitterRef };
            parameterTrigger.TriggerEvent = triggerEvent;

            eventEmitter.PlayEvent = playEvent;
            eventEmitter.StopEvent = stopEvent;
            eventEmitter.EventReference = eventRef;

            eventEmitter.Params = paramRefs;
        }

        public void Play()
        {
            eventEmitter.Play();
        }

        public static explicit operator StudioEventEmitter(AudioSource source) => source.eventEmitter;
        public static explicit operator StudioParameterTrigger(AudioSource source) => source.parameterTrigger;
    }
}