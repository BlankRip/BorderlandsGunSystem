using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Components {
    public class SimpleHealthComponent : MonoBehaviour, IDamagable
    {
        [SerializeField] float health = 100.0f;
        public Action OnDeath;
        private bool dead;

        public void TakeDamage(float _dmgAmount, ElementData _elemData) {
            if(dead)
                return;
            
            health -= _dmgAmount;
            if(health <= 0) {
                health = 0;
                dead = true;
                if(OnDeath != null)
                    OnDeath();
            }
        }

        // private void Start() {
        //     OnDeath += TestOnDeath1;
        //     OnDeath += TestOnDeath2;
        //     OnDeath();

        //     OnDeath -= TestOnDeath2;
        //     OnDeath();
        // }

        // void TestOnDeath1() {
        //     Debug.Log("111111111111");
        // }

        // void TestOnDeath2() {
        //     Debug.Log("222222222222");
        // }
    }
}