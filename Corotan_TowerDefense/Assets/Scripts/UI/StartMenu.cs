using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button _sb, _qb;
    private GameManager _gM;
    private AudioManager _aM;
    private UIHandler _uiH;

    void Start()
    {
        _gM = GameManager.Instance;
        _aM = AudioManager.Instance;
        _uiH = UIHandler.Instance;
        _sb.onClick.AddListener(StartGame);
        _qb.onClick.AddListener(GameManager.Instance.QuitGame);
    }

    void StartGame()
    {
        _aM.PlaySFX(_aM._selectSFX);

        _gM._gold = 150;
        _gM._score = 0;
        _gM._lives = 20;
        _gM._wave = 1;
        _gM._dMultiplier = 1f;
        _gM._gStatus = true;

        _uiH._mState = MenuState.off;
    }
}
