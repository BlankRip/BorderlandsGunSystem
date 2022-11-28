using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Components {
    public class SheildComponent : MonoBehaviour
    {
        [SerializeField] float maxSheild;
        [SerializeField] float rechargeDelay;
        [Tooltip("This is per second")]
        [SerializeField] float recoveryRate;


        private bool fullDmgAbsorbed;
        private float damageRemaining;

        private void Update() {
            
        }

        public void TakeDamage(float _dmgAmount) {
            fullDmgAbsorbed = false;
            damageRemaining = _dmgAmount;

            //Do dmg stuff here
            
            damageRemaining = 0;
            fullDmgAbsorbed = true;
        }

        public float GetDmgRamaining() {
            return damageRemaining;
        }

        public bool IsFullDmgAbsorbed() {
            return fullDmgAbsorbed;
        }
    }
}