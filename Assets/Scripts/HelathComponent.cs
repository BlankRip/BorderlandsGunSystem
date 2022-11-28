using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Guns;

namespace Gameplay.Components
{
    public class HelathComponent : MonoBehaviour
    {
        [SerializeField] float maxHealth;
        [SerializeField] SheildComponent sheild;

        private float health;
        private ElementData appliedElement;
        private int elementTime;
        private float dmgTimer;

        private void Start() {
            health = maxHealth;
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
            if(health == maxHealth)
                return;
            health += _healAmount;
            if(health > maxHealth)
                health = maxHealth;
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
            health -= _dmgAmount;
        }
    }
}