using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public static EndGameScript instance;

    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject mainCanva;
    [SerializeField] private GameObject[] playerPrefab;
    [SerializeField] private GameObject[] playerHud;
    [SerializeField] private Sprite[] playerIcon;

    private int playerLenght;

    private void Start()
    {
        screen.SetActive(false);
        instance = this;
        Invoke(nameof(GetPlayerRefs), .1f);
    }

    public void On()
    {
        screen.SetActive(true);
    }

    private void GetPlayerRefs()
    {
        var playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs();
        var players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerLenght; i++)
        {
            if (playerConfigs[i] != null)
            {
                InitializeHUD(i, playerConfigs[i], players[i].GetComponent<Inventory>());
            }
        }
        mainCanva.SetActive(false);
    }

    private void InitializeHUD(int i, PlayerConfiguration player, Inventory score)
    {
        playerHud[i].SetActive(true);
        playerHud[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerIcon[player.CharacterSelection];
        playerHud[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = player.CharacterName;
        playerHud[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "score : " + score.GetScore();
    }
}