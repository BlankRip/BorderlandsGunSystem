using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UI {
    [CreateAssetMenu(fileName = "Player HUD", menuName = "ScriptableItems/Player HUD")]
    public class ScriptablePlayerHud : ScriptableObject
    {
        public IGunHud gunHud;
        public IHealthHud healthHud;
    }
}