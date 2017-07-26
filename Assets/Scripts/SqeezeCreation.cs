using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SqeezeCreation : MonoBehaviour

{
    private Vector3 center = new Vector3(0,-1000,0);
    private Vector3 anchoredPosition;
    private Vector3 anchoredCameraPosition;

    void Start()
    {

        anchoredCameraPosition = Camera.main.ScreenToWorldPoint(GetComponent<RectTransform>().position);
        anchoredPosition = GetComponent<RectTransform>().position;

        MoveToCenter();
    }



    void MoveToCenter()
    {
        //transform.position = Vector3.Lerp();
        Debug.Log(anchoredPosition.y +"\n"+ anchoredCameraPosition.y);
    }

}