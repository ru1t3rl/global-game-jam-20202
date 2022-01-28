using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ.Floors
{
    public class FloorManager : MonoBehaviour
    {
        [SerializeField] Floor[] floors;
        [SerializeField] UnityEvent onFinishFloor, onFinishAllFloors;
        int currentFloor = 0;

        void OnEnable()
        {
            for (int i = 0; i < floors.Length; i++)
            {
                floors[i].onFinishFloor.AddListener(ActivateNextFloor);
            }
        }

        void OnDisable()
        {
            for (int i = 0; i < floors.Length; i++)
            {
                floors[i].onFinishFloor.RemoveListener(ActivateNextFloor);
            }
        }

        void ActivateNextFloor()
        {
            onFinishFloor?.Invoke();

            if (currentFloor + 1 >= floors.Length)
            {
                onFinishAllFloors?.Invoke();
            }

            currentFloor++;
            floors[currentFloor].Activate();
        }
    }
}