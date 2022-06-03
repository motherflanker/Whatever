using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
  
    public GameObject[] taggedObjects;

    public void Start()
    {
        taggedObjects = GameObject.FindGameObjectsWithTag("GameCanvas");
        if(taggedObjects != null)
        {
            foreach (GameObject tagged in taggedObjects)
            {
                 tagged.SetActive(false);
            }
        }   
    }

    public void PlayPressed()
    {
        SceneManager.LoadScene("SampleScene");
        /*
        if (taggedObjects != null)
        {
            foreach (GameObject tagged in taggedObjects)
            {
                tagged.SetActive(true);
            }
        }*/
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
