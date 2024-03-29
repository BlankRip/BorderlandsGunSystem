using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns {
    [CreateAssetMenu(fileName = "Gun Mode Data", menuName = "Guns/Gun Mode Data", order = 0)]
    public class GunModeData: ScriptableObject
    {
        public FiringMode FireMode;
        public ElementData ElementData;
        public GameObject BulletObj_GO;
        public string ModeDisplayName_S;
        public GunSpreadConfig spreadConfig;
        public float RecoilAmount_F;
        public float GapBtwShots_F;
        public int BulletsPerBurst_I;

        public GunModeData() {
            FireMode = FiringMode.SemiAuto;
            ElementData = new ElementData();
            BulletObj_GO = null;
            ModeDisplayName_S = "Not Entered";
            RecoilAmount_F = 0;
            GapBtwShots_F = 0.3f;
            BulletsPerBurst_I = -1;
        }

        public void CopyStats(GunModeData _copyFrom) {
            this.FireMode = _copyFrom.FireMode;
            this.ElementData = _copyFrom.ElementData;
            this.BulletObj_GO = _copyFrom.BulletObj_GO;
            this.RecoilAmount_F = _copyFrom.RecoilAmount_F;
            this.GapBtwShots_F = _copyFrom.GapBtwShots_F;
            this.spreadConfig = _copyFrom.spreadConfig;
        }
    }
}

namespace Gameplay {
    [System.Serializable]
    public class ElementData
    {
        public ElementType Element;
        public int ElementPower;
        [Range(0, 100.0f)]
        public float TriggerChance;

        public ElementData() {
            Element = ElementType.Nada;
            ElementPower = 0;
            TriggerChance = 0;
        }

        public ElementData(ElementData other) {
            this.Element = other.Element;
            this.ElementPower = other.ElementPower;
            this.TriggerChance = other.TriggerChance;
        }
    }
}