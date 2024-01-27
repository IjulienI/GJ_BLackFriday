using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject settings;
    private EventSystem eventSystem;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    public void GoToPlayerSelection()
    {
        SceneManager.LoadScene("MainMenu 1");
    }
    public void Settings()
    {
        main.SetActive(false);
        settings.SetActive(true);
        GameObject obj = settings.transform.GetChild(0).gameObject;
        obj.GetComponent<Button>().Select();
    }

    public void PauseSettings()
    {
        main.SetActive(false);
        settings.SetActive(true);
        GameObject obj = settings.transform.GetChild(1).transform.GetChild(0).gameObject;
        obj.GetComponent<Button>().Select();
    }

    public void ChangeScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Resume()
    {
        main.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Return()
    {
        settings.SetActive(false);
        main.SetActive(true);
        GameObject obj = main.transform.GetChild(1).gameObject;
        obj.GetComponent<Button>().Select();
    }

    public void PauseReturn()
    {
        settings.SetActive(false);
        main.SetActive(true);
        GameObject obj = main.transform.GetChild(1).transform.GetChild(1).gameObject;
        obj.GetComponent<Button>().Select();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
