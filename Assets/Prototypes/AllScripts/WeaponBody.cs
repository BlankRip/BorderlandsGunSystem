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
		{                                                                                // going thru all statistics of all weapon parts

            rawRarity += (int)part.rarityLevel;


			foreach (KeyValuePair<WeaponStatType, float> statType in part.stats)         //for each weapon part, looping thru all statistics available in the dictionary
            {

                weaponStats.Add(statType.Key, statType.Value);                          //saving them in a dictionary

                //Debug.Log(statType.Key);
                //Debug.Log(statType.Value);

            }

        }

    }

    void OutlineSetter()
    {
        foreach(WeaponParams weaponPart in weaponParts)
        {
            Outline outlineWeaponPart = weaponPart.GetComponent<Outline>();
            outlineWeaponPart.OutlineColor = Color.yellow;
        }
    }


    void DetermineRarity()
    {

        int averageRarity = rawRarity / weaponParts.Count;         // taking out average Rarity
        averageRarity = Mathf.Clamp(averageRarity, 0, weaponParts.Count);
        rarityLevel = (RarityLevel)averageRarity;                 // setting average rarity from int to RarityLevel which is enum

        OutlineSetter();

        //Debug.Log(rarityLevel);
    }


}
