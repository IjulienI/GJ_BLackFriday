using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerListScript : MonoBehaviour
{
    public static PlayerListScript Instance;

    [SerializeField] private TextMeshProUGUI[] listTexts;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject[] listLines;
    [SerializeField] private GameObject listGO;
    [SerializeField] private GameObject lineListGO;
    [SerializeField] private int timer;

    private List<GameObject> itemInStore = new List<GameObject>();
    private List<GameObject> itemInList = new List<GameObject>();
    private List<GameObject> ItemGet = new List<GameObject>();
    private bool displayList;


    private void Start()
    {
        Instance = this;

        RayonScript[] rayonInLevel = Object.FindObjectsOfType<RayonScript>();

        for(int i = 0; i< rayonInLevel.Length; i++)
        {
            itemInStore.Add(rayonInLevel[i].GetItemShelf());
        }

        GameTimer();
        MakeNewList();
    }
    private void Update()
    {
        if(displayList)
        {
            listGO.transform.localPosition = Vector3.Slerp(listGO.transform.localPosition, new Vector3(0f, -267f, 0f), 5 * Time.deltaTime);
            lineListGO.transform.localPosition = Vector3.Slerp(listGO.transform.localPosition, new Vector3(0f, -267f, 0f), 5 * Time.deltaTime);
        }
        else
        {
            listGO.transform.localPosition = Vector3.Slerp(listGO.transform.localPosition, new Vector3(0f, -790f, 0f), 5 * Time.deltaTime);
            lineListGO.transform.localPosition = Vector3.Slerp(listGO.transform.localPosition, new Vector3(0f, -790f, 0f), 5 * Time.deltaTime);
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
       for(int i = 0; i < itemInList.Count; i++)
        {
            if (itemInList[i] == actualObject)
            {
                listLines[i].SetActive(true); break;
            }
        }
    }

    private void GameTimer()
    {
        if(timer >= 1)
        {
            timer--;
            timerText.text = "Temps restant : " + timer;
            Invoke(nameof(GameTimer), 1);
        }
        else
        {
            SceneManager.LoadScene("MainMenu 1");
        }
    }

    private void MakeNewList()
    {
        for (int i = 0; i < listTexts.Length; i++)
        {
            int randomItem = Random.Range(0, itemInStore.Count);

            while (itemInList.Contains(itemInStore[randomItem]))
            {
                randomItem = Random.Range(0, itemInStore.Count);
            }
            if (!itemInList.Contains(itemInStore[randomItem]))
            {
                itemInList.Add(itemInStore[randomItem]);
            }
            listTexts[i].SetText(itemInStore[randomItem].GetComponent<ItemScript>().GetName());
        }
    }
}
