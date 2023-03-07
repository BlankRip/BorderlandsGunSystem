using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns.Projectile {
    public interface IProjectile
    {
        void Initilize(Vector3 _targetPos);
        void Initilize(Vector3 _targetPos, float _damage);
        void Initilize(Vector3 _targetPos, float _damage, ElementData _elementData);
    }
}