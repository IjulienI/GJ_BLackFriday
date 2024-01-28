using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs = new List<PlayerConfiguration>();
    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] GameObject LSRef;
    [SerializeField] string[] levelNames;

    private int countDown = 4;
    private bool countDownStart;
    private AsyncOperation async;

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

    public void SetPlayerName(int index, string name)
    {
        if (playerConfigs[index] != null)
        {
            playerConfigs[index].CharacterName = name;
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
                int ValidPlayer = 0;
                for(int i = 0; i < playerConfigs.Count; i++)
                {
                    if (playerConfigs != null)
                    {
                        ValidPlayer++;
                    }
                }
                print(ValidPlayer);
                if(ValidPlayer >= 2) 
                {
                    CountDownStart();
                }
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
            Instantiate(LSRef, transform);
            int randomLevel = Random.Range(2, 3);
            SceneManager.LoadScene(randomLevel);

            SceneManager.UnloadSceneAsync("MainMenu 1");
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
    public string CharacterName { get; set; }   
}
