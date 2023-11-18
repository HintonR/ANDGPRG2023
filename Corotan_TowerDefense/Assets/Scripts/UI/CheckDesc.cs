using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class CheckDesc : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI _desc;
    [SerializeField] RawImage _tower;
    [SerializeField] TowerType _type;

    void Start()
    {
        _desc.gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch(_type)
        {
            case TowerType.arrow:
            _desc.text = "Arrow Tower \n" +
                         "Cost: 50 \n" +
                         "Fast Reload Speed \n" +
                         "Single Target \n" +
                         "Strong vs Flying";
            _tower.color = GameManager.Instance.HexToColor("#9A9A9A");
            break;
            case TowerType.bomb:
            _desc.text = "Bomb Tower \n" +
                         "Cost: 150 \n" +
                         "Slow Reload Speed \n" +
                         "Deals Heavy Area Damage \n" +
                         "Can't Target Flying \n";
            _tower.color = GameManager.Instance.HexToColor("#FFA500");
            break;
            case TowerType.ice:
            _desc.text = "Ice Tower \n" +
                         "Cost: 120 \n" +
                         "Medium Reload Speed \n" +
                         "Deals Light Area Damage \n" +
                         "Slows Enemies";
            _tower.color = Color.blue;
            break;
            case TowerType.fire:
            _desc.text = "Fire Tower \n" +
                         "Cost: 100 \n" +
                         "Medium Reload Speed \n" +
                         "Single Target \n" +
                         "Burns Enemies";
            _tower.color = Color.red;
            break;
        }
        _desc.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _desc.gameObject.SetActive(false);
        _tower.color = GameManager.Instance.HexToColor("#FFFFFF");
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        _desc.gameObject.SetActive(false);
        _tower.color = GameManager.Instance.HexToColor("#FFFFFF");
    }
}
