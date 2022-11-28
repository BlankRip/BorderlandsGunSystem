using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.UI;

namespace Gameplay.Components {
    public class SheildComponent : MonoBehaviour
    {
        [Tooltip("Leave empty if not for player & attach health hud to this object")]
        [SerializeField] ScriptablePlayerHud playerHud;
        [SerializeField] float maxSheild = 150.0f;
        [SerializeField] float rechargeDelay = 2.7f;
        [Tooltip("This is per second")]
        [SerializeField] float recoveryRate = 30.0f;

        private IHealthHud uiHandler;
        private float sheild;
        private float Sheild {
            get {return sheild;}
            set {
                sheild = value;
                if(uiHandler != null)
                    uiHandler.SetSheildValue(sheild);
            }
        }
        private bool fullDmgAbsorbed;
        private float damageRemaining;
        private float rechargeDelayTimer;
        private float recoveryTimer;

        private void Start() {
            if(playerHud != null)
                uiHandler = playerHud.healthHud;
            else
                uiHandler = GetComponent<IHealthHud>();
            if(uiHandler != null)
                uiHandler.SetMaxSheild(maxSheild);
            else
                Debug.LogError("Health Hud Not attached to this object", this.gameObject);

            Sheild = maxSheild;
            rechargeDelayTimer = 0.0f;
            recoveryTimer = 1.0f;

            HealthComponent healthComp = GetComponent<HealthComponent>();
            if(healthComp)
                healthComp.SetSheild(this);
        }

        private void Update() {
            if(Sheild == maxSheild)
                return;
            
            rechargeDelayTimer += Time.deltaTime;
            if(rechargeDelayTimer >= rechargeDelay) {
                recoveryTimer += Time.deltaTime;
                if(recoveryTimer > 1.0f) {
                    Sheild += recoveryRate;
                    recoveryTimer = 0.0f;
                    if(Sheild >= maxSheild) {
                        recoveryTimer = 1.0f;
                        Sheild = maxSheild;
                    }
                }
            }
        }

        public void TakeDamage(float _dmgAmount) {
            fullDmgAbsorbed = true;
            damageRemaining = 0;
            rechargeDelayTimer = 0;

            if(Sheild < _dmgAmount) {
                damageRemaining = _dmgAmount - Sheild;
                fullDmgAbsorbed = false;
                Sheild = 0;
                return;
            }

            Sheild -= _dmgAmount;
        }

        public float GetDmgRamaining() {
            return damageRemaining;
        }

        public bool IsFullDmgAbsorbed() {
            return fullDmgAbsorbed;
        }
    }
}