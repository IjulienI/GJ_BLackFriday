using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs = new List<PlayerConfiguration>();
    [SerializeField] int MaxPlayer = 4;

    public static PlayerConfigManager Instance {get; private set;}

    private void Awake() 
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
        playerConfigs[index].CharacterSelection = character;
    }

    public void ReadyPlayer(int index) 
    {
        playerConfigs[index].IsReady = true;

        if(playerConfigs.Count <= MaxPlayer && playerConfigs.Count >= 2 && playerConfigs.All(p => p.IsReady == true)) 
        {
            //CE QUE TU VEUX FAIRE UNE FOIS QUE TOUS LES JOUEURS SONT READY
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Playe joined " + pi.playerIndex);
        if(pi.playerIndex != null)
        {
            print("exist");
        }
        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
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
