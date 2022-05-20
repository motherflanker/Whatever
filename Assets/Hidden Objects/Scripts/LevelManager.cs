using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{   
    public static LevelManager instance;

    [SerializeField]
    private List<HiddenObjectData> hiddenObjectList;

    private List<HiddenObjectData> activeHiddenObjectList;

    [SerializeField]
    private int maxActiveHiddenObjectList = 5;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);
    }

    private void Start()
    {
        activeHiddenObjectList = new List<HiddenObjectData>();
        AssignHiddenObject();
    }

    void AssignHiddenObject()
    {
        activeHiddenObjectList.Clear();
        for(int i = 0; i < hiddenObjectList.Count; i++)
        {
            hiddenObjectList[i].hiddenObject.GetComponent<Collider2D>().enabled = false;    

        }
        
        int k = 0;
        while(k < maxActiveHiddenObjectList)
        {
            int randVal = Random.Range(0, hiddenObjectList.Count);
            if (hiddenObjectList[randVal].makeHidden)
            {
                hiddenObjectList[randVal].hiddenObject.name = "" + k;
                hiddenObjectList[randVal].makeHidden = true;
                hiddenObjectList[randVal].hiddenObject.GetComponent<Collider2D>().enabled = true;

                activeHiddenObjectList.Add(hiddenObjectList[randVal]);

                k++;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D tap = Physics2D.Raycast(pos, Vector3.zero);

            if(tap && tap.collider != null)
            {
                Debug.Log("Object Name:" + tap.collider.gameObject.name);
            }
        }
    }

}

[System.Serializable]
public class HiddenObjectData 
{
    public string name;
    public GameObject hiddenObject;
    public bool makeHidden;
}