using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponParams : MonoBehaviour
{


	public enum WeaponStatType
    {
       Damage,
        RoF,
        MagazineSize,
        Accuracy,
        ReloadSpeed
    }


    public class WeaponStatPair
    {
        public WeaponStatType statType;


        public float minStatValue;
        public float maxStatValue;
    }


	public List<WeaponStatPair> rawStats;


    private void Awake()
    {
        foreach(WeaponStatPair statPair in rawStats)
        {
            float chosenValue = Random.Range(statPair.minStatValue, statPair.maxStatValue);
            Debug.Log(chosenValue);

        }
    }





}
