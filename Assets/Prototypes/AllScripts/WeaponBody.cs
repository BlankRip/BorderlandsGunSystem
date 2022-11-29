using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class WeaponBody : WeaponParams
{

    public Transform barrelSocket;
    public Transform magazineSocket;
    public Transform gripSocket;
    public Transform sightsSocket;
    public Transform stockSocket;

    List<WeaponParams> weaponParts = new List<WeaponParams>();
    public Dictionary<WeaponStatType, float> weaponStats = new Dictionary<WeaponStatType, float>();

    int rawRarity = 0;

    public RarityScriptableObject raritySO;

    public void Initialize(WeaponParams barrel, WeaponParams scope, WeaponParams magazine, WeaponParams grip, WeaponParams stock)
    {
        weaponParts.Add(this);
        weaponParts.Add(barrel);
        weaponParts.Add(scope);
        weaponParts.Add(magazine);
        weaponParts.Add(grip);
        weaponParts.Add(stock);

        CalculateStats();
        DetermineRarity();

    }

    void CalculateStats()
    {
        // going thru list of all weaponParts

        foreach(WeaponParams part in weaponParts)
		{                                                                                
            rawRarity += (int)part.rarityLevel;

			foreach (KeyValuePair<WeaponStatType, float> statType in part.stats)         
            {
                weaponStats.Add(statType.Key, statType.Value);                         
            }
        }
    }

    void OutlineSetter()
    {
        foreach(WeaponParams weaponPart in weaponParts)
        {
            Outline outlineWeaponPart = weaponPart.GetComponent<Outline>();
            outlineWeaponPart.OutlineColor = raritySO.rarityColors[(int) rarityLevel];
        }
    }


    void DetermineRarity()
    {

        int averageRarity = rawRarity / weaponParts.Count;         
        averageRarity = Mathf.Clamp(averageRarity, 0, weaponParts.Count);
        rarityLevel = (RarityLevel)averageRarity;                 

        OutlineSetter();
        //Debug.Log(rarityLevel);
    }
}