using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private DontDestroyOnLoadScript dontDestroyOnLoadScript;


    //The input field that's going to be
    //Used by the player to write down a name
    [SerializeField] private TMP_InputField characterInputField;

    //The Main Buttons
    [Header("The Main Buttons")]
    [SerializeField] private Transform playButton;
    [SerializeField] private Transform highScoreButtonButton;
    [SerializeField] private Transform quitButton;

    //The Main Menu Itself
    [Header("The Main Menu")]
    [SerializeField] private Transform mainMenu;

    //The HighScore Table
    [Header("HighScore Table")]
    [SerializeField] private Transform highScoreTable;

    //The Elements that are needed for the gameplay
    [Header("Gameplay Elements")]
    [SerializeField] private Transform enemiesSpawner;
    [SerializeField] private Transform playerHealthBar;
    [SerializeField] private Transform scoreUI;
    [SerializeField] private Transform playerCharacter;

    //The Input Field in which The Player Will Introduce the name
    [Header("The Input Field")]
    [SerializeField] private Transform createPlayerField;

    //The Panels that are used for the Input Field
    //Depending on the case
    [Header("Input Field Warnings")]
    [SerializeField] private Transform noCharactersPanels;
    [SerializeField] private Transform invalidCharactersPanel;
    [SerializeField] private Transform tooManyCharactersPanel;
    [SerializeField] private Transform instructionsPanel;

    //The 'Yes/No' Panel in case the 'Quit' button has been pressed
    [Header("The Quitting Panel")]
    [SerializeField] private Transform areYouSureYouWantToQuitPanel;

    //The bools that we're going to need to
    //Check if the name chosen by the player is a valid one
    private bool playerHasName = true;
    private bool playerNameIsNotTooBig = true;
    private bool playerHasValidCharacters = true;

    //And this variable will be used to store the valid name
    //The player has chosen in order to use it on the Leaderboard
    public string usedPlayerName;


    private void Awake()
    {
        dontDestroyOnLoadScript = GameObject.Find("Don'tDestroyOnLoadObject").GetComponent<DontDestroyOnLoadScript>();
    }


    //THESE ARE THE FUNCTIONS WE CALL IF WE
    //WANT TO SEE THE HIGHSCORE TABLE IN THE MAIN MENU
    public void OpenHighScoreTable() 
    {
        highScoreTable.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        highScoreButtonButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }
    public void GoBackFromHighScoreTable() 
    {
        playButton.gameObject.SetActive(true);
        highScoreButtonButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        highScoreTable.gameObject.SetActive(false);
    }

    //WHEN WE PRESS THE 'PLAY' BUTTON IN THE MAIN MENU,
    //WE CALL THIS FUNCTION THAT WILL GIVE US THE 'CREATE NAME' PANEL FOR THE PLAYER
    public void CreatePlayerButton() 
    {
        createPlayerField.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        highScoreButtonButton.gameObject.SetActive(false);
    }


    //IF EVERYTHING IS SET, THEN WE CLOSE DOWN THE MAIN MENU AND
    //START THE GAME BY ACTIVATING THE PROPER OBJECTS IN THE GAME SCENE
    public void StartGameButton() 
    {
        SceneManager.LoadScene("Game Scene");
    }

    //AND WE ARE GIVEN A 'YES/NO' PANEL IN CASE WE PRESS ON THE 'QUIT' BUTTON
    public void QuitGame()
    {
        areYouSureYouWantToQuitPanel.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        highScoreButtonButton.gameObject.SetActive(false);
    }


    //THIS IS THE FUNCTION WE CALL IN ORDER CHECK IF THE PLAYER NAME IS A VALID ONE
    public void CheckCharacterName() 
    {
        string characterName = characterInputField.text.ToString();
        string regex = "^[a-zA-Z]+$";

        if (characterName.Length == 0)
        {
            playerHasName = false;
        }
        else if (characterName.Length > 10) 
        {
            playerNameIsNotTooBig = false;
        }
        else
            playerHasValidCharacters = Regex.IsMatch(characterName, regex);

        DoWeProceedWithTheGivenName();
    }

    //THIS IS THE FUNCTION THAT OPENS UP THE PROPER PANEL IF
    //THE GIVEN NAME IS AN INVALID ONE, DEPENDING ON THE ERROR
    public void DoWeProceedWithTheGivenName() 
    {
        if (playerHasName == false)
        {
            noCharactersPanels.gameObject.SetActive(true);
            createPlayerField.gameObject.SetActive(false);
        }
        else if (playerNameIsNotTooBig == false)
        {
            tooManyCharactersPanel.gameObject.SetActive(true);
            createPlayerField.gameObject.SetActive(false);
        }
        else if (playerHasValidCharacters == false)
        {
            invalidCharactersPanel.gameObject.SetActive(true);
            createPlayerField.gameObject.SetActive(false);
        }
        else 
        {
            dontDestroyOnLoadScript.playerName = characterInputField.text.ToString();
            instructionsPanel.gameObject.SetActive(true);
            createPlayerField.gameObject.SetActive(false);
        }
    }


    //AND THESE THE FUNCTIONS WE CALL IN ORDER TO CLOSE THE
    //INSTRUCTIONS PANELS THAT WERE OPEN IN CASE AN INVALID NAME HAS BEEN GIVEN
    public void NoCharactersAtAllUnderstoodInstructions()
    {
        createPlayerField.gameObject.SetActive(true);
        noCharactersPanels.gameObject.SetActive(false);
        playerHasName = true;
        playerNameIsNotTooBig = true;
        playerHasValidCharacters = true;
    }
    public void TooManyCharactersUnderstoodInstructions()
    {
        createPlayerField.gameObject.SetActive(true);
        tooManyCharactersPanel.gameObject.SetActive(false);
        playerHasName = true;
        playerNameIsNotTooBig = true;
        playerHasValidCharacters = true;
    }
    public void InvalidCharactersUnderstoodInstructions() 
    {
        createPlayerField.gameObject.SetActive(true);
        invalidCharactersPanel.gameObject.SetActive(false);
        playerHasName = true;
        playerNameIsNotTooBig = true;
        playerHasValidCharacters = true;
    }


    //AND FINALLY, THESE ARE THE FUNTIONS WE CALL WHEN WE WANT TO QUIT THE GAME
    public void AreYouSureYouWantToQuitYES() 
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    public void AreYouSureYouWantToQuitNO()
    {
        playButton.gameObject.SetActive(true);
        highScoreButtonButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        areYouSureYouWantToQuitPanel.gameObject.SetActive(false);
    }
}
