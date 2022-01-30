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
        int currentFloor = -1;

        void Awake()
        {
            if (floors.Length > 0)
                ActivateNextFloor();

            for (int iFloor = 0; iFloor < floors.Length; iFloor++)
            {
                floors[iFloor].onEnterRoom.AddListener(OnEnterRoom);
            }

        }


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

        void OnEnterRoom()
        {
            if (currentFloor > 0)
            {
                floors[currentFloor - 1].CloseDoors();
                floors[currentFloor - 1].gameObject.SetActive(false);
            }
        }


        public void ActivateNextFloor()
        {
            if (currentFloor >= 0)
            {
                try
                {
                    onFinishFloor?.Invoke();
                }
                catch (System.StackOverflowException) { }
            }

            if (currentFloor + 1 >= floors.Length)
            {
                onFinishAllFloors?.Invoke();
            }

            currentFloor++;
            floors[currentFloor].Activate();
        }
    }
}