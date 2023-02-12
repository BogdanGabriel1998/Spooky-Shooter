using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour
{

    public string playerName;

    //void Start()
    //{
    //    DontDestroyOnLoad(transform.gameObject);
    //}

    private static GameObject playerInstance;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (playerInstance == null)
        {
            playerInstance = transform.gameObject;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
}
