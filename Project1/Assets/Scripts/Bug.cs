using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private AudioSource aSource;
    
    private int health = 1;

    void Awake() 
    {
        health = Random.Range(1,4);    
    }
    
    void Update()
    {
        die();
    }

    public void takeDamage()
    {
        health -= 1;
    }

    void die()
    {
        if (health <= 0) 
        {
            if (!aSource.isPlaying) aSource.Play();
            Destroy(gameObject, 0.5f);
        }
    }

    
}
