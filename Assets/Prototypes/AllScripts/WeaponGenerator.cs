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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
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

		//getting random body from a list
		//instantiating the random body
		GameObject randBody = GetRandomParts(bodyParts);
        //Instantiate(randBody, Vector3.zero, Quaternion.identity);

        //assigning body instatiate to a varialble and getting reference to WeaponBody
        GameObject instantiateBody = Instantiate(randBody, Vector3.zero, Quaternion.identity);
        WeaponBody weaponBody = instantiateBody.GetComponent<WeaponBody>();                      //to get sockets from WeaponBody

        SpawnWeaponPart(barrelParts, weaponBody.barrelSocket);
        SpawnWeaponPart(magazineParts, weaponBody.magazineSocket);
        SpawnWeaponPart(gripParts, weaponBody.gripSocket);
        SpawnWeaponPart(sightsParts, weaponBody.sightsSocket);
        SpawnWeaponPart(stockParts, weaponBody.stockSocket);

        prevWeapon = instantiateBody;
       

	}


    void SpawnWeaponPart(List<GameObject> parts, Transform socket)
    {
        GameObject randomPart = GetRandomParts(parts);
        GameObject instantiatePart = Instantiate(randomPart, socket.transform.position, socket.transform.rotation);
        instantiatePart.transform.parent = socket;
    }


    //function for getting random parts
    GameObject GetRandomParts(List<GameObject> partList)
    {
        int randNumber = Random.Range(0, partList.Count);
        return partList[randNumber];
    }


}
