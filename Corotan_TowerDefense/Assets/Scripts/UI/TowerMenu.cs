using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TowerMenu : MonoBehaviour
{
    [SerializeField] Button _aBtn, _bButn, _iBtn, _fBtn, _cBtn;
    [SerializeField] TextMeshProUGUI _gold;
    [SerializeField] GameObject _tower;
    
    private GameManager _gM;
    private AudioManager _aM;
    private UIHandler _uiH;
    void Start()
    {
        _gM = GameManager.Instance;
        _aM = AudioManager.Instance;
        _uiH = UIHandler.Instance;
        _aBtn.onClick.AddListener(() => BuildTower(50, TowerType.arrow));
        _bButn.onClick.AddListener(() => BuildTower(150, TowerType.bomb));
        _iBtn.onClick.AddListener(() => BuildTower(120, TowerType.ice));
        _fBtn.onClick.AddListener(() => BuildTower(100, TowerType.fire));
        _cBtn.onClick.AddListener(CloseMenu);
    }

    void Update()
    {
        _gold.text = "Gold: " + GameManager.Instance._gold;
    }

    void BuildTower(int cost, TowerType type)
    {
        if(_gM._gold >= cost)
        {
            _aM.PlaySFX(_aM._coinSFX);
            GameObject _t = Instantiate(_tower, _gM._cursor.transform.position, transform.rotation);
            _t.GetComponent<Tower>().SetTowerType(type);
            //_gM._cursor.SetTower(_t);
            _uiH._mState = MenuState.off;
        }
        else _aM.PlaySFX(_aM._invalidSFX);
    }

    void CloseMenu() 
    {
        _uiH._mState = MenuState.off;
    }

}
