using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    //This is the  script that's used to generate the sound of each enemy when it's either damaged or killed


    //Here we define the 'Audioclips' and the 'Source' that's going to play them
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip dieSound;


    //This is the function that's going to play the 'Damaged' sound
    public void EnemyIsHit()
    {
        audioSource.clip = hurtSound;
        audioSource.Play();
    }


    //And this is the function that's going to play the 'Killed' sound
    public void EnemyDies()
    {
        audioSource.clip = dieSound;
        audioSource.Play();
    }
}
