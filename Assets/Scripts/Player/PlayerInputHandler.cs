using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private PlayerMovement movement;

    private PlayerInput input;
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        input = pc.Input;
    }

    private void Update()
    {
        if (input.actions["Movement"].triggered)
        {
            OnMove(input.actions["Movement"].ReadValue<Vector2>());
        }
        if (input.actions["AttackRight"].triggered)
        {
            movement.AttackLeft();
        }
        if (input.actions["AttackLeft"].triggered)
        {
            movement.AttackRight();
        }
        if (input.actions["Interact"].triggered)
        {
            movement.InputBoost();
        }
        if (input.actions["shoppingList"].triggered)
        {
            PlayerListScript.Instance.DisplayList();
            movement.OpenList();
        }

        if (input.actions["Pause"].triggered)
        {

        }


    }

    public void OnMove(Vector2 newInput)
    {
        if(movement != null )
        {
            movement.InputMovement(newInput);
        }
    }
}
