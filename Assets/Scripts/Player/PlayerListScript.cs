using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListScript : MonoBehaviour
{
    public static PlayerListScript Instance;

    [SerializeField] private TextMeshProUGUI[] listTexts;
    [SerializeField] private Slider[] listLines;
    [SerializeField] private GameObject[] listObjects;
    [SerializeField] private GameObject listGO;

    private List<GameObject> itemInList = new List<GameObject>();
    private List<GameObject> ItemGet = new List<GameObject>();
    private bool displayList;

    private void Start()
    {
        Instance = this;

        for (int i = 0; i < listTexts.Length; i++)
        {
            int randomItem = Random.Range(0, listObjects.Length);
            if (!itemInList.Contains(listObjects[randomItem]))
            {
                itemInList.Add(listObjects[randomItem]);
            }
            listTexts[i].SetText(listObjects[randomItem].name);
        }
    }
    private void Update()
    {
        if(displayList)
        {
            listGO.transform.localPosition = new Vector3(0f, -267f, 0f);
        }
        else
        {
            listGO.transform.localPosition = new Vector3(0f, -790f, 0f);
        }
    }

    public void DisplayList()
    {
        displayList = true;
        Invoke(nameof(ToggleList), 1.5f);
    }
    private void ToggleList()
    {
        displayList = false;
    }

    public bool ListContain(GameObject actualObject)
    {
        if(itemInList.Contains(actualObject) && !ItemGet.Contains(actualObject))
        {
            ItemGet.Add(actualObject);
            DrawRayOnItems(actualObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DrawRayOnItems(GameObject actualObject)
    {
        for(int i = 0; i < listTexts.Length; i++) 
        { 

        }
    }
}
