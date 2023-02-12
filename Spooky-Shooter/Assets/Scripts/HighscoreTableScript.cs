using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HighscoreTableScript : MonoBehaviour
{
    [SerializeField] private Transform entryTemplate;
    [SerializeField] private Transform entryContainer;

    //private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private Highscores table;

    public void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();

        if (PlayerPrefs.HasKey("highscoreTable"))
        {
            string jsonTable = PlayerPrefs.GetString("highscoreTable");
            table = JsonUtility.FromJson<Highscores>(jsonTable);
        }
        else 
        {
            table = new Highscores();
        }

        highscoreEntryTransformList = new List<Transform>();
        CreatePreliminaryTransforms(entryContainer);

        for (int i = 0; i < table.highscoreEntryList.Count; i++) 
        {
            HighscoreEntry highscoreEntry = table.highscoreEntryList[i];
            UpdateUIEntry(highscoreEntry, i);
        }
    }


    private float lastAddTime = 0.0f;
    private int lastScore = 0;
    //private void Update()
    //{
    //    if (Time.time - lastAddTime > 1.0)
    //    {
    //        lastAddTime = Time.time;
    //        AddHighscoreEntry(lastScore, "Bob");
    //        lastScore++;
    //    }
    //}



    private void CreatePreliminaryTransforms(Transform container) 
    {
        for (int i = 0; i < 10; i++) 
        {
            float templateHeight = 25f;

            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

            entryRectTransform.anchoredPosition = new Vector2(0, -highscoreEntryTransformList.Count * templateHeight);

            entryTransform.gameObject.SetActive(true);

            highscoreEntryTransformList.Add(entryTransform);
        }  
    }

    //This is the function we use to show on the panel the entries (not sure if it can be used the same)
    private void UpdateUIEntry(HighscoreEntry highscoreEntry, int index)
    {
        int rank = index + 1;
        string rankString;

        switch (index + 1)
        {
            default:
                rankString = rank + "TH"; break;
            case 1:
                rankString = "1ST"; break;
            case 2:
                rankString = "2ND"; break;
            case 3:
                rankString = "3RD"; break;
        }

        highscoreEntryTransformList[index].Find("Position Text").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;
        highscoreEntryTransformList[index].Find("Score Text").GetComponent<Text>().text = score.ToString();


        string name = highscoreEntry.name;
        highscoreEntryTransformList[index].Find("Name Text").GetComponent<Text>().text = name;
    }


    //This is the funtion we use in order to add a new entry to the list
    public void AddHighscoreEntry(int score, string name)
    {
        if (table.highscoreEntryList.Count == 0)
        {
            HighscoreEntry entry = new HighscoreEntry();
            entry.score = score;
            entry.name = name;
            table.highscoreEntryList.Add(entry);
        }
        else
        {
            if (table.highscoreEntryList.Count < 10) 
            {
                HighscoreEntry entry = new HighscoreEntry();
                entry.score = score;
                entry.name = name;
                table.highscoreEntryList.Add(entry);
            }
            else if (score > table.highscoreEntryList[table.highscoreEntryList.Count - 1].score)
            {
                table.highscoreEntryList[table.highscoreEntryList.Count - 1].score = score;
                table.highscoreEntryList[table.highscoreEntryList.Count - 1].name = name;
            }

            for (int i = table.highscoreEntryList.Count - 1; i >= 1; i--)
            {
                if (table.highscoreEntryList[i].score > table.highscoreEntryList[i - 1].score)
                {
                    HighscoreEntry def = table.highscoreEntryList[i];
                    table.highscoreEntryList[i] = table.highscoreEntryList[i - 1];
                    table.highscoreEntryList[i - 1] = def;
                }
            }
        }

        for (int j = 0; j < table.highscoreEntryList.Count; j++) 
        {
            HighscoreEntry highscoreEntry = table.highscoreEntryList[j];
            UpdateUIEntry(highscoreEntry, j);
        }



        PlayerPrefs.SetString("highscoreTable", JsonUtility.ToJson(table));
        PlayerPrefs.Save();
    }

    

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
        public Highscores()
        {
            highscoreEntryList = new List<HighscoreEntry>();
        }
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}

