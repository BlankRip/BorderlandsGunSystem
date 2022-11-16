using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gameplay.UI {
    public class PlayerHud : MonoBehaviour, IPlayerHud
    {
        [SerializeField] ScriptablePlayerHud scriptableHud;

        [Header("Gun Data")]
        [SerializeField] TextMeshProUGUI gunModeText;

        [Space] [Header("Ammo Data")]
        [SerializeField] TextMeshProUGUI ammoInClipText;
        [SerializeField] TextMeshProUGUI availabeAmmoText;

        [Space] [Header("ADS Data")]
        [SerializeField] Image adsOverlay;
        [SerializeField] GameObject hudUnderOverlay;

        private void Awake() {
            scriptableHud.hud = this;
        }

        public void ADSOverlay(bool _activeState) {
            hudUnderOverlay.SetActive(!_activeState);
            adsOverlay.gameObject.SetActive(_activeState);
        }

        public void SetADSSprite(Sprite _adsSprite) {
            adsOverlay.sprite = _adsSprite;
        }

        public void SetGunModeText(string _name) {
            gunModeText.SetText(_name);
        }

        public void SetAmmoInClipText(int _amount) {
            ammoInClipText.SetText(_amount.ToString());
        }

        public void SetAvailableAmmoText(int _amount) {
            availabeAmmoText.SetText(_amount.ToString());
        }
    }
}