using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Spells
{
    public class SpellController : MonoBehaviour
    {
        [SerializeField] private GameObject startSpell;
        private float offset = 0.5f;

        public BaseSpell CurrentSpell { get; private set;  }
        private void Awake()
        {
            CurrentSpell = Instantiate(startSpell).GetComponent<BaseSpell>();
        }

        
        // Update is called once per frame
        void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                CurrentSpell.Attack(transform.position + transform.forward * offset, transform.position + transform.forward * CurrentSpell.Stats.Range);
            }
        }
    }
}