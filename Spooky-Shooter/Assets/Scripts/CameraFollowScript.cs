using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    //This is the script we use in order to make the camera move along with the player

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 position;


    //And in the update function, we attach the camera at a fixed position to the player
    private void Update()
    {
        transform.position = player.position + position;
    }
}
