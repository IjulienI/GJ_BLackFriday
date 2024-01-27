using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class spawnPlayerSetupMenu : MonoBehaviour
{
    [SerializeField] private GameObject playerSetupMenuPrefab;
    public PlayerInput input;
    private void Awake()
    {
        var rootMenu = GameObject.Find("PlayerSetupMenu");
        if (rootMenu != null)
        {
            var menu = Instantiate(playerSetupMenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetUpMenuScript>().SetPlayerIndex(input.playerIndex);
        }
        print(rootMenu);
    }
}
