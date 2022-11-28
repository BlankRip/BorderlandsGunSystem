using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns.Projectile {
    public class ProjectileBase : MonoBehaviour, IProjectile
    {
        protected float damage;
        protected ElementType element;
        protected int elementPower = 0;
        protected float triggerChance;

        public virtual void Initilize() { }

        public void SetDamage(float _damage) {
            damage = _damage;
        }

        public void SetElement(ElementType _elementType, int _elementPower, float _triggerChance) {
            element = _elementType;
            elementPower = _elementPower;
            triggerChance = _triggerChance;
        }
    }
}