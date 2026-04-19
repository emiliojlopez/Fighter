using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script should be used by the power script to call the audio files
//Name is super broad becuae we won't be working too much more on this project
public class PowerUpSound : MonoBehaviour
{
    //Makes a static copy shareable to the whole game
    public static PowerUpSound Instance;
    //Uses the audioSource game object for sound
    public AudioSource audioManager;
    //Uses existing mp3 files in the project
    public AudioClip powerup;
    public AudioClip powerdown;
    
    void Awake()
    {
        //THIS copy becomes the shareable instance mentioned at the top
        Instance = this;
    }
    //Using void functions since this isn't a continous one like movement
    //Plays the power-up sound
    public void playPowerUp()
    {
        //PlayOneShot is built in audioSource and it only plays once per function execution
        //fyi i think this porbably makes it good for most other sound fx? i could be wrong though
        audioManager.PlayOneShot(powerup);
    }

    //Plays the power-down sound
    public void playPowerDown()
    {
        audioManager.PlayOneShot(powerdown);
    }
}
