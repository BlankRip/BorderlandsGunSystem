using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns {
    public class AmmoSupply : MonoBehaviour
    {
        [System.Serializable]
        private class AmmoData {
            public int baseAmmo;
            public int maxAmmo;
            [HideInInspector] public int currentAmmo;

            public AmmoData() {
                baseAmmo = 30;
                maxAmmo = 150;
                currentAmmo = 0;
            }
        }

        [SerializeField] AmmoData pistolAmmo, assaultRifle, smgAmmo, shotGunAmmo, sniperAmmo, rockLauncherAmmo;
        private AmmoData currentSupply;

        private void Awake() {
            pistolAmmo.currentAmmo = pistolAmmo.baseAmmo;
            assaultRifle.currentAmmo = assaultRifle.baseAmmo;
            smgAmmo.currentAmmo = smgAmmo.baseAmmo;
            shotGunAmmo.currentAmmo = shotGunAmmo.baseAmmo;
            sniperAmmo.currentAmmo = sniperAmmo.baseAmmo;
            rockLauncherAmmo.currentAmmo = rockLauncherAmmo.baseAmmo;
        }

        public void SetCurrentAmmoSupplier(GunType _gunType) {
            switch (_gunType) {
                case GunType.Pistol:
                    currentSupply = pistolAmmo;
                    break;
                case GunType.SMG:
                    currentSupply = smgAmmo;
                    break;
                case GunType.ShotGun:
                    currentSupply = shotGunAmmo;
                    break;
                case GunType.Sniper:
                    currentSupply = sniperAmmo;
                    break;
                case GunType.RocketLauncher:
                    currentSupply = rockLauncherAmmo;
                    break;
            }
        }

        public int GetAmmoInReserve() {
            return currentSupply.currentAmmo;
        }

        public void TakeAmmoFromSupply(int _amount) {
            currentSupply.currentAmmo -= _amount;
        }
    }
}