using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns {
    public class TestGunMode1 : GunModeBase
    {
        public override void ReloadEvent() {
            Debug.Log("Test Reload event");
        }

        public override void SwithcModeEvent() {
            Debug.Log("Test Switch Event");
        }
    }
}