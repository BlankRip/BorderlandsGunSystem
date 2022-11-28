using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns.Projectile {
    public class ProjectileBase : MonoBehaviour, IProjectile
    {
        protected float damage;
        protected ElementData elementData;

        public virtual void Initilize() { }

        public void SetDamage(float _damage) {
            damage = _damage;
        }

        public void SetElement(ElementData _elementData) {
            elementData = _elementData;
        }
    }
}