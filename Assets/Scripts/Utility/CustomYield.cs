using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomYield : CustomYieldInstruction
{
    private int varA;
    private int varB;

    public int VarA { set { varA = value; } }
    public int VarB { set { varB = value; } }


    public override bool keepWaiting
    {
        get { return CheckCount(varA); }
    }

    private bool CheckCount(int varA)
    {
        if (varA == 0)
        {
            return false;
        }
        else
        {
            return false;
        }
    }
}
