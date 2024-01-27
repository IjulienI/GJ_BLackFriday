using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private Transform[] playerSpawns;
    [SerializeField] private GameObject[] playerPrefab;

    private int playerLenght;
    void Start()
    {
        playerLenght = PlayerConfigManager.Instance.GetPlayerConfigs().Count-1;
        Invoke(nameof(GetPlayerRefs), 0.1f);
    }
    private void GetPlayerRefs()
    {
        var playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs();
        for (int i = 0; i <= playerLenght; i++)
        {

            if (playerConfigs[i] != null)
            {
                var player = Instantiate(playerPrefab[playerConfigs[i].CharacterSelection], playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
                player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            }
        }
    }

}
