using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum TowerType
{
    arrow,
    bomb,
    ice,
    fire
}
public class Tower : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _tText;
    private TowerType _type;
    private bool _isSlowed;
    private float _rlsp, _arng, _dmg, _bsp;
    private int _tier, _cost, _uCost, _uCostMod;
  
    public float GetRLSP() { return _rlsp; }
    public float GetARNG() { return _arng; }
    public float GetDMG() { return _dmg; }
    public float GetBSP() { return _bsp; }
    public bool GetSlowStatus() { return _isSlowed; }

    public TowerType GetTowerType() { return _type; }
    public int GetTier() { return _tier; }
    public int GetCost() { return _cost; }
    public int GetUpgradeCost() { return _uCost; }
    public int GetUpgCostMod() { return _uCostMod; }

    public void SetTowerType(TowerType type) { _type = type; }
    public void SetSlowStatus(bool value) { _isSlowed = value; }
    public void UpdateDMG() { _dmg *= 1.25f; }
    public void UpdateSPD() { _rlsp *= 0.9f; }
    public void UpdateRNG() { _arng *= 1.1f; }
    public void IncTier() { _tier++; }
    public void IncCost() { _uCost += _uCostMod; }

    void Start()
    {    
        _tier = 1;
        _isSlowed = false;
        Init();
        GameManager.Instance._gold -= _cost;
    }

    void Update()
    {
        DisplayTier();
        
        Color nAlpha = GetComponent<SpriteRenderer>().color;
        if (_isSlowed) nAlpha.a = 0.5f;
        else nAlpha.a = 1f;
        GetComponent<SpriteRenderer>().color = nAlpha;    
    }

    void Init() 
    {
        if(_type == TowerType.arrow)
        {
            GetComponent<SpriteRenderer>().color = GameManager.Instance.HexToColor("#9A9A9A");
            _rlsp = 0.5f;
            _arng = 3f;
            _dmg = 1f;
            _bsp = 9f;
            _cost = 50;
            _uCost = 75;
            _uCostMod = 25; //75,100,125,150
        }
        if(_type == TowerType.bomb)
        {
            GetComponent<SpriteRenderer>().color = GameManager.Instance.HexToColor("#FFA500");
            _rlsp = 1.8f;
            _arng = 2.3f;
            _dmg = 4.2f;
            _bsp = 5.5f;
            _cost = 150;
            _uCost = 120;
            _uCostMod = 30; //120, 150, 180, 210
        }
        if(_type == TowerType.ice)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
            _tText.color = Color.white;
            _rlsp = 1f;
            _arng = 2.7f;
            _dmg = 0.7f;
            _bsp = 7.5f;
            _cost = 120;
            _uCost = 100;
            _uCostMod = 50; //100, 150, 200, 250
        }
        if(_type == TowerType.fire)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            _rlsp = 1.2f;
            _arng = 2.5f;
            _dmg = 5f;
            _bsp = 7.5f;
            _cost = 100;
            _uCost = 80;
            _uCostMod = 40; //80, 120, 160, 200
        }   
    }

    void DisplayTier()
    {
        if(_tier == 1) _tText.text = "I";
        if(_tier == 2) _tText.text = "II";
        if(_tier == 3) _tText.text = "III";
        if(_tier == 4) _tText.text = "IV";
        if(_tier == 5) _tText.text = "V";
    }
}
