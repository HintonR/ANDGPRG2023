using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioSource aSource;
    [SerializeField] AudioClip gameOver;
    [SerializeField] AudioClip bgm;

    GameObject[] player;
    GameObject[] enemies;
    GameObject[] upMods; //Unused
    GameObject[] repKits;
    
    bool gmState = true;

    void Start()
    {
        aSource = this.GetComponent<AudioSource>();
        aSource.clip = bgm;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectsWithTag("Player"); 
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        upMods = GameObject.FindGameObjectsWithTag("Upgrade");
        repKits = GameObject.FindGameObjectsWithTag("Heal");

       if (player.Length == 0 && gmState)
        {
            aSource.Stop();
            aSource.PlayOneShot(gameOver);
            gmState = false;
        } 
 
        if(!aSource.isPlaying && gmState) aSource.Play();        
    }
}
