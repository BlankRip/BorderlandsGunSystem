using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMode : MonoBehaviour
{
    [SerializeField] protected GunModeData normal;
    [SerializeField] protected bool copyNormalToADS;
    [SerializeField] protected Sprite adsSprite;
    [SerializeField] protected GunModeData ads;

    protected void Start() {
        if(copyNormalToADS)
            ads.CopyStats(normal);
    }
}
