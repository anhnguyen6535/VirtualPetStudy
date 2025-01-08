using UnityEngine;
using System.Collections.Generic;

/*
   This script is attached to Beagle_c1
*/
public class ConditionManager : MonoBehaviour
{
    [SerializeField] List<Material> listMaterials = new List<Material>();
    [SerializeField] int colorIndex;
    [SerializeField] GameObject dog;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dog.GetComponent<Renderer>().material = listMaterials[colorIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
