using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns {
    public class GunModeBase : MonoBehaviour, IGunMode
    {
        [SerializeField] protected GunModeData normal;
        [SerializeField] protected Sprite adsSprite;
        [SerializeField] protected GunModeData ads;

        public GunModeData GetADSData() {
            return ads;
        }

        public Sprite GetAdsSprite() {
            return adsSprite;
        }

        public GunModeData GetNormalData() {
            return normal;
        }

        public virtual void ReloadEvent() { }

        public virtual void SwithcModeEvent() { }
    }
}