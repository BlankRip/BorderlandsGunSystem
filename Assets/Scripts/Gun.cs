using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Sprite A, B;
    private IGunMode modeA, modeB;

    private void Start() {
        IGunMode[] modes = GetComponents<IGunMode>();
        if(modes.Length != 2)
            Debug.LogError("There is either less than or more than 2 gun modes attached");
        modeA = modes[0];
        modeB = modes[1];
        A = modeA.GetAdsSprite();
        B = modeB.GetAdsSprite();
    }

    public void ADS() {

    }

    public void Fire() {

    }

    public void Reload() {

    }

    public void SwithcMode() {

    }

}
