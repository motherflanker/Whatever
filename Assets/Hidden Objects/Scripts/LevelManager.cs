using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{   
    public static LevelManager instance;

    [SerializeField]
    private List<HiddenObjectData> hiddenObjectList;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);
    }

}

[System.Serializable]
public class HiddenObjectData 
{
    public string name;
    public GameObject hiddenObject;
    public bool makeHidden;
}