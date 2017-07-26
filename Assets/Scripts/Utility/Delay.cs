using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour {

    public static IEnumerator SimpleDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
