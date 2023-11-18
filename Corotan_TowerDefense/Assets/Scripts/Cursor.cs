using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] float _sp = 5;


    private bool _bStatus = true;
    private bool _tStatus = false;
    private GameObject _tower;

    public bool GetBuildState() { return _bStatus; }
    public bool GetTowerState() { return _tStatus; }
    public GameObject GetTower() { return _tower; }
    public void SetTower(GameObject tower) { _tower = tower; }

    void Start()
    {
        GameManager.Instance._cursor = this;
    }

    void Update()
    {
        if(GameManager.Instance._gStatus) Movement();
        CheckLocation();
    }


    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("NoBuild")) _bStatus = true;
        if (obj.gameObject.CompareTag("Tower")) 
        {
            _bStatus = true;
            _tStatus = false;
            _tower = null;
        }
    }
    void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("NoBuild")) _bStatus = false;
        if (obj.gameObject.CompareTag("Tower")) 
        {
            _bStatus = false;
            _tStatus = true;
            _tower = obj.gameObject;
        }
    }

    void Movement()
    {
        if(Input.GetKey(KeyCode.W) && GetComponent<Transform>().position.y < 6.6) 
            gameObject.transform.Translate(Vector2.left * (_sp * 1f) * Time.deltaTime);
        if(Input.GetKey(KeyCode.S) && GetComponent<Transform>().position.y > -6.4)  
            gameObject.transform.Translate(Vector2.right * (_sp * 1f) * Time.deltaTime);
        if(Input.GetKey(KeyCode.A) && GetComponent<Transform>().position.x > -12.1) 
            gameObject.transform.Translate(Vector2.down * (_sp * 1f) * Time.deltaTime);
        if(Input.GetKey(KeyCode.D) && GetComponent<Transform>().position.x < 12.45) 
            gameObject.transform.Translate(Vector2.up * (_sp * 1f) * Time.deltaTime);
    }

    void CheckLocation()
    {
        if(_bStatus) this.GetComponent<SpriteRenderer>().color = Color.green;
        else if(!_bStatus) this.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
