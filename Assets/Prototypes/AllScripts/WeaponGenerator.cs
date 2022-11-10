using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponGenerator : MonoBehaviour
{

    public List<GameObject> bodyParts;
    public List<GameObject> magazineParts;
    public List<GameObject> gripParts;
    public List<GameObject> barrelParts;

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

		//getting random body from a list
		//instantiating the random body
		GameObject randBody = GetRandomParts(bodyParts);
        Instantiate(randBody, Vector3.zero, Quaternion.identity);

		GameObject randBarrel = GetRandomParts(barrelParts);
		Instantiate(randBarrel, Vector3.zero, Quaternion.identity);

		GameObject randMagazine = GetRandomParts(magazineParts);
		Instantiate(randMagazine, Vector3.zero, Quaternion.identity);

		GameObject randGrip = GetRandomParts(gripParts);
		Instantiate(randGrip, Vector3.zero, Quaternion.identity);


	}


    //function for getting random parts
    GameObject GetRandomParts(List<GameObject> partList)
    {
        int randNumber = Random.Range(0, partList.Count);
        return partList[randNumber];
    }


}
