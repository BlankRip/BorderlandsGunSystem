using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGunMode1 : GunMode, IGunMode {
    public GunModeData GetADSData() {
        throw new System.NotImplementedException();
    }

    public Sprite GetAdsSprite() {
        return adsSprite;
    }

    public GameObject GetBullet() {
        throw new System.NotImplementedException();
    }

    public GunModeData GetNormalData() {
        throw new System.NotImplementedException();
    }

    public void ReloadEvent() {
        throw new System.NotImplementedException();
    }

    public void SwithcModeEvent() {
        throw new System.NotImplementedException();
    }
}
