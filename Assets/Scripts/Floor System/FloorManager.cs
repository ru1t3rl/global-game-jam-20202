using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace GGJ.Floors
{
#if UNITY_EDITOR
    [CustomEditor(typeof(FloorManager))]
    public class FloorManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Unlock Next"))
            {
                (target as FloorManager).Floors[(target as FloorManager).CurrentFloor].onFinishFloor?.Invoke();
            }
        }
    }

#endif

    public class FloorManager : MonoBehaviour
    {
        [SerializeField] Floor[] floors;
        public Floor[] Floors => floors;
        public UnityEvent onFinishFloor, onFinishAllFloors;
        int currentFloor = -1;
        public int CurrentFloor => currentFloor;

        void Awake()
        {
            for (int iFloor = 0; iFloor < floors.Length; iFloor++)
            {
                floors[iFloor].onEnterRoom.AddListener(OnEnterRoom);
                floors[iFloor].gameObject.SetActive(false);
            }


            if (floors.Length > 0)
                ActivateNextFloor();
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