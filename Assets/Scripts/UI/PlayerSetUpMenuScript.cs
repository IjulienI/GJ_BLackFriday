using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
public class PlayerSetUpMenuScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image characterImage;
    [SerializeField] private Slider readySlider;
    [SerializeField] private Sprite[] characterUI;

    private int playerIndex, characterSelected;
    private float ignoreInputTime = 0.5f;
    private bool inputEnable, playerReady;

    public void SetPlayerIndex(PlayerInput pi) 
    {
        playerIndex = pi.playerIndex;
        titleText.SetText("Player " + (pi.playerIndex + 1));
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    private void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnable = true;
        }

        if(playerReady)
        {
            if(readySlider.value <= 1)
            {
                readySlider.value += Time.deltaTime * 5;
            }        
        }
        else
        {
            if (readySlider.value >= 0)
            {
                readySlider.value -= Time.deltaTime * 5;
            }
        }
    }


    public void SelectCharacterLeft()
    {
        if (!inputEnable || playerReady) { return; }

            if(characterSelected >= characterUI.Length-1)
            {
                characterSelected = 0;
            }
            else
            {
                characterSelected += 1;
            }

        characterImage.sprite = characterUI[characterSelected];
        PlayerConfigManager.Instance.SetPlayerCharacter(playerIndex, characterSelected);
    }
    public void SelectCharacterRight()
    {
        if (!inputEnable || playerReady) { return; }

        if (characterSelected <= 0)
        {
            characterSelected = characterUI.Length - 1;
        }
        else
        {
            characterSelected -= 1;
        }
        characterImage.sprite = characterUI[characterSelected];
        PlayerConfigManager.Instance.SetPlayerCharacter(playerIndex, characterSelected);
    }

    public void ReadyPlayer()
    {
            if (!inputEnable) { return; }

            playerReady = true;
            PlayerConfigManager.Instance.ReadyPlayer(playerIndex, true);
    }

    public void NotReady()
    {
        if (!inputEnable) { return; }

        if(playerReady)
        {
            playerReady = false;
            PlayerConfigManager.Instance.ReadyPlayer(playerIndex, false);
        }
        else
        {
            PlayerConfigManager.Instance.HandlePlayerQuit(playerIndex);
            gameObject.SetActive(false);
        }

    }
}
