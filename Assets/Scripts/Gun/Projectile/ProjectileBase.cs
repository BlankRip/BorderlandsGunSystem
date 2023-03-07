using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns.Projectile {
    public class ProjectileBase : MonoBehaviour, IProjectile
    {
        protected float damage;
        protected Vector3 targetPos;
        protected ElementData elementData;

        protected virtual void InitilizeCustom()
        {
            transform.forward = (targetPos - transform.position).normalized;
        }

        public void Initilize(Vector3 _targetPos)
        { 
            targetPos = _targetPos;
            damage = 0.0f;
            elementData = new ElementData();
            InitilizeCustom();
        }

        public void Initilize(Vector3 _targetPos, float _damage)
        {
            targetPos = _targetPos;
            damage = _damage;
            elementData = new ElementData();
            InitilizeCustom();
        }

        public void Initilize(Vector3 _targetPos, float _damage, ElementData _elementData)
        {
            targetPos = _targetPos;
            damage = _damage;
            elementData = _elementData;
            InitilizeCustom();
        }
    }
}