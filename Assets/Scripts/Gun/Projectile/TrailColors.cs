using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Guns.Projectile
{
    [CreateAssetMenu(fileName = "TrailColors", menuName = "Guns/Trail Colors", order = 1)]
    public class TrailColors : ScriptableObject
    {
        [SerializeField] private Color NutralElement, FireElement, CorrosionElement, ElectricElement, RadiationElement, BlastElement;

        public Color GetTrailColor(ElementType elementType)
        {
            switch (elementType)
            {
                case ElementType.Fire:
                    return FireElement;
                case ElementType.Corrosion:
                    return CorrosionElement;
                case ElementType.Electric:
                    return ElectricElement;
                case ElementType.Radiation:
                    return RadiationElement;
                case ElementType.Blast:
                    return BlastElement;
                default:
                    return NutralElement;
            }
        }
    }
}