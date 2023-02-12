using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerNavMesh : MonoBehaviour
{
    //This is the script we use to make the monsters follow the player


    public NavMeshAgent navMeshAgent;

    //Here we memorize the player that has to be followed and the his health that's going to be reduced in case it's been caught
    private GameObject playerDestinationPoint;
    private PlayerHealth playerHealth;

    private bool hitPlayer = false;
    private float playerDamageRate = 3.0f;

    //First we have to to find the player that will be followed and his health
    private void Awake()
    {
        playerDestinationPoint = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerHealth = playerDestinationPoint.GetComponent<PlayerHealth>();   
    }


    //And we define if we catch the player and set at how many seconds
    //We can reduce his health again in case we still have him caught
    private void Update()
    {
        playerDamageRate -= Time.deltaTime;

        navMeshAgent.destination = playerDestinationPoint.transform.position;

        if (hitPlayer == true && playerDamageRate <= 0)
        {
            playerHealth.TakeDamage();
            playerDamageRate = 3.0f;
        }

    }

    //Here we check if we caught the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.tag == "Player")
            hitPlayer = true;
    }


    //And here we check he escaped
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.gameObject.tag == "Player")
            hitPlayer = false;
    }


}
