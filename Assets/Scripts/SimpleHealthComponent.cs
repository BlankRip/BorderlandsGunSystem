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
    }
}