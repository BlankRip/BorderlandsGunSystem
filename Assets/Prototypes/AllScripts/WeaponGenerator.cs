using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponGenerator : MonoBehaviour
{

    public List<GameObject> bodyParts;
    public List<GameObject> magazineParts;
    public List<GameObject> gripParts;
    public List<GameObject> barrelParts;
    public List<GameObject> sightsParts;
    public List<GameObject> stockParts;


    GameObject prevWeapon;


    void Update()
    {

        StartUponPress();
        
    }


    void StartUponPress()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GenerateWeapon();
		}

	}

	void GenerateWeapon()
    {

        if(prevWeapon != null)
        {
            Destroy(prevWeapon);
        }

		GameObject randBody = GetRandomParts(bodyParts);

        GameObject instantiateBody = Instantiate(randBody, Vector3.zero, Quaternion.identity);
        WeaponBody weaponBody = instantiateBody.GetComponent<WeaponBody>();                      

        WeaponParams barrel = SpawnWeaponPart(barrelParts, weaponBody.barrelSocket);
		WeaponParams magazine = SpawnWeaponPart(magazineParts, weaponBody.magazineSocket);
		WeaponParams grip = SpawnWeaponPart(gripParts, weaponBody.gripSocket);
		WeaponParams sights = SpawnWeaponPart(sightsParts, weaponBody.sightsSocket);
		WeaponParams stock = SpawnWeaponPart(stockParts, weaponBody.stockSocket);

        weaponBody.Initialize(barrel, sights, magazine, grip, stock);

        prevWeapon = instantiateBody;
       

	}



    WeaponParams SpawnWeaponPart(List<GameObject> parts, Transform socket)
    {
        GameObject randomPart = GetRandomParts(parts);
        GameObject instantiatePart = Instantiate(randomPart, socket.transform.position, socket.transform.rotation);
        instantiatePart.transform.parent = socket;

        return instantiatePart.GetComponent<WeaponParams>();                                

    }


    //function for getting random parts
    GameObject GetRandomParts(List<GameObject> partList)
    {
        int randNumber = Random.Range(0, partList.Count);
        return partList[randNumber];
    }


}
