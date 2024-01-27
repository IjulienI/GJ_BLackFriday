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

    private List<string> old = new List<string> { "Sunday", "Marie-Francoise", "Danielle", "Anicette", "Mélo", "Gaston", "Maurice", "René", "Albert", "Georges", "Henri", "Léon", "Marcel", "Roger", "André", "Raymond", "Louis", "Émile", "Paul", "Bernard", "Gilbert", "Antoine", "Victor", "François", "Jacques", "Édouard", "Fernand", "Lucien", "Michel", "Roland", "Gustave", "Jules", "Armand", "Pierre", "Charles", "Robert", "Jean", "Yves", "Jean-Pierre", "Hubert", "Claude", "Alain", "Serge", "Noël", "Michel", "Romain", "Gaston", "Henri", "Maurice", "Léon", "Paul", "Robert", "Jacques", "René", "André", "Lucien", "Georges", "Bernard", "Roger", "Albert", "Émile", "Gilbert", "Antoine", "Raymond", "François", "Victor", "Yves", "Armand", "Jules", "Louis", "Michel", "Charles", "Roland", "Fernand", "Jean", "Claude", "Gustave", "Hubert", "Jean-Pierre", "Noël", "Romain", "Alain", "Pierre", "Serge", "René", "Marcel", "Paul", "André", "Maurice", "Robert", "Léon", "Jacques", "Georges", "Bernard", "Lucien", "Roger", "Antoine", "Raymond", "Victor", "François", "Yves", "Armand", "Jules", "Louis", "Michel", "Charles", "Roland", "Fernand", "Jean", "Claude", "Gustave", "Hubert", "Jean-Pierre", "Noël", "Romain", "Alain", "Pierre", "Serge", "Germaine", "Simone", "Marguerite", "Yvonne", "Madeleine", "Marie", "Suzanne", "Denise", "Monique", "Paulette", "Colette", "Thérèse", "Jacqueline", "Raymonde", "Lucienne", "Élisabeth", "Odette", "Andrée", "Ginette", "Hélène", "Juliette", "Josette", "Annie", "Françoise", "Solange", "Claudine", "Renée", "Léa", "Gilberte", "Aline", "Alice", "Margot", "Juliette", "Léonie", "Josiane", "Évelyne", "Rosalie", "Irène", "Charlotte", "Gertrude", "Émilie", "Rachel", "Élodie", "Isabelle", "Monique", "Suzanne", "Thérèse", "Gilberte", "Huguette", "Margot", "Andrée", "Paulette", "Gilberte", "Odile", "Marguerite", "Simone", "Yvette", "Marthe", "Léonne", "Hélène", "Denise", "Marcelle", "Élise", "Lise", "Lucette", "Rose", "Clémence", "Aurore", "Clarisse", "Antoinette", "Alice", "Lucile", "Gertrude", "Lorraine", "Laurette", "Jacqueline", "Irène", "Brigitte", "Léontine", "Cécile", "Émilienne", "Édith", "Sophie", "Françoise", "Bernadette", "Hélène", "Rosalie", "Marguerite", "Henriette", "Laurence", "Blanche", "Solange", "Clémence", "Noémie", "Éva", "Adrienne", "Isabelle", "Margot", "Gertrude", "Juliette" };
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
