using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI {
    public class HealthHud : MonoBehaviour, IHealthHud
    {
        [SerializeField] ScriptablePlayerHud playerHud;
        [SerializeField] Slider hpSlider, sheildSlider;

        private void Awake() {
            if(playerHud != null)
                playerHud.healthHud = this;
        }

        public void SetHealthValue(float _value) {
            hpSlider.value = _value;
        }

        public void SetSheildValue(float _value) {
            sheildSlider.value = _value;
        }

        public void SetMaxHealth(float _maxHp) {
            hpSlider.maxValue = _maxHp;
        }

        public void SetMaxSheild(float _maxSheild) {
            sheildSlider.maxValue = _maxSheild;
        }
    }
}