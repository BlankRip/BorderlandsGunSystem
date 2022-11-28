using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Guns;
using Gameplay.UI;

namespace Gameplay.Components
{
    public class HelathComponent : MonoBehaviour
    {
        [Tooltip("Leave empty if not for player & attach health hud to this object")]
        [SerializeField] ScriptablePlayerHud playerHud;
        [SerializeField] float maxHealth = 100.0f;
        [SerializeField] SheildComponent sheild;

        private IHealthHud uiHandler;
        private float health;
        private float Health {
            get {return health;}
            set {
                health = value;
                if(uiHandler != null)
                    uiHandler.SetHealthValue(health);
            }
        }
        private ElementData appliedElement;
        private int elementTime;
        private float dmgTimer;


        private void Start() {
            if(playerHud)
                uiHandler = playerHud.healthHud;
            else
                uiHandler = GetComponent<IHealthHud>();
            if(uiHandler != null)
                uiHandler.SetMaxHealth(maxHealth);
            else
                Debug.LogError("Health Hud Not attached to this object");
            
            Health = maxHealth;
            dmgTimer = 0;
            elementTime = 0;
        }

        private void Update() {
            if(appliedElement.Element != ElementType.Nada) {
                dmgTimer += Time.deltaTime;
                if(dmgTimer >= 1.0f) {
                    TakeDamage(appliedElement.ElementPower);
                    dmgTimer = 0;
                    elementTime--;
                    if(elementTime <= 0) {
                        elementTime = 0;
                        appliedElement.Element = ElementType.Nada;
                    }
                }
            }
        }

        public void SetSheild(SheildComponent _sheild) {
            sheild = _sheild;
        }

        public void Heal(float _healAmount) {
            if(Health == maxHealth)
                return;
            Health += _healAmount;
            if(Health > maxHealth)
                Health = maxHealth;
        }

        public void TakeDamage(float _dmgAmount, ElementData _elemData) {
            if(_elemData.Element != ElementType.Nada) {
                float _randTrigger = Random.Range(0.0f, 100.0f);
                if(_randTrigger < _elemData.TriggerChance) {
                    elementTime++;
                    if(appliedElement.Element == ElementType.Nada)
                        dmgTimer = 0;
                    appliedElement = _elemData;
                }
            }
            TakeDamage(_dmgAmount);
        }

        private void TakeDamage(float _dmgAmount) {
            if(sheild) {
                sheild.TakeDamage(_dmgAmount);
                if(sheild.IsFullDmgAbsorbed())
                    return;
                _dmgAmount = sheild.GetDmgRamaining();
            }
            Health -= _dmgAmount;
        }
    }
}