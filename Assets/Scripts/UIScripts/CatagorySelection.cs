using System;
using System.Collections;
using System.Collections.Generic;
using DoozyUI;
using UnityEngine;
using UnityEngine.UI;


public class CatagorySelection : MonoBehaviour
{
    public GameObject rowTemplate;

    private Transform rowToPopulate;
    private int rowcount;
    private Button button;

    void Start()
    {
        rowToPopulate = GameObject.FindGameObjectWithTag("RowOrganizer").transform;


        //This will throw a nullrefrence errors since DoozyUI has the object inactive at start
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Call);

    }

    void Update()
    {
    }

    private void Call()
    {
        foreach (Transform cell in rowToPopulate)
        {
            var row = cell.GetComponent<IsRowUsed>();

            if (row.isUsed == false)
            {
                GameObject newItem = Instantiate(rowTemplate, transform.position, Quaternion.identity, cell.transform);
                newItem.name = gameObject.name;
                newItem.GetComponent<MenuPopulate>().rowID = row.rowID;

                row.isUsed = true;
                break;
            }
        }
    }


}
