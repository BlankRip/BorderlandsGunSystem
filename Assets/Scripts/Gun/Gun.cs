using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns {
    public enum GunType {Pistol, SMG, ShotGun, Sniper, RocketLauncher}
    public class Gun : MonoBehaviour
    {
        [SerializeField] GunType gunType;
        [SerializeField] protected int clipSize = 7;
        protected int currentInClip;
        protected AmmoSupply ammoSupply;

        protected Camera playerCam;

        protected IGunMode modeA, modeB;

        protected Sprite adsSprite_A, adsSprite_B;
        protected IGunMode currentGunMode;
        protected GunModeData currentModeData;

        protected enum GunState {Idel, InADS, Reloading};
        protected GunState gunState;
        protected GunState stateBeforeReload;
        protected bool firing;
        protected float timer;
        protected int burstTracker;

        protected void Start() {
            IGunMode[] _modes = GetComponents<IGunMode>();
            if(_modes.Length != 2) {
                Debug.LogError("There is either less than or more than 2 gun modes attached");
                return;
            }
            modeA = _modes[0];
            modeB = _modes[1];
            adsSprite_A = modeA.GetAdsSprite();
            adsSprite_B = modeB.GetAdsSprite();

            currentGunMode = modeA;
            currentModeData = currentGunMode.GetNormalData();
            gunState = GunState.Idel;
            currentInClip = clipSize;
        }

        public void Equip(Camera _camera, AmmoSupply _supply) {
            if(playerCam == null)
                playerCam = _camera;
            if(ammoSupply == null)
                ammoSupply = _supply;
            ammoSupply.SetCurrentAmmoSupplier(gunType);
        }

        private void Update() {
            if(gunState == GunState.Reloading)
                return;
            
            timer += Time.deltaTime;
            if(timer > currentModeData.GapBtwShots_F) {
                if(firing) {
                    Fire();
                    timer = 0;
                    if(currentModeData.FireMode == FiringMode.SemiAuto) {
                        burstTracker++;
                        if(burstTracker >= currentModeData.BulletsPerBurst_I)
                            firing = false;
                    }
                }
            }
        }

        public void EnterADS() {
            currentModeData = currentGunMode.GetADSData();
            gunState = GunState.InADS;
            Debug.Log("ADS ENTERED");
            //move into ads view
        }

        public void ExitADS() {
            if(gunState != GunState.InADS)
                return;
            currentModeData = currentGunMode.GetNormalData();
            gunState = GunState.Idel;
            Debug.Log("ADS EXITED");
            //move into ads view
        }

        public void StartFiring() {
            if(currentModeData.FireMode == FiringMode.Auto) {
                firing = true;
            } else {
                if(currentModeData.BulletsPerBurst_I > 0) {
                    firing = true;
                    burstTracker = 0;
                } else
                    Fire();
            }
        }

        protected virtual void Fire() {
            if(currentInClip > 0) {
                currentInClip--;
                Debug.Log("SHOT FIRED");
            } else {
                StartReload();
            }
        }

        public void StopFiring() {
            firing = false;
        }

        public virtual void StartReload() {
            if(currentInClip == clipSize || ammoSupply.GetAmmoAvailable() <= 0)
                return;
            
            firing = false;
            stateBeforeReload = gunState;
            gunState = GunState.Reloading;
            //Play Reload Animation
            Reload();
        }

        private void Reload() {
            Debug.Log("RELOADED");
            int _amountToAdd = clipSize;
            int _bulletsRequired = clipSize - currentInClip;
            int _availableAmmo = ammoSupply.GetAmmoAvailable();
            if(_bulletsRequired > _availableAmmo) {
                _bulletsRequired = _availableAmmo;
                _amountToAdd = currentInClip + _availableAmmo;
            }
            ammoSupply.TakeAmmoFromSupply(_bulletsRequired);
            currentInClip = _amountToAdd;

            currentGunMode.ReloadEvent();
            gunState = stateBeforeReload;
            if(stateBeforeReload == GunState.InADS)
                EnterADS();
        }

        public void StartModeSwitch() {
            if(gunState == GunState.InADS)
                ExitADS();
            //Play switching animation
            SwitchMode();
        }

        private void SwitchMode() {
            Debug.Log("GUN MODE SWITCHED");
            currentGunMode.SwithcModeEvent();
            currentGunMode = (currentGunMode == modeA) ? modeB : modeA;
            currentModeData = currentGunMode.GetNormalData();
        }
    }
}