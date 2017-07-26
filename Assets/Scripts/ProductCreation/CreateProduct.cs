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
    private bool finished = false;
    private static int touchCount;
    private Animator anim;
    private AudioSource audio;
    private static bool isDone = false;
    private ParticleSystem[] particle;

    public static bool IsDone { get { return isDone; } set { isDone = value; } }
    public static int TouchCount { get { return touchCount; } set { touchCount = value; } }

    //This function will take all the data store in the selection list (from IngredientMembers) and create a combined product. Create a new gameobject from it, and add to the database.
    //TODO combine all the sprites, values, combo,names etc and create a new prefab from it and put it into the database. Make sure to add the IngredientMembers script so we can store members

    void Awake()
    {
        allValues = new List<float>();
        stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Text>();
        rowOrganizer = GameObject.FindWithTag("RowOrganizer").transform;
        newProduct = GameObject.FindWithTag("NewProduct").transform;
        anim = newProduct.GetComponent<Animator>();
        audio = newProduct.GetComponent<AudioSource>();
        particle = FindObjectsOfType<ParticleSystem>();
    }

    void Start()
    {
    }

    void Update()
    {
        PlayAfterAnimation(audio,audio.clip);
    }

    public void NewProduct()
    {
        //Call this to get updated values in the list
        MembersFromSelection();
        OrganizeSelections();

        //Very important we delete the row AFTER we organize the list into a new array. If we dont delete the rows the collider will get in the way of the create screen.
        //DeleteCells();

        EditOrganizedSelection();
        CloneSelection();

        //Utility for now
        DisplayValues();

        anim.SetBool("DroppedDown", true);
        
    }

    //The Main functions that allow us to make the actual product.

    void CloneSelection()
    {
        for (int i = 0; i < organizedSelection.Length; i++)
        {
            if (organizedSelection[i] != null)
            {
                GameObject clone = Instantiate(organizedSelection[i], transform.position, Quaternion.identity, newProduct);
                var rect = clone.GetComponent<RectTransform>();

                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.anchorMin = new Vector2(0.5f, 0.5f);

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

    void PlayAfterAnimation(AudioSource source, AudioClip audio)
    {
        if (!isDone)
        {
            source.PlayDelayed(0.20f);
            StartCoroutine("EnableParticles");
            isDone = true;
        }

    }

    IEnumerator EnableParticles()
    {
        yield return new WaitForSeconds(0.4f);

        foreach (var item in particle)
        {
            item.GetComponent<ParticleSystemRenderer>().enabled = true;
        }
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
        anim.SetBool("DroppedDown", false);

        //We need to make sure ALL lists are cleared, not just the list of GameObjects in selection. We only really need to trim the List of GameObjects for now
        ResetList(selection);
        ResetList(productsprites);
        ResetOrganizedSelections();
        //Clear all the other lists for fresh information
        allValues.Clear();
        largestRow = 0;
        touchCount = 0;
        isDone = false;
        
        //Set all the cells "isUsed" Values to false
        ChangeIsUsed();
        enabled = false;
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
        foreach (var item in particle)
        {
            item.GetComponent<ParticleSystemRenderer>().enabled = false;
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
