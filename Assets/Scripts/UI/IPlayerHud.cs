using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UI {
    public interface IPlayerHud
    {
        void SetAmmoInClipText(int _amount);
        void SetAvailableAmmoText(int _amount);
        void SetGunModeText(string _name);
    }
}