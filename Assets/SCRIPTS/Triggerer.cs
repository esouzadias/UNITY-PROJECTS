using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerer : MonoBehaviour
{
    public List<GameObject> objectsToHide;
    public List<GameObject> objectsToShow;
    // Start is called before the first frame update
    void Start()
    {
        /* objectsToHide = FindObjectsOfType(typeof(GameObject)) as GameObject[]; */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            foreach(GameObject objectToHide in objectsToHide)
            {
                objectToHide.SetActive(false);
            }
            foreach(GameObject objectToShow in objectsToShow)
            {
                objectToShow.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            foreach(GameObject objectToHide in objectsToHide)
            {
                objectToHide.SetActive(true);
            }
            foreach(GameObject objectToShow in objectsToShow)
            {
                objectToShow.SetActive(false);
            }
        }
    }
}
