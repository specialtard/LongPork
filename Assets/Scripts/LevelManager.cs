using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{


    public void LoadLevelStart(string level)
    {
        StartCoroutine(Wait(level));
        
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    IEnumerator Wait(string level)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level);
    }

    
}
