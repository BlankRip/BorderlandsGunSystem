using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Components {
    public class SheildComponent : MonoBehaviour
    {
        [SerializeField] float maxSheild = 100.0f;
        [SerializeField] float rechargeDelay = 2.7f;
        [Tooltip("This is per second")]
        [SerializeField] float recoveryRate = 30.0f;


        private float sheild;
        private bool fullDmgAbsorbed;
        private float damageRemaining;
        private float rechargeDelayTimer;
        private float recoveryTimer;

        private void Start() {
            sheild = maxSheild;
            rechargeDelayTimer = 0.0f;
            recoveryTimer = 1.0f;

            HelathComponent healthComp = GetComponent<HelathComponent>();
            if(healthComp)
                healthComp.SetSheild(this);
        }

        private void Update() {
            if(sheild == maxSheild)
                return;
            
            rechargeDelayTimer += Time.deltaTime;
            if(rechargeDelayTimer >= rechargeDelay) {
                recoveryTimer += Time.deltaTime;
                if(recoveryTimer > 1.0f) {
                    sheild += recoveryRate;
                    recoveryTimer = 0.0f;
                    if(sheild >= maxSheild) {
                        recoveryTimer = 1.0f;
                        sheild = maxSheild;
                    }
                }
            }
        }

        public void TakeDamage(float _dmgAmount) {
            fullDmgAbsorbed = true;
            damageRemaining = 0;
            rechargeDelayTimer = 0;

            if(sheild < _dmgAmount) {
                damageRemaining = _dmgAmount - sheild;
                fullDmgAbsorbed = false;
                sheild = 0;
                return;
            }

            sheild -= _dmgAmount;
        }

        public float GetDmgRamaining() {
            return damageRemaining;
        }

        public bool IsFullDmgAbsorbed() {
            return fullDmgAbsorbed;
        }
    }
}