using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class spawnPlayerSetupMenu : MonoBehaviour
{
    [SerializeField] private GameObject playerSetupMenuPrefab;
    [SerializeField] private  PlayerInput input;

    private PlayerSetUpMenuScript menu;
    private void Start()
    {
        var rootMenu = GameObject.Find("PlayerSetupMenu");
        if (rootMenu != null)
        {
            var newMenu = Instantiate(playerSetupMenuPrefab, rootMenu.transform);
            menu = newMenu.GetComponent<PlayerSetUpMenuScript>();
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.SetPlayerIndex(input);
        }

    }

    private void Update()
    {
        if (input.actions["Interact"].triggered)
        {
            if(menu.gameObject.activeInHierarchy)
            {
                menu.ReadyPlayer();
            }
            else
            {
                menu.gameObject.SetActive(true);
            }

        }

        if(input.actions["Navigate"].triggered)
        {
            if(menu.gameObject.activeInHierarchy)
            {
                if(input.actions["Navigate"].ReadValue<Vector2>().x < 0)
                {
                    menu.SelectCharacterLeft();
                }
                if(input.actions["Navigate"].ReadValue<Vector2>().x > 0)
                {
                    menu.SelectCharacterRight();
                }
               
            }
            else
            {
                menu.gameObject.SetActive(true);
            }
        }
        if (input.actions["Cancel"].triggered)
        {
            if(menu.gameObject.activeInHierarchy)
            {
                menu.NotReady();
            }
        }
    }
}
