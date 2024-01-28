using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private Transform[] playerSpawns;
    [SerializeField] private GameObject[] playerPrefab;
    [SerializeField] private GameObject[] playerHud;
    [SerializeField] private Sprite[] playerIcon;

    private int playerLenght;
    void Start()
    {
        playerLenght = PlayerConfigManager.Instance.GetPlayerConfigs().Count;
        for (int i = 0; i < playerHud.Length; i++)
        {
            playerHud[i].SetActive(false);
        }
        Invoke(nameof(GetPlayerRefs), 0.1f);
    }
    private void GetPlayerRefs()
    {
        var playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs();
        for (int i = 0; i < playerLenght; i++)
        {

            if (playerConfigs[i] != null)
            {
                var player = Instantiate(playerPrefab[playerConfigs[i].CharacterSelection], playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
                player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
                InitializeHUD(i, playerConfigs[i]);
            }
        }
    }

    private void InitializeHUD(int i, PlayerConfiguration player)
    {
        //playerHud[i].SetActive(true);
        //playerHud[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerIcon[player.CharacterSelection];
        //playerHud[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = player.CharacterName;
    }

}
