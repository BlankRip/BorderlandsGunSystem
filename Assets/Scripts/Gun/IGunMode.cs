using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns {
    public enum FiringMode {SemiAuto, Auto}
    public interface IGunMode
    {
        GunModeData GetNormalData();
        GunModeData GetADSData();
        Sprite GetAdsSprite();
        void ReloadEvent();
        void SwithcModeEvent();
    }

    [System.Serializable]
    public class GunModeData
    {
        public FiringMode fireMode;
        public GameObject bulletObj;
        public string modeDisplayName;
        public float recoil;
        public float gapBtwShots;
        public float spreadAmount;

        public GunModeData() {
            fireMode = FiringMode.SemiAuto;
            bulletObj = null;
            modeDisplayName = "Not Entered";
            recoil = 0;
            gapBtwShots = 0.3f;
            spreadAmount = 1;
        }

        public void CopyStats(GunModeData copyFrom) {
            this.fireMode = copyFrom.fireMode;
            this.bulletObj = copyFrom.bulletObj;
            this.recoil = copyFrom.recoil;
            this.gapBtwShots = copyFrom.gapBtwShots;
            this.spreadAmount = copyFrom.spreadAmount;
        }
    }
}