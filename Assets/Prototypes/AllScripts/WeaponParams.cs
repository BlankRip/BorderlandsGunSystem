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

    [System.Serializable]
    public class WeaponStatPair
    {
        public WeaponStatType statType;


        public float minStatValue;
        public float maxStatValue;
    }


	public List<WeaponStatPair> rawStats;
    public Dictionary<WeaponStatType, float> stats = new Dictionary<WeaponStatType , float>();


    private void Awake()
    {
        foreach(WeaponStatPair statPair in rawStats)
        {
            float chosenValue = Random.Range(statPair.minStatValue, statPair.maxStatValue);
            stats.Add(statPair.statType, chosenValue);
            //Debug.Log(chosenValue);

        }
    }





}
