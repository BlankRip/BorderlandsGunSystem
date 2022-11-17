using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns {
    public interface IGunMode
    {
        GunModeData GetNormalData();
        GunModeData GetADSData();
        Sprite GetAdsSprite();
        void ReloadEvent();
        void SwithcModeEvent();
    }
}