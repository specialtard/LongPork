using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;
using ItemSystem;
using ItemSystem.Database;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class MenuPopulate : MonoBehaviour

{

    private List<ItemBase> database = new List<ItemBase>();
    private GameObject databaseItem;
    private IsRowUsed cellID;

    public int rowID;

    void Start()
    {
        
        PopulateRow();
    }

    void Update()
    {
        UpdateRow();        
    }


    void PopulateRow()
    {
        var rowName = gameObject.name;
        //Enum.Parse(typeof(ItemType), name);
        database = ItemSystemUtility.GetAllTypeItems<ItemBase>((ItemType) Enum.Parse(typeof(ItemType), rowName));

        //Load the database item at runtime.
        databaseItem = Resources.Load("DatabaseItem") as GameObject;

        foreach (var item in database)
        {
            var scrollScript = gameObject.GetComponent<HorizontalScrollSnap>();
            AssignContentRect();

            var sprite = item.itemSprite;
            var name = item.itemName;
            var description = item.itemDescription;
            var isUnlocked = item.isUnlocked;
            var value = item.itemValue;


            if (item.isUnlocked)
            {
                GameObject instance =
                    Instantiate(databaseItem,
                        transform.position,
                        Quaternion.identity);

                //Here we can pass the unique values of the database, and others, into the component so each one will be unique.
                IngredientMembers add = instance.GetComponent<IngredientMembers>();

                add.RowSource = rowID;
                add.Name = item.itemName;
                add.Description = item.itemDescription;
                add.IsUnlocked = item.isUnlocked;
                add.Value = item.itemValue;
                add.Sprite = item.itemSprite;

                scrollScript.AddChild(instance, false);
                scrollScript.StartingScreen = 1;

                instance.GetComponent<Image>().sprite = sprite;
                instance.name = name;
            }

        }
    }

    void AssignContentRect()
    {
        //Get refrence to ScrollRect that the script is attached to
        var scrollRect = gameObject.GetComponent<ScrollRect>();
        var content = gameObject.transform.GetChild(0);
        scrollRect.content = content.GetComponent<RectTransform>();

    }

    void UpdateRow()
    {
        if (transform.parent != null)
        {
            cellID = GetComponentInParent<IsRowUsed>();
            rowID = cellID.rowID;
        }
    }
}
