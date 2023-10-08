using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    GameObject[] bugs;
    string cScene;
    
    void Start()
    {
        bugs = GameObject.FindGameObjectsWithTag("Target"); 
        cScene = SceneManager.GetActiveScene().name;
        Debug.Log(cScene);
    }

    // Update is called once per frame
    void Update()
    {
        bugs = GameObject.FindGameObjectsWithTag("Target");
        if (bugs.Length == 0) 
        {
            GlobalVars.totalScore = player.GetComponent<Player>().score;
            if (cScene == "Level" + GlobalVars.level) {
                GlobalVars.level += 1;
                SceneManager.LoadScene("Level" + GlobalVars.level);
            }
        }
        
    }
}
