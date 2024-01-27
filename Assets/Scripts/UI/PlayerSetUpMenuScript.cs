using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
public class PlayerSetUpMenuScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI oldName;
    [SerializeField] private Image characterImage;
    [SerializeField] private Slider readySlider;
    [SerializeField] private Sprite[] characterUI;

    private List<string> old = new List<string> { "Sunday", "Marie-Francoise", "Danielle", "Anicette", "M�lo", "Gaston", "Maurice", "Ren�", "Albert", "Georges", "Henri", "L�on", "Marcel", "Roger", "Andr�", "Raymond", "Louis", "�mile", "Paul", "Bernard", "Gilbert", "Antoine", "Victor", "Fran�ois", "Jacques", "�douard", "Fernand", "Lucien", "Michel", "Roland", "Gustave", "Jules", "Armand", "Pierre", "Charles", "Robert", "Jean", "Yves", "Jean-Pierre", "Hubert", "Claude", "Alain", "Serge", "No�l", "Michel", "Romain", "Gaston", "Henri", "Maurice", "L�on", "Paul", "Robert", "Jacques", "Ren�", "Andr�", "Lucien", "Georges", "Bernard", "Roger", "Albert", "�mile", "Gilbert", "Antoine", "Raymond", "Fran�ois", "Victor", "Yves", "Armand", "Jules", "Louis", "Michel", "Charles", "Roland", "Fernand", "Jean", "Claude", "Gustave", "Hubert", "Jean-Pierre", "No�l", "Romain", "Alain", "Pierre", "Serge", "Ren�", "Marcel", "Paul", "Andr�", "Maurice", "Robert", "L�on", "Jacques", "Georges", "Bernard", "Lucien", "Roger", "Antoine", "Raymond", "Victor", "Fran�ois", "Yves", "Armand", "Jules", "Louis", "Michel", "Charles", "Roland", "Fernand", "Jean", "Claude", "Gustave", "Hubert", "Jean-Pierre", "No�l", "Romain", "Alain", "Pierre", "Serge", "Germaine", "Simone", "Marguerite", "Yvonne", "Madeleine", "Marie", "Suzanne", "Denise", "Monique", "Paulette", "Colette", "Th�r�se", "Jacqueline", "Raymonde", "Lucienne", "�lisabeth", "Odette", "Andr�e", "Ginette", "H�l�ne", "Juliette", "Josette", "Annie", "Fran�oise", "Solange", "Claudine", "Ren�e", "L�a", "Gilberte", "Aline", "Alice", "Margot", "Juliette", "L�onie", "Josiane", "�velyne", "Rosalie", "Ir�ne", "Charlotte", "Gertrude", "�milie", "Rachel", "�lodie", "Isabelle", "Monique", "Suzanne", "Th�r�se", "Gilberte", "Huguette", "Margot", "Andr�e", "Paulette", "Gilberte", "Odile", "Marguerite", "Simone", "Yvette", "Marthe", "L�onne", "H�l�ne", "Denise", "Marcelle", "�lise", "Lise", "Lucette", "Rose", "Cl�mence", "Aurore", "Clarisse", "Antoinette", "Alice", "Lucile", "Gertrude", "Lorraine", "Laurette", "Jacqueline", "Ir�ne", "Brigitte", "L�ontine", "C�cile", "�milienne", "�dith", "Sophie", "Fran�oise", "Bernadette", "H�l�ne", "Rosalie", "Marguerite", "Henriette", "Laurence", "Blanche", "Solange", "Cl�mence", "No�mie", "�va", "Adrienne", "Isabelle", "Margot", "Gertrude", "Juliette" };
    private int playerIndex, characterSelected;
    private float ignoreInputTime = 0.5f;
    private bool inputEnable, playerReady;

    public void SetPlayerIndex(PlayerInput pi) 
    {
        int oldIndex = Random.Range(0, old.Count);
        oldName.text = old[oldIndex];
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
