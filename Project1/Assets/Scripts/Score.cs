using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] GameObject player;

    void Update()
    {
        score.text = "Score: " + player.GetComponent<Player>().score; 
    }
}
