using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;
using ItemSystem;
using ItemSystem.Database;
using UnityEngine.UI;

public class RowCreator : MonoBehaviour

{
    public GameObject row;

    private string[] allTypes = Enum.GetNames(typeof(ItemType));
    private string buttonName;

    void Start()
    {
        CreateRows();
    }

    //Methods start here

    void CreateRows()
    {

        //Get the names of the enum ItemTypes created in ItemBaset
        for (int i = 0; i < allTypes.Length; i++)
        {
            //Instantiate a new row template based on the public row GameObject and then name it so we can modify it as necessary
            GameObject newRow = 
                Instantiate(row, 
                transform.position, 
                Quaternion.identity,
                gameObject.transform);

            newRow.name = allTypes[i];
            newRow.transform.Find("Item").GetComponent<Text>().text = allTypes[i];

        }

        
    }

}










//Proof of concept
/*
 * List<ItemBase> bunDatabase = new List<ItemBase>();
        List<Sprite> bunSprite = new List<Sprite>();
        List<Dropdown.OptionData> bunOptions = new List<Dropdown.OptionData>();


        bunDatabase = ItemSystemUtility.GetAllTypeItems<ItemBase>(ItemType.BurgerBuns);


        for (int i = 0; i < bunDatabase.Count; i++)
        {
            var bunImage = bunDatabase[i].itemSprite;
            var bunName = bunDatabase[i].itemName;

            Dropdown.OptionData temp = new Dropdown.OptionData(bunName,bunImage);
            bunOptions.Add(temp);
            
        }

        bunRow = gameObject.GetComponent<Dropdown>();
        bunRow.AddOptions(bunOptions);
        */



/*
 for (int i = 0; i < bunDatabase.Count; i++)
    {

        var bunImage = bunDatabase[i].itemSprite;
        var bunName = bunDatabase[i].itemName;
        var bunDescription = bunDatabase[i].itemDescription;

        //Create the object and place it under the viewport in the hierarchy
        Debug.Log("Creating new item");
        Instantiate(prefab, transform.position, Quaternion.identity, content);

        transform.GetComponent<Image>().sprite = bunImage;
    }
 */

