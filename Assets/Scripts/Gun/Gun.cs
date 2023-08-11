using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.UI;
using Gameplay.Guns.Projectile;

namespace Gameplay.Guns {
    public class Gun : MonoBehaviour
    {
        [SerializeField] ScriptablePlayerHud playerHud;
        [SerializeField] GunType gunType;
        [SerializeField] protected int clipSize = 7;
        [SerializeField] float weaponDamage = 20.0f;
        [SerializeField] Transform muzzlePosition;
        [SerializeField] float rayRange = 1000.0f;
        [SerializeField] LayerMask bulletHitLayers;

        [Space] [Header("Debugging")]
        [SerializeField] bool debugTargetPoint;
        [SerializeField] Transform debugTargetPointTransform;
        
        private int currentInClip;
        protected int CurrentInClip {
            get{return currentInClip; } 
            set{
                currentInClip = value;
                playerHud.gunHud.SetAmmoInClipText(CurrentInClip);
            }
        }
        protected AmmoSupply ammoSupply;

        protected Camera playerCam;

        protected IGunMode modeA, modeB;

        protected IGunMode currentGunMode;
        protected GunModeData currentModeData;

        protected enum GunState {Idel, InADS, Reloading};
        protected GunState gunState;
        protected GunState stateBeforeReload;
        protected bool firing;
        protected float timer;
        protected int burstTracker;

        protected float spreadTimer;
        protected float stoppedShootingTime;

        bool initilizedGun;

        protected void Start()
        {
            if(initilizedGun)
                return;
            
            IGunMode[] _modes = GetComponents<IGunMode>();
            if(_modes.Length != 2)
            {
                Debug.LogError("There is either less than or more than 2 gun modes attached");
                return;
            }
            modeA = _modes[0];
            modeB = _modes[1];

            currentGunMode = modeA;
            currentModeData = currentGunMode.GetNormalData();
            gunState = GunState.Idel;
            CurrentInClip = clipSize;

            initilizedGun = true;
        }

        public void Equip(Camera _camera, AmmoSupply _supply) {
            if(playerCam == null)
                playerCam = _camera;
            if(ammoSupply == null)
                ammoSupply = _supply;
            ammoSupply.SetCurrentAmmoSupplier(gunType);
            spreadTimer = 0;

            if(currentModeData == null)
                Start();

            if(playerHud != null && playerHud.gunHud != null)
            {
                playerHud.gunHud.SetGunIcon(gunType);
                playerHud.gunHud.SetElementIcon(currentModeData.ElementData.Element);

                playerHud.gunHud.SetGunModeText(currentModeData.ModeDisplayName_S);

                playerHud.gunHud.SetAmmoInClipText(CurrentInClip);
                playerHud.gunHud.SetAvailableAmmoText(ammoSupply.GetAmmoInReserve());

                playerHud.gunHud.SetADSSprite(currentGunMode.GetAdsSprite());
            }
            else
                Debug.LogError("Gun Hud was not set up");
        }

        private void Update()
        {
            if(gunState == GunState.Reloading)
                return;
            
            timer += Time.deltaTime;
            if(timer > currentModeData.GapBtwShots_F)
            {
                if(firing)
                {
                    Fire();
                    timer = 0;
                    if(currentModeData.FireMode == FiringMode.SemiAuto)
                    {
                        burstTracker++;
                        if(burstTracker >= currentModeData.BulletsPerBurst_I)
                            firing = false;
                    }
                }   
            }
            HandleSpreadTimer();
        }

        private void HandleSpreadTimer()
        {
            if(currentModeData.spreadConfig.TypeOfSpread == GunSpreadConfig.SpreadType.None)
                return;

            if(firing)
                spreadTimer = Mathf.Clamp(0, spreadTimer + Time.deltaTime, currentModeData.spreadConfig.MaxSpreadTime_F);
            else
            {
                float recoveryPercentage = (currentModeData.spreadConfig.RecoilRevoverySpeed - (Time.time - stoppedShootingTime))/currentModeData.spreadConfig.RecoilRevoverySpeed;
                spreadTimer = Mathf.Lerp(0, currentModeData.spreadConfig.MaxSpreadTime_F, Mathf.Clamp01(recoveryPercentage));
            }
        }

        public void EnterADS()
        {
            currentModeData = currentGunMode.GetADSData();
            gunState = GunState.InADS;
            //move into ads view
            playerHud.gunHud.ADSOverlay(true);
        }

        public void ExitADS()
        {
            if(gunState != GunState.InADS)
                return;
            currentModeData = currentGunMode.GetNormalData();
            gunState = GunState.Idel;
            //move into ads view
            playerHud.gunHud.ADSOverlay(false);
        }

        public void StartFiring() {
            if(currentModeData.FireMode == FiringMode.Auto)
            {
                firing = true;
            }
            else
            {
                if(currentModeData.BulletsPerBurst_I > 0)
                {
                    firing = true;
                    burstTracker = 0;
                } 
                else
                {
                    spreadTimer = 0;
                    Fire();
                }
            }
        }

        protected virtual void Fire()
        {
            if(CurrentInClip > 0)
            {
                CurrentInClip--;
                IProjectile spawned = Instantiate(currentModeData.BulletObj_GO, muzzlePosition.position, muzzlePosition.rotation).GetComponent<IProjectile>();
                spawned.Initilize(CaluculateProjectileTarget(), weaponDamage, currentModeData.ElementData);
            }
            else
            {
                StartReload();
            }
        }

        private Vector3 CaluculateProjectileTarget()
        {
            Vector3 targetPos = Vector3.zero;
            Ray ray =  playerCam.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
            ray.direction = (ray.direction + currentModeData.spreadConfig.GetSpread(spreadTimer)).normalized;
            RaycastHit hitResult;
            if(Physics.Raycast(ray, out hitResult, rayRange, bulletHitLayers))
            {
                targetPos = hitResult.point;
                if(debugTargetPoint)
                {
                    debugTargetPointTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    debugTargetPointTransform.position = targetPos;
                }
            }
            else
            {
                targetPos = playerCam.transform.position + (ray.direction * rayRange);
                if(debugTargetPoint)
                {
                    debugTargetPointTransform.localScale = new Vector3(15f, 15f, 15f);
                    debugTargetPointTransform.position = targetPos;
                }
            }
            return targetPos;
        }

        public void StopFiring()
        {
            firing = false;
            stoppedShootingTime = Time.time;
        }

        public virtual void StartReload()
        {
            if(CurrentInClip == clipSize || ammoSupply.GetAmmoInReserve() <= 0)
                return;
            
            firing = false;
            stateBeforeReload = gunState;
            gunState = GunState.Reloading;
            //Play Reload Animation
            Reload();
        }

        private void Reload()
        {
            Debug.Log("RELOADED");
            int _amountToAdd = clipSize;
            int _bulletsRequired = clipSize - CurrentInClip;
            int _availableAmmo = ammoSupply.GetAmmoInReserve();
            if(_bulletsRequired > _availableAmmo) {
                _bulletsRequired = _availableAmmo;
                _amountToAdd = CurrentInClip + _availableAmmo;
            }
            ammoSupply.TakeAmmoFromSupply(_bulletsRequired);
            playerHud.gunHud.SetAvailableAmmoText(ammoSupply.GetAmmoInReserve());
            CurrentInClip = _amountToAdd;

            currentGunMode.ReloadEvent();
            gunState = stateBeforeReload;
            if(stateBeforeReload == GunState.InADS)
                EnterADS();
        }

        public void StartModeSwitch()
        {
            if(gunState == GunState.InADS)
                ExitADS();
            //Play switching animation
            SwitchMode();
        }

        private void SwitchMode()
        {
            currentGunMode.SwithcModeEvent();
            currentGunMode = (currentGunMode == modeA) ? modeB : modeA;
            currentModeData = currentGunMode.GetNormalData();

            playerHud.gunHud.SetElementIcon(currentModeData.ElementData.Element);
            playerHud.gunHud.SetGunModeText(currentModeData.ModeDisplayName_S);
            playerHud.gunHud.SetADSSprite(currentGunMode.GetAdsSprite());
        }
    }
}