using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Guns;
using Gameplay.UI;

namespace Gameplay.Components
{
    public class HealthComponent : MonoBehaviour
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
            appliedElement = new ElementData();
            appliedElement.Element = ElementType.Nada;
            dmgTimer = 0;
            elementTime = 0;
            Health = maxHealth;

            if(playerHud != null)
                uiHandler = playerHud.healthHud;
            else
                uiHandler = GetComponent<IHealthHud>();
            if(uiHandler != null) {
                uiHandler.SetMaxHealth(maxHealth);
                uiHandler.SetHealthValue(Health);
            } else
                Debug.LogError("Health Hud Not attached to this object", this.gameObject);
            
        }

        private void Update() {
            if(appliedElement.Element != ElementType.Nada) {
                dmgTimer += Time.deltaTime;
                TakeDamage(appliedElement.ElementPower * Time.deltaTime);
                if(dmgTimer >= 1.0f) {
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
                    Debug.Log("HERE");
                if(_randTrigger < _elemData.TriggerChance) {
                    elementTime++;
                    if(appliedElement.Element == ElementType.Nada)
                        dmgTimer = 0;
                    appliedElement = new ElementData(_elemData);
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
            if(Health < 0)
                Health = 0;
        }
    }
}