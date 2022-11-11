using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class RandomPicker : MonoBehaviour
    {
        [SerializeField] bool attachAsChild;
        [SerializeField] List<GameObject> itemList;

        private void Start() {
            if(itemList.Count != 0) {
                GameObject pickedObj = Instantiate(itemList[Random.Range(0, itemList.Count)], transform.position, Quaternion.identity);
                if(attachAsChild) {
                    pickedObj.transform.parent = this.transform;
                    pickedObj.transform.localPosition = Vector3.zero;
                }
            }

            Destroy(this);
        }
    }
}