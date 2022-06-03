using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private TextMeshProUGUI items;
    public static LevelManager instance;

    [SerializeField]
    private List<HiddenObjectData> hiddenObjectList;

    private List<HiddenObjectData> activeHiddenObjectList;

    [SerializeField]
    private int maxActiveHiddenObjectCount = 5;

    private int totalHiddenObjectsFound = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);
    }

    private void Start()
    {
        items = GameObject.Find("Items").GetComponent<TextMeshProUGUI>();
        activeHiddenObjectList = new List<HiddenObjectData>();
        AssignHiddenObject();
        UpdateItems();

    }

    void AssignHiddenObject()
    {
        totalHiddenObjectsFound = 0;
        activeHiddenObjectList.Clear();
        for(int i = 0; i < hiddenObjectList.Count; i++)
        {
            hiddenObjectList[i].hiddenObject.GetComponent<Collider2D>().enabled = false;    

        }
        
        int k = 0;
        while(k < maxActiveHiddenObjectCount)
        {
            int randVal = Random.Range(0, hiddenObjectList.Count);
            if (!hiddenObjectList[randVal].makeHidden)
            {
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
                // Debug.Log("Object Name:" + tap.collider.gameObject.name);
                
                tap.collider.gameObject.SetActive(false);

                for(int i = 0; i < activeHiddenObjectList.Count; i++)
                {
                    if(activeHiddenObjectList[i].hiddenObject.name == tap.collider.gameObject.name)
                    {
                        activeHiddenObjectList.RemoveAt(i);
                        break;
                    }
                }
                UpdateItems();
                totalHiddenObjectsFound++;
                
                if(totalHiddenObjectsFound >= maxActiveHiddenObjectCount)
                {
                    CompleteLevel();
                }
            }
        }
    }

    private void CompleteLevel()
    {
        var summaryLabel = FindInactiveObjectByName("LevelSummary");
        summaryLabel.gameObject.SetActive(true);
    }

    private void UpdateItems()
    {
        items.text = string.Join(", ", activeHiddenObjectList.Select(item => item.hiddenObject.name));
    }

    public void NavigateToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private GameObject FindInactiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

}

[System.Serializable]
public class HiddenObjectData 
{
    public string name;
    public GameObject hiddenObject;
    public bool makeHidden;
}