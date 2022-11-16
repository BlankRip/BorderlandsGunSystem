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
        public FiringMode FireMode;
        public GameObject BulletObj_GO;
        public string ModeDisplayName_S;
        public float RecoilAmount_F;
        public float GapBtwShots_F;
        public float SpreadAmount_F;
        public int BulletsPerBurst_I;

        public GunModeData() {
            FireMode = FiringMode.SemiAuto;
            BulletObj_GO = null;
            ModeDisplayName_S = "Not Entered";
            RecoilAmount_F = 0;
            GapBtwShots_F = 0.3f;
            SpreadAmount_F = 1;
            BulletsPerBurst_I = -1;
        }

        public void CopyStats(GunModeData _copyFrom) {
            this.FireMode = _copyFrom.FireMode;
            this.BulletObj_GO = _copyFrom.BulletObj_GO;
            this.RecoilAmount_F = _copyFrom.RecoilAmount_F;
            this.GapBtwShots_F = _copyFrom.GapBtwShots_F;
            this.SpreadAmount_F = _copyFrom.SpreadAmount_F;
        }
    }
}