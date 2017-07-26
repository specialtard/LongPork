using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.Networking;

public class IngredientMembers : MonoBehaviour {

    private Sprite sprite;
    private string name;
    private string description;
    private bool isUnlocked;
    private float value;
    public int rowSource; //TODO Make this private after testing
    private MenuPopulate rowReference;
    public bool hasCollided = false; //TODO make this private after testing
    private Rigidbody2D rigidbody;

    public string Name { get; set; }
    public Sprite Sprite { get; set; }
    public string Description { get; set; }
    public bool IsUnlocked { get; set; }
    public float Value { get; set; }
    public int RowSource { get { return rowSource; } set { rowSource = value; } }
    public bool HasCollided { get { return hasCollided; } set { hasCollided = value; } }

    void Update()
    {
        UpdateRowID();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void UpdateRowID()
    {
        if (transform.parent.parent != null)
        {
            rowReference = transform.parent.parent.GetComponent<MenuPopulate>();

            if (rowReference != null)
            {
                RowSource = rowReference.rowID; 
            }
        }
    }
    
    //col is the GameObject this is colliding INTO
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "DatabaseRow")
        {
            Debug.Log("Entering a trigger for items");
            CreateProduct.selection.Add(gameObject); 
        }
        else if (col.tag == "DatabaseItem")
        {
            HasCollided = true;
            Debug.Log("Entering a trigger for items");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "DatabaseRow")
        {
            Debug.Log("Exiting a trigger for items");
            CreateProduct.selection.Remove(gameObject); 
        }
        else if (col.tag == "DatabaseItem")
        {
            Debug.Log("Exiting a trigger for items");
        }
    }
}
