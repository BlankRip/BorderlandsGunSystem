using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Player;
using Gameplay.Guns;

namespace Gameplay
{
    public class LocalBlackboard : MonoBehaviour
    {
        public static LocalBlackboard instance;

        [HideInInspector] public Camera playerCamera;
        [HideInInspector] public SimplePlayer_FPS localPlayerScript;
        [HideInInspector] public GameObject localPlayerGo;
        [HideInInspector] public AmmoSupply localAmmoSupply;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.Log("<color=red>Second instance of LocalBlackboard was deleted</color>");
                Destroy(this);
            }
        }

        private void Start()
        {
            playerCamera = Camera.main;
        }
    }
}