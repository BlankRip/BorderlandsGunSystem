using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns.Projectile {
    public class ProjectileBase : MonoBehaviour, IProjectile
    {
        [SerializeField] protected float moveSpeed = 20.0f;
        protected float damage;
        protected TrailRenderer trailRenderer;
        [SerializeField] protected TrailColors trailColors;
        protected Vector3 targetPos;
        protected ElementData elementData;

        protected virtual void InitilizeCustom()
        {
            transform.forward = (targetPos - transform.position).normalized;

            trailRenderer = GetComponent<TrailRenderer>();
            if(trailRenderer != null)
            {
                Color trailColor = trailColors.GetTrailColor(elementData.Element);
                trailRenderer.startColor = trailColor;
                trailRenderer.endColor = trailColor;
            }
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