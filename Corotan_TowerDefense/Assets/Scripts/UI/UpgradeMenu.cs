using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] Button _dBtn, _sBtn, _rBtn, _cBtn, _yes, _no;
    [SerializeField] TextMeshProUGUI _gold, _tier, _upg, _cost, _info;
    [SerializeField] RawImage _tIMG;
    [SerializeField] GameObject _cW;

    private GameObject _tower;
    private bool _cFlag;
    private UpgradeType _type;
    private GameManager _gM;
    private AudioManager _aM;
    private UIHandler _uiH;

    public GameObject GetTower() { return _tower; }

    void Awake()
    {
        _gM = GameManager.Instance;
        _aM = AudioManager.Instance;
        _uiH = UIHandler.Instance;
    }

    void Start()
    {
        _cFlag = false;
        _dBtn.onClick.AddListener(() => UpgradeAttribute(UpgradeType.damage));
        _sBtn.onClick.AddListener(() => UpgradeAttribute(UpgradeType.speed));
        _rBtn.onClick.AddListener(() => UpgradeAttribute(UpgradeType.range));
        _yes.onClick.AddListener(Proceed);
        _no.onClick.AddListener(Cancel);
        _cBtn.onClick.AddListener(CloseMenu);
    }

    void Update()
    {
        CheckTower();
        DisableUpgrades();
        ConfirmWindow();
        _gold.text = "Gold: " + GameManager.Instance._gold;
        _cost.text = "Cost: " + _tower.GetComponent<Tower>().GetUpgradeCost();
    }
    
    void OnEnable()
    {
        _tower = _gM._cursor.GetTower();
        UpdateDesc();
    }
       void OnDisable()
    {
        _tower = null;
    }

    void ConfirmWindow()
    {
        if (_cFlag) _cW.SetActive(true);
        if (!_cFlag) _cW.SetActive(false);
    }

    void Proceed()
    {  
        _aM.PlaySFX(_aM._upgradeSFX);
        _gM._gold -= _tower.GetComponent<Tower>().GetUpgradeCost();
        switch(_type)
            {
            case UpgradeType.damage:
            _tower.GetComponent<Tower>().UpdateDMG();
            break;
            case UpgradeType.speed:
            _tower.GetComponent<Tower>().UpdateSPD();
            break;
            case UpgradeType.range:
            _tower.GetComponent<Tower>().UpdateRNG();
            break;
        }
        if(_tower.GetComponent<Tower>().GetTier() < 5) _tower.GetComponent<Tower>().IncTier();
        if(_tower.GetComponent<Tower>().GetTier() < 5) _tower.GetComponent<Tower>().IncCost();
        UpdateDesc();
        _cFlag = false;
    }

    void Cancel()
    {
        _aM.PlaySFX(_aM._selectSFX);
        _cFlag = false;
    }
    void UpdateDesc()
    {
        _info.text = "Tower Info: \n" +
                     "Damage: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetDMG()) + "\n" +
                     "Speed: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetRLSP()) + "\n" +
                     "Range: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetARNG());
    }

    void UpgradeAttribute(UpgradeType type)
    {
        if(_gM._gold >= _tower.GetComponent<Tower>().GetUpgradeCost())
        {
        _aM.PlaySFX(_aM._selectSFX);
        _type = type;
        _cFlag = true;
        }
        else _aM.PlaySFX(_aM._invalidSFX);
    }

    void CheckTower()
    {
        if(_tower.GetComponent<Tower>().GetTier() == 1) { _tier.text = "I"; _upg.text = "Upgrades Left: 4"; }
        if(_tower.GetComponent<Tower>().GetTier() == 2) { _tier.text = "II"; _upg.text = "Upgrades Left: 3"; }
        if(_tower.GetComponent<Tower>().GetTier() == 3) { _tier.text = "III"; _upg.text = "Upgrades Left: 2"; }
        if(_tower.GetComponent<Tower>().GetTier() == 4) { _tier.text = "IV"; _upg.text = "Upgrades Left: 1"; }
        if(_tower.GetComponent<Tower>().GetTier() == 5) { _tier.text = "V"; _upg.text = "Max Level"; }

        if(_tower.GetComponent<Tower>().GetTowerType() == TowerType.arrow) _tIMG.color = GameManager.Instance.HexToColor("#9A9A9A");
        if(_tower.GetComponent<Tower>().GetTowerType() == TowerType.bomb) _tIMG.color = GameManager.Instance.HexToColor("#FFA500");
        if(_tower.GetComponent<Tower>().GetTowerType() == TowerType.ice) { _tIMG.color = Color.blue; _tier.color = Color.white; }
        if(_tower.GetComponent<Tower>().GetTowerType() == TowerType.fire) _tIMG.color = Color.red;
    }

    void DisableUpgrades()
    {
        if(_tower.GetComponent<Tower>().GetTier() == 5)
        {
            _dBtn.gameObject.SetActive(false);
            _sBtn.gameObject.SetActive(false);
            _rBtn.gameObject.SetActive(false);
            _gold.gameObject.SetActive(false);
            _cost.gameObject.SetActive(false);
        }
        else
        {
            _dBtn.gameObject.SetActive(true);
            _sBtn.gameObject.SetActive(true);
            _rBtn.gameObject.SetActive(true);
            _gold.gameObject.SetActive(true);
            _cost.gameObject.SetActive(true);
        }
    }

    void CloseMenu() 
    {
        _uiH._mState = MenuState.off;
    }

}
