using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum UpgradeType
{
    damage,
    speed,
    range
}

public class CheckUpgrade : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI _info;
    [SerializeField] UpgradeType _type;

    private GameManager _gM;

    private GameObject _tower;
    
    void Start()
    {
        _gM = GameManager.Instance;
        _tower = transform.GetComponentInParent<UpgradeMenu>().GetTower();
        _info.text = "Tower Info: \n" +
                     "Damage: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetDMG()) + "\n" +
                     "Speed: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetRLSP()) + "\n" +
                     "Range: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetARNG());
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        switch(_type)
        {
            case UpgradeType.damage:
            _info.text = "Tower Info: \n" +
                         "Damage: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetDMG()) + " >> " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetDMG() * 1.25f) + "\n" +
                         "Speed: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetRLSP()) + "\n" +
                         "Range: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetARNG());
            break;
            case UpgradeType.speed:
            _info.text = "Tower Info: \n" +
                         "Damage: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetDMG()) + "\n" +
                         "Speed: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetRLSP()) + " >> " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetRLSP() * 0.9f) + "\n" +
                         "Range: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetARNG());
            break;
            case UpgradeType.range:
            _info.text = "Tower Info: \n" +
                         "Damage: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetDMG()) + "\n" +
                         "Speed: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetRLSP()) + "\n" +
                         "Range: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetARNG()) + " >> " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetARNG() * 1.1f);
            break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _info.text = "Tower Info: \n" +
                     "Damage: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetDMG()) + "\n" +
                     "Speed: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetRLSP()) + "\n" +
                     "Range: " + _gM.TruncateFloat(_tower.GetComponent<Tower>().GetARNG());
    }

    void Update()
    {
        _tower = transform.GetComponentInParent<UpgradeMenu>().GetTower();
    }


}
