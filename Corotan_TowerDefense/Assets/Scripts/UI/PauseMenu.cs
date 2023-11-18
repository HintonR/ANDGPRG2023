using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button _rb, _qb;
    private GameManager _gM;
    private AudioManager _aM;
    private UIHandler _uiH;
    void Start()
    {
        _gM = GameManager.Instance;
        _aM = AudioManager.Instance;
        _uiH = UIHandler.Instance;
        _rb.onClick.AddListener(ResumeGame);
        _qb.onClick.AddListener(GameManager.Instance.QuitGame);
    }

    void ResumeGame()
    {
        _aM.PlaySFX(_aM._selectSFX);
        _gM._gStatus = true;
        _uiH._mState = MenuState.off;
    }
}
