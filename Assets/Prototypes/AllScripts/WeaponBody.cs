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

    public void Initialize(WeaponParams barrel, WeaponParams scope, WeaponParams magazine, WeaponParams grip, WeaponParams stock)
    {
        weaponParts.Add(barrel);
        weaponParts.Add(scope);
        weaponParts.Add(magazine);
        weaponParts.Add(grip);
        weaponParts.Add(stock);

        CalculateStats();

    }

    void CalculateStats()
    {
        // going thru list of all weaponParts

        foreach(WeaponParams part in weaponParts)
		{                                                                                // going thru all statistics of all weapon parts
			foreach (KeyValuePair<WeaponStatType, float> statType in part.stats)         //for each weapon part, looping thru all statistics available in the dictionary
            {

                weaponStats.Add(statType.Key, statType.Value);                          //saving them in a dictionary

                //Debug.Log(statType.Key);
                //Debug.Log(statType.Value);

            }

        }

    }


}
