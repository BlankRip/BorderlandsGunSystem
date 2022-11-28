using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Guns;
using Gameplay.Components;

namespace Gameplay {
    public class TestDummy : MonoBehaviour
    {
        [SerializeField] float dmg = 20.0f;
        [SerializeField] ElementData elmDmg;
        [SerializeField] float healAmt = 50.0f;

        private HealthComponent healthComp;

        private void Start() {
            healthComp = GetComponent<HealthComponent>();
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.L))
                healthComp.TakeDamage(dmg, elmDmg);
            if(Input.GetKeyDown(KeyCode.H))
                healthComp.Heal(healAmt);
        }
    }
}