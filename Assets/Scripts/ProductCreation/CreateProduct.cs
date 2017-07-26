using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodManager;
using ItemSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class CreateProduct : MonoBehaviour
{
    //All of the lists that we will use to create the product, *Remember to add an entry into ClearList() if you add a new list*
    public static List<GameObject> selection = new List<GameObject>();
    private GameObject[] organizedSelection = new GameObject[7];
    private List<Sprite> productsprites = new List<Sprite>();

    private static List<float> allValues;
    private Text stats;
    private Transform rowOrganizer;
    private Transform newProduct;
    private int largestRow = 0;
    private int order = -1;

    private int nullCount;
    private static int touchCount;

    //Oue fields for the final product
    private float totalValue;

    public static int TouchCount { get { return touchCount; } set { touchCount = value; } }

    //This function will take all the data store in the selection list (from IngredientMembers) and create a combined product. Create a new gameobject from it, and add to the database.
    //TODO combine all the sprites, values, combo,names etc and create a new prefab from it and put it into the database. Make sure to add the IngredientMembers script so we can store members

    void Start()
    {
        allValues = new List<float>();
        stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Text>();
        rowOrganizer = GameObject.FindWithTag("RowOrganizer").transform;
        newProduct = GameObject.FindWithTag("NewProduct").transform;
    }

    void Update()
    {
        //Move the items together after a delay, use a coroutine
    }

    public void NewProduct()
    {
        //Call this to get updated values in the list
        MembersFromSelection();
        OrganizeSelections();

        //Very important we delete the row AFTER we organize the list into a new array. If we dont delete the rows the collider will get in the way of the create screen.
        DeleteCells();

        EditOrganizedSelection();
        CloneSelection();
        

        //Utility for now
        DisplayValues();
        

        totalValue = Calculate.ProductValue(allValues.ToArray());
        stats.text = "Your Product has a value of: \n" + "$" + totalValue;
    }

    //The Main functions that allow us to make the actual product.

    void CloneSelection()
    {
        foreach (var original in organizedSelection)
        {
            if (original != null)
            {
                nullCount++;

                GameObject clone = Instantiate(original, transform.position, Quaternion.identity, newProduct);

                //TODO All the data manipulation for the new clone, put this in a Function later
                
            }
        }
    }

    void OrganizeSelections()
    {
        foreach (var item in selection)
        {
            if (item != null)
            {
                var row = item.GetComponent<IngredientMembers>().RowSource;
                organizedSelection[row] = item;

                if (row > largestRow)
                {
                    largestRow = row;
                }
            }
        }
    }

    //This Coroutine is to check if we have squeezed everything together. *Not in use as of 0.1.0.8*
    IEnumerator CheckSqueeze()
    {
        yield return new WaitForSeconds(1);

        Debug.Log("Null count:" + nullCount + "\nTouch count: " + touchCount);

        if (touchCount == nullCount)
        {
            SceneManager.LoadScene("NewProduct");
        }

        
    }

    IEnumerator TransformPosition()
    {
        yield return new WaitForSeconds(3);
        //Move all children to a center spot?



    }

    //Utility functions, testing etc
    public void DisplayValues()
    {
        //First we need to get all the member variables from the selections

        foreach (var item in organizedSelection)
        {
            if (item != null)
            {
                var component = item.GetComponent<IngredientMembers>();

                var name = component.Name;
                var rowID = component.RowSource;
                var value = component.Value;

                Debug.Log(name + " Was added to " + rowID + " with a value of " + value + " \nObject: " + item); 
            }
        }
    }


    //Management for the prodcut creation so when we go to make a new one it is empty.
    public void ClearList()
    {
        //Delete the rows for a fresh start
        DeleteRows();

        //We need to make sure ALL lists are cleared, not just the list of GameObjects in selection. We only really need to trim the List of GameObjects for now
        ResetList(selection);
        ResetList(productsprites);
        ResetOrganizedSelections();
        //Clear all the other lists for fresh information
        allValues.Clear();
        largestRow = 0;
        nullCount = 0;
        touchCount = 0;

        //Set all the cells "isUsed" Values to false
        ChangeIsUsed();
    }

    void DeleteRows()
    {
        var row = GameObject.FindGameObjectsWithTag("DatabaseRow");
        var items = GameObject.FindGameObjectsWithTag("DatabaseItem");

        foreach (var item in row)
        {
            Destroy(item);
        }
        foreach (var item in items)
        {
            Destroy(item);
        }


    }

    void DeleteCells()
    {
        var row = GameObject.FindGameObjectsWithTag("DatabaseRow");

        foreach (var item in row)
        {
            Destroy(item);
        }
    }

    void ChangeIsUsed()
    {
        foreach (Transform cell in rowOrganizer)
        {
            var row = cell.GetComponent<IsRowUsed>();
            row.isUsed = false;
        }
    }

    void ResetList<T>(List<T> list)
    {
        list.Clear();
        list.TrimExcess();
    }

    void ResetOrganizedSelections()
    {
        for (int i = 0; i < organizedSelection.Length; i++)
        {
            organizedSelection[i] = null;
        }
    }


    //Call this to update the values of the selections
    private void MembersFromSelection()
    {
        foreach (var item in selection)
        {
            var store = item.GetComponent<IngredientMembers>();

            allValues.Add(store.Value);
            productsprites.Add(store.Sprite);
        }
    }

    void EditOrganizedSelection()
    {
        for (int i = 0; i < organizedSelection.Length; i++)
        {
            if (organizedSelection[i] != null)
            {
                order -= i;
                organizedSelection[i].layer = 8;
                organizedSelection[i].AddComponent<Canvas>().overrideSorting = true;
                organizedSelection[i].AddComponent<GraphicRaycaster>();
                organizedSelection[i].GetComponent<Canvas>().sortingLayerName = "NewProduct";
                organizedSelection[i].GetComponent<Canvas>().sortingOrder = order;
            }
        }
    }

}
