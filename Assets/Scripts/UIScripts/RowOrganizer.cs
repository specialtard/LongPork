using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowOrganizer : MonoBehaviour
{

    public static int childcount;
	// Update is called once per frame
	void Update ()
	{
	    childcount = gameObject.transform.childCount;
		CheckRowOrder();
	}

    void CheckRowOrder()
    {
        var top = GameObject.Find("Bun");
        var bottom = GameObject.Find("BunBottom");

        if (top)
        {
            top.transform.SetAsFirstSibling();
        }
        if (bottom)
        {
            bottom.transform.SetAsLastSibling();
        }
    }

}
