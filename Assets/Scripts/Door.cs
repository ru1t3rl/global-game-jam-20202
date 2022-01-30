using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GGJ
{
    public class Door : MonoBehaviour
    {
        [SerializeField] bool startClosed = true;
        [SerializeField] float rotationDuration = 2.5f;

        [Tooltip("Awake will overide the open or closed quaternion based on what you specified as start state!")]
        [SerializeField] Quaternion openRotation, closedRotation;

        bool closed;

        void Awake()
        {
            closed = startClosed;
            if (closed)
            {
                closedRotation = transform.rotation;
            }
            else
            {
                openRotation = transform.rotation;
            }
        }

        public void Close()
        {
            closed = true;
            transform.DORotateQuaternion(closedRotation, rotationDuration);
        }

        public void Open()
        {
            closed = false;
            transform.DORotateQuaternion(openRotation, rotationDuration);
        }

        public void Toggle()
        {
            closed = !closed;
            transform.DORotateQuaternion(closed ? closedRotation : openRotation, rotationDuration);
        }

    }
}
