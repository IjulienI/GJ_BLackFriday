using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerSetUpMenuScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI oldName;
    [SerializeField] private Image characterImage;
    [SerializeField] private Slider readySlider;
    [SerializeField] private Sprite[] characterUI;

    private List<string> old = new List<string> { "Sunday", "Marie-Francoise", "Danielle", "Anicette", "Melo", "Gaston", "Maurice", "Rene", "Albert", "Georges", "Henri", "Leon", "Marcel", "Roger", "Andre", "Raymond", "Louis", "Emile", "Paul", "Bernard", "Gilbert", "Antoine", "Victor", "Francois", "Jacques", "Edouard", "Fernand", "Lucien", "Michel", "Roland", "Gustave", "Jules", "Armand", "Pierre", "Charles", "Robert", "Jean", "Yves", "Jean-Pierre", "Hubert", "Claude", "Alain", "Serge", "Noel", "Michel", "Romain", "Gaston", "Henri", "Maurice", "Leon", "Paul", "Robert", "Jacques", "Rene", "Andre", "Lucien", "Georges", "Bernard", "Roger", "Albert", "Emile", "Gilbert", "Antoine", "Raymond", "Francois", "Victor", "Yves", "Armand", "Jules", "Louis", "Michel", "Charles", "Roland", "Fernand", "Jean", "Claude", "Gustave", "Hubert", "Jean-Pierre", "Noel", "Romain", "Alain", "Pierre", "Serge", "Rene", "Marcel", "Paul", "Andre", "Maurice", "Robert", "Leon", "Jacques", "Georges", "Bernard", "Lucien", "Roger", "Antoine", "Raymond", "Victor", "Francois", "Yves", "Armand", "Jules", "Louis", "Michel", "Charles", "Roland", "Fernand", "Jean", "Claude", "Gustave", "Hubert", "Jean-Pierre", "Noel", "Romain", "Alain", "Pierre", "Serge", "Germaine", "Simone", "Marguerite", "Yvonne", "Madeleine", "Marie", "Suzanne", "Denise", "Monique", "Paulette", "Colette", "Therese", "Jacqueline", "Raymonde", "Lucienne", "Elisabeth", "Odette", "Andree", "Ginette", "Helene", "Juliette", "Josette", "Annie", "Francoise", "Solange", "Claudine", "Renee", "Lea", "Gilberte", "Aline", "Alice", "Margot", "Juliette", "Leonie", "Josiane", "Evelyne", "Rosalie", "Irene", "Charlotte", "Gertrude", "Emilie", "Rachel", "Elodie", "Isabelle", "Monique", "Suzanne", "Therese", "Gilberte", "Huguette", "Margot", "Andree", "Paulette", "Gilberte", "Odile", "Marguerite", "Simone", "Yvette", "Marthe", "Leonne", "Helene", "Denise", "Marcelle", "Elise", "Lise", "Lucette", "Rose", "Clemence", "Aurore", "Clarisse", "Antoinette", "Alice", "Lucile", "Gertrude", "Lorraine", "Laurette", "Jacqueline", "Irene", "Brigitte", "Leontine", "Cecile", "Emilienne", "Edith", "Sophie", "Francoise", "Bernadette", "Helene", "Rosalie", "Marguerite", "Henriette", "Laurence", "Blanche", "Solange", "Clemence", "Noemie", "Eva", "Adrienne", "Isabelle", "Margot", "Gertrude", "Juliette" };
    private int playerIndex, characterSelected;
    private float ignoreInputTime = 0.5f;
    private bool inputEnable, playerReady, hasQuit;
    private PlayerInput pi;

    public void SetPlayerIndex(PlayerInput newpi) 
    {
        pi = newpi;
        int oldIndex = Random.Range(0, old.Count);
        oldName.text = old[oldIndex];
        playerIndex = pi.playerIndex;
        titleText.SetText("Player " + (pi.playerIndex + 1));
        ignoreInputTime = Time.time + ignoreInputTime;
        PlayerConfigManager.Instance.SetPlayerName(playerIndex, oldName.text);
        PlayerConfigManager.Instance.SetPlayerCharacter(playerIndex, 0);
        characterImage.sprite = characterUI[0];
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

        if (hasQuit)
        {
            hasQuit = false;
            PlayerConfigManager.Instance.HandlePlayerJoin(pi);
        }
        else
        {
            if (characterSelected >= characterUI.Length - 1)
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
    }
    public void SelectCharacterRight()
    {
        if (!inputEnable || playerReady) { return; }

        if (hasQuit)
        {
            hasQuit = false;
            PlayerConfigManager.Instance.HandlePlayerJoin(pi);
        }
        else
        {
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

    }

    public void ReadyPlayer()
    {
        if (!inputEnable) { return; }
        if(hasQuit)
        {
            hasQuit = false;
            PlayerConfigManager.Instance.HandlePlayerJoin(pi);
        }
        else
        {
            playerReady = true;
            PlayerConfigManager.Instance.ReadyPlayer(playerIndex, true);
        }
    }

    public void NotReady()
    {
        if (!inputEnable) { return; }

        if (hasQuit)
        {
            hasQuit = false;
            PlayerConfigManager.Instance.HandlePlayerJoin(pi);
        }
        else
        {
            if (playerReady)
            {
                playerReady = false;
                PlayerConfigManager.Instance.ReadyPlayer(playerIndex, false);
            }
            else
            {
                hasQuit = true;
                PlayerConfigManager.Instance.HandlePlayerQuit(playerIndex);
                gameObject.SetActive(false);
            }
        }
    }
}
