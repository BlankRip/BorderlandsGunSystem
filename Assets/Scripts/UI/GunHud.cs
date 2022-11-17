using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gameplay.Guns;

namespace Gameplay.UI {
    public class GunHud : MonoBehaviour, IGunHud
    {
        [SerializeField] ScriptablePlayerHud scriptableHud;

        [Header("Gun Icon Data")]
        [SerializeField] Image gunIcon;
        [SerializeField] GunTypeIcons gunTypeIcons;
        [SerializeField] Image elementTypeIcon;
        [SerializeField] ElementIcons elementIcons;

        [Space]
        [SerializeField] TextMeshProUGUI gunModeText;

        [Header("Ammo Data")]
        [SerializeField] TextMeshProUGUI ammoInClipText;
        [SerializeField] TextMeshProUGUI availabeAmmoText;

        [Header("ADS Data")]
        [SerializeField] Image adsOverlay;
        [SerializeField] GameObject hudUnderOverlay;

        private void Awake() {
            scriptableHud.gunHud = this;
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

        public void SetGunIcon(GunType _gunType) {
            switch(_gunType) {
                case GunType.Pistol:
                    gunIcon.sprite = gunTypeIcons.PistolIcon;
                    break;
                case GunType.AssaultRifle:
                    gunIcon.sprite = gunTypeIcons.AssaultRifleIcon;
                    break;
                case GunType.ShotGun:
                    gunIcon.sprite = gunTypeIcons.ShotGunIcon;
                    break;
                case GunType.SMG:
                    gunIcon.sprite = gunTypeIcons.SMGIcon;
                    break;
                case GunType.Sniper:
                    gunIcon.sprite = gunTypeIcons.SniperIcon;
                    break;
                case GunType.RocketLauncher:
                    gunIcon.sprite = gunTypeIcons.RocketLauncherIcon;
                    break;
            }
        }

        public void SetElementIcon(ElementType _elementType) {
            switch(_elementType) {
                case ElementType.Nada:
                    elementTypeIcon.sprite = elementIcons.NadaIcon;
                    break;
                case ElementType.Fire:
                    elementTypeIcon.sprite = elementIcons.FireIcon;
                    break;
                case ElementType.Corrosion:
                    elementTypeIcon.sprite = elementIcons.CorrosionIcon;
                    break;
                case ElementType.Electric:
                    elementTypeIcon.sprite = elementIcons.ElectricIcon;
                    break;
                case ElementType.Radiation:
                    elementTypeIcon.sprite = elementIcons.RadiationIcon;
                    break;
                case ElementType.Blast:
                    elementTypeIcon.sprite = elementIcons.BlastIcon;
                    break;
            }
        }
    }
}