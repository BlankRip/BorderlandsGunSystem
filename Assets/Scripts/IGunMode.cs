using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FiringMode {SemiAuto, Auto}
public interface IGunMode
{
    GunModeData GetADSData();
    GunModeData GetNormalData();
    GameObject GetBullet();
    Sprite GetAdsSprite();
    void ReloadEvent();
    void SwithcModeEvent();
}

[System.Serializable]
public class GunModeData
{
    public FiringMode fireMode;
    public string modeDisplayName;
    public float recoil;
    public float gapBtwShots;
    public float spreadAmount;

    public GunModeData() {
        fireMode = FiringMode.SemiAuto;
        modeDisplayName = "Not Entered";
        recoil = 0;
        gapBtwShots = 0.3f;
        spreadAmount = 1;
    }

    public void CopyStats(GunModeData copyFrom) {
        this.fireMode = copyFrom.fireMode;
        this.recoil = copyFrom.recoil;
        this.gapBtwShots = copyFrom.gapBtwShots;
        this.spreadAmount = copyFrom.spreadAmount;
    }
}