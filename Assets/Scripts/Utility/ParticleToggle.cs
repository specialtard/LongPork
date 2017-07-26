using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleToggle : MonoBehaviour
{
    private ParticleSystem.EmissionModule pmodule;

    void Start()
    {
        pmodule = GetComponent<ParticleSystem>().emission;
    }

    void Update()
    {
        if (CreateProduct.IsDone)
        {
        }
    }
}
