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

    public enum RarityLevel
    {
        Common = 0,
        Uncommon = 1,
        Rare = 2,
        Epic = 3,
        Legendary = 4
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

    public RarityLevel rarityLevel;


    private void Awake()
    {
        foreach(WeaponStatPair statPair in rawStats)
        {
            float chosenValue = Random.Range(statPair.minStatValue, statPair.maxStatValue);
            stats.Add(statPair.statType, chosenValue);

        }
    }





}
