using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Guns;

namespace Gameplay.UI {
    public interface IGunHud
    {
        void SetElementIcon(ElementType _elementType);
        void SetGunIcon(GunType _gunType);
        
        void SetGunModeText(string _name);
        
        void SetADSSprite(Sprite _adsSprite);
        void ADSOverlay(bool _activeState);

        void SetAmmoInClipText(int _amount);
        void SetAvailableAmmoText(int _amount);
    }

    public interface IHealthHud
    {
        void SetMaxHealth(float _maxHp);
        void SetMaxSheild(float _maxSheild);
        void SetHealthValue(float _value);
        void SetSheildValue(float _value);
    }
}