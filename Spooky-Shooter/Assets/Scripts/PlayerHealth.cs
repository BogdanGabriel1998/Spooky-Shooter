using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //In these variables we'll store the maxmimum
    //Health value and actual one when we're hit
    public int maxHealth = 100;
    public int currentHealth;

    //We'll also need to access the HealthBar script
    //To generate the life value on the screen
    public HealthBar healthBar;

    //We're going to need these assets that will show the player score
    //And to generate the proper UI and sound when the player dies
    [Header("Needed assets")]
    [SerializeField] private Transform gameOverPanel;
    [SerializeField] private Transform scoreUI;
    [SerializeField] private AudioSource playerDeathSound;
    
    //We're going to need these scripts in order to access the name and
    //The score of the player, and then submit a new entry on the highscore table
    [Header("Submit Player Score Scripts")]
    [SerializeField] private MainMenuScript mainMenuScript;
    [SerializeField] private ScoreScript scoreScript;
    [SerializeField] private HighscoreTableScript highscoreTableScript;

    //And in these variables we're going to store the needed data
    //For when we need to submit a new entry to the highscore
    private int submissionScore;
    private string submissionName;

    //And we'll use this variiable in order to generate
    //The 'Death' procedure only one time when we die
    private bool hasAScoreBeenSubmitted = false;

    private GameObject dontDestroyOnLoadObject;


    //At first we set the maximum health
    public void Awake()
    {
        dontDestroyOnLoadObject = GameObject.Find("Don'tDestroyOnLoadObject");
        submissionName = dontDestroyOnLoadObject.transform.GetComponent<DontDestroyOnLoadScript>().playerName;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    //And we update the health value every time we get hit,
    //And also we generate the 'Death' procedure for when we die and then we submit the score
    public void TakeDamage()
    {
        int damage = Random.Range(8, 11);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) 
        {
            if (hasAScoreBeenSubmitted == false) 
            {
                hasAScoreBeenSubmitted = true;

                submissionScore = scoreScript.scoreValue;
                
                highscoreTableScript.AddHighscoreEntry(submissionScore, submissionName);

                playerDeathSound.Play();
                gameOverPanel.gameObject.SetActive(true);
                scoreUI.gameObject.SetActive(false);
                transform.gameObject.SetActive(false);
            }

            
        }
    }

}
