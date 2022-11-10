using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns {
    public class Gun : MonoBehaviour
    {
        private IGunMode modeA, modeB;

        protected Sprite adsSprite_A, adsSprite_B;
        protected IGunMode currentGunMode;
        protected GunModeData currentModeData;
        protected bool inADS;

        protected void Start() {
            IGunMode[] modes = GetComponents<IGunMode>();
            if(modes.Length != 2) {
                Debug.LogError("There is either less than or more than 2 gun modes attached");
                return;
            }
            modeA = modes[0];
            modeB = modes[1];
            adsSprite_A = modeA.GetAdsSprite();
            adsSprite_B = modeB.GetAdsSprite();

            currentGunMode = modeA;
            currentModeData = currentGunMode.GetNormalData();
        }

        private void Update() {
            if(currentModeData.fireMode == FiringMode.SemiAuto)
                return;
        }

        public void EnterADS() {
            currentModeData = currentGunMode.GetADSData();
            inADS = true;
            //move into ads view
        }

        public void ExitADS() {
            currentModeData = currentGunMode.GetNormalData();
            inADS = false;
            //move into ads view
        }

        public virtual void Fire() { }

        public void Reload() { }

        public void SwithcMode() {
            if(inADS)
                ExitADS();
            currentGunMode = (currentGunMode == modeA) ? modeB : modeA;
            currentModeData = currentGunMode.GetNormalData();
        }
    }
}