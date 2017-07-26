using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductCreation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnableCreation()
    {
        GetComponent<CreateProduct>().enabled = true;
    }
}
