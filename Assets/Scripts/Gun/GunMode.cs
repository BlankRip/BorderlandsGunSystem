using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns {
    public class GunMode : MonoBehaviour, IGunMode
    {
        [SerializeField] protected GunModeData normal;
        [SerializeField] protected bool copyNormalToADS;
        [SerializeField] protected Sprite adsSprite;
        [SerializeField] protected GunModeData ads;

        protected void Start() {
            if(copyNormalToADS)
                ads.CopyStats(normal);
        }

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