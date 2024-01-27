using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs = new List<PlayerConfiguration>();
    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] string[] levelNames;

    private int countDown = 4;
    private bool countDownStart;
    public static PlayerConfigManager Instance {get; private set;}

    private void Start() 
    {
        if(Instance != null) 
        {
            Debug.Log("Try to create an another PlayerConfig INSTANCE");
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(Instance);    
        }
    }

    public void SetPlayerCharacter(int index, int character) 
    {
        if (playerConfigs[index] != null)
        {
            playerConfigs[index].CharacterSelection = character;
        }
    }

    public void ReadyPlayer(int index, bool ready) 
    {
        if (playerConfigs[index] != null)
        {
            playerConfigs[index].IsReady = ready;

            bool allPlayersReady = playerConfigs
                   .Where(player => player != null)
                   .All(player => player.IsReady);
        
            if (allPlayersReady)
            {
                CountDownStart();
            }
            else
            {
                CancelInvoke(nameof(CountDownStart));
                countDownText.text = "";
                countDown = 4;
            }

        }
    }
    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }


    public void HandlePlayerJoin(PlayerInput pi)
    {
        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Insert(pi.playerIndex, new PlayerConfiguration(pi));
        }
    }

    public void HandlePlayerQuit(int index)
    {
        Debug.Log("Playe quit " + index);
        playerConfigs[index] = null;
    }

    private void CountDownStart()
    {
        bool allPlayersReady = playerConfigs
                   .Where(player => player != null)
                   .All(player => player.IsReady);

        if (countDown != 0 && allPlayersReady)
        {
            countDown--;
            countDownText.text = countDown.ToString();
            Invoke(nameof(CountDownStart), 1);
        }
        else if (countDown == 0)
        {
            //int randomLevel = Random.Range(2, levelNames.Length - 1);
            //SceneManager.LoadScene(randomLevel);
            SceneManager.UnloadSceneAsync("MainMenu 1");
            SceneManager.LoadScene("TestPlayers");
        }
        else
        {
            CancelInvoke(nameof(CountDownStart));
        }

    }
}


public class PlayerConfiguration 
{
    public PlayerConfiguration(PlayerInput pi) 
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public int CharacterSelection { get; set; }
}
