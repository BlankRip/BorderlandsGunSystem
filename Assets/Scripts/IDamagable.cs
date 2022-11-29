using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Guns;

namespace Gameplay {
    public interface IDamagable
    {
        void TakeDamage(float _dmgAmount, ElementData _elemData);
    }
}