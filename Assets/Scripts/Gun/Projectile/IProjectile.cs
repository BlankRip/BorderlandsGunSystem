using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns.Projectile {
    public interface IProjectile
    {
        void Initilize();
        void SetDamage(float _damage);
        void SetElement(ElementData _elementData);
    }
}