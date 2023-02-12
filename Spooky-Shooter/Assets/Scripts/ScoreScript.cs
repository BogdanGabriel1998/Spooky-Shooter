using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int scoreValue = 0;

    [SerializeField] private Transform finalScoreUI;
    private Text score;


    private void Start()
    {
        score = GetComponent<Text>();
    }

    private void Update()
    {
        score.text = "Score : " + scoreValue;
        finalScoreUI.GetComponent<Text>().text = "Your Score Was : " + scoreValue;
    }
}
