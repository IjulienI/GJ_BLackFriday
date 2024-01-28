using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListScript : MonoBehaviour
{
    public static PlayerListScript Instance;

    [SerializeField] private TextMeshProUGUI[] listTexts;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider[] listLines;
    [SerializeField] private GameObject listGO;

    private GameObject[] listObjects;
    private List<GameObject> itemInList = new List<GameObject>();
    private List<GameObject> ItemGet = new List<GameObject>();
    private bool displayList;
    private int timer;

    private void Start()
    {
        Instance = this;

        RayonScript[] rayonInLevel = Object.FindObjectsOfType<RayonScript>();

        for(int i = 0; i< rayonInLevel.Length; i++)
        {
            listObjects.SetValue(rayonInLevel[i].GetItemShelf(), i);
        }

        MakeNewList();
    }
    private void Update()
    {
        if(displayList)
        {
            listGO.transform.localPosition = Vector3.Slerp(listGO.transform.localPosition, new Vector3(0f, -267f, 0f), 5 * Time.deltaTime);
        }
        else
        {
            listGO.transform.localPosition = Vector3.Slerp(listGO.transform.localPosition, new Vector3(0f, -790f, 0f), 5 * Time.deltaTime);
        }
    }

    public void DisplayList()
    {
        CancelInvoke(nameof(ToggleList));
        displayList = true;
        Invoke(nameof(ToggleList), 2.5f);
    }
    private void ToggleList()
    {
        displayList = false;
    }

    public bool ListContain(GameObject actualObject)
    {
        if (itemInList == ItemGet)
        {
            MakeNewList();
        }
        if (itemInList.Contains(actualObject) && !ItemGet.Contains(actualObject))
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

    }

    private void GameTimer()
    {
        timer++;
        timerText.text = "Temps restant : " + timer;
    }

    private void MakeNewList()
    {
        for (int i = 0; i < listTexts.Length; i++)
        {
            int randomItem = Random.Range(0, listObjects.Length);

            while (itemInList.Contains(listObjects[randomItem]))
            {
                randomItem = Random.Range(0, listObjects.Length);
            }
            if (!itemInList.Contains(listObjects[randomItem]))
            {
                itemInList.Add(listObjects[randomItem]);
            }
            listTexts[i].SetText(listObjects[randomItem].GetComponent<ItemScript>().GetName());
        }
    }
}
