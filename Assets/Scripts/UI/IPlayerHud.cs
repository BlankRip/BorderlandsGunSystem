using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UI {
    public interface IPlayerHud
    {
        void SetGunModeText(string _name);
        
        void SetADSSprite(Sprite _adsSprite);
        void ADSOverlay(bool _activeState);

        void SetAmmoInClipText(int _amount);
        void SetAvailableAmmoText(int _amount);
    }
}