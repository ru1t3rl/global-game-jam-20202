using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Spells
{
    public class SpellController : MonoBehaviour
    {
        [SerializeField] private GameObject startSpell;
        [SerializeField] private Transform origin;

        private float offset = 0.5f;
        private float currentCooldown;

        public BaseSpell CurrentSpell { get; private set;  }
        
        // Update is called once per frame
        void Update()
        {
            if (currentCooldown <= 0)
                GetInput();
            else
                currentCooldown -= Time.deltaTime;
        }

        private void GetInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CurrentSpell = Instantiate(startSpell).GetComponent<BaseSpell>();
                CurrentSpell.TryPerform(origin.position + origin.forward * offset, origin.position + origin.forward * CurrentSpell.Stats.Range);
                currentCooldown = CurrentSpell.Stats.Cooldown;
            }
        }
    }
}