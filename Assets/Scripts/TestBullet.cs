using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns.Projectile {
    public class TestBullet : ProjectileBase
    {
        [SerializeField] float moveSpeed = 20.0f;
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            rb.MovePosition(transform.position + (transform.forward * moveSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"entered {other.gameObject.name}");
            IDamagable damagableObj = other.GetComponent<IDamagable>();
            if(damagableObj != null)
                damagableObj.TakeDamage(damage, elementData);
            Destroy(this.gameObject);
        }
    }
}