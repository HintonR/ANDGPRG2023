using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Button _rb, _qb;
    [SerializeField] TextMeshProUGUI _score;
    private GameManager _gM;
    private AudioManager _aM;
    private UIHandler _uiH;
    void Start()
    {
        _gM = GameManager.Instance;
        _aM = AudioManager.Instance;
        _uiH = UIHandler.Instance;
        _score.text = "Score: " + GameManager.Instance._score;
        _rb.onClick.AddListener(RestartGame);
        _qb.onClick.AddListener(GameManager.Instance.QuitGame);

        _aM.StopBGM();
        _aM.PlaySFX(_aM._gameoverMFX);
    }
    void RestartGame()
    {
        _aM.PlaySFX(_aM._selectSFX);
        _aM.ChangeBGM(_aM._buildBGM);
        _gM._lives = 1;
        _gM._gStatus = false;
        _uiH._mState = MenuState.start;
    }
}
