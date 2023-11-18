using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _gold, _lives, _score, _wave;
    [SerializeField] Button _sWave;
    [SerializeField] GameObject _sPoint;
    private GameManager _gM;
    private AudioManager _aM;
    void Start()
    {
        _gM = GameManager.Instance;
        _aM = AudioManager.Instance;
        _sWave.onClick.AddListener(StartWave);
    }

    void Update()
    {
        _gold.text = "Gold: " + _gM._gold;
        _lives.text = "Lives: " + _gM._lives;
        _score.text = "Score: " + _gM._score;
        if (_gM._enemies.Count != 0)
        {
            _sWave.gameObject.SetActive(false);
            _wave.gameObject.SetActive(false);
        }
        else 
        {
            _wave.text = "Planning Phase";
            _wave.gameObject.SetActive(true);
            _sWave.gameObject.SetActive(true);
        }
   
    }
    void StartWave()
    {
        _aM.PlaySFX(_aM._selectSFX);
        _sPoint.GetComponent<SpawnControl>().StartSpawn();
    }
}
