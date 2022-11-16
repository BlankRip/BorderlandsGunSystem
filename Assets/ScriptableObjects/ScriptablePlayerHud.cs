using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UI {
    [CreateAssetMenu()]
    public class ScriptablePlayerHud : ScriptableObject
    {
        public IPlayerHud hud;
    }
}