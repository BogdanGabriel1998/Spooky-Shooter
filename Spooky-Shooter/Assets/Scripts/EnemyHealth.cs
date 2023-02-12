using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //This is the script that defines the enemy health


    public int currentHealth;
    public HealthBar healthBar;

    [SerializeField] private int maxHealth = 100;

    //In the Awake() funtion we set the enemy health to the maximum value
    //And also the healthbar to the same value
    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    //This is the function that is called when we want for the enemy to take damage
    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
