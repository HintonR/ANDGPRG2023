using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType
{
    ground,
    flying,
    boss,
    mixed,
    bossmixed
}

public class Enemy : MonoBehaviour
{
    private float _hp, _sp, _rsp, _slsp;
    private bool _isSlowed, _isChilled, _isBurned;
    private Transform _wp;
    private int _wpTracker = 0;
    private int _gDrop, _sVal;
    private EnemyType _type;
    private Coroutine _slow;
    private AudioManager _aM;

    public EnemyType GetEnemyType() { return _type; }
    public void SetEnemyType(EnemyType type) { _type = type; }
    public void SetChillFlag(bool value) { _isChilled = value; }
    public void SetBurnFlag(bool value) { _isBurned = value; }

    void Awake()
    {
        _aM = AudioManager.Instance;
        _rsp = 90f;
        _isSlowed = false;
        switch(GameManager.Instance._wave % 5)
        {
            case 0:
            _type = EnemyType.boss;
            break;
            case 1:
            _type = EnemyType.ground;
            break;
            case 2:
            _type = EnemyType.flying;
            break;
            case 3:
            _type = EnemyType.mixed;
            break;
            case 4:
            _type = EnemyType.bossmixed;
            break;
        }
    }

    void Start()
    {
        Init(_type);
        _slsp = _sp * 0.66f;
        GameManager.Instance.AddEnemy(gameObject);
    }

    void Update()
    {
         Pathing();
         CheckStatus();
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("WayPoint")) 
        {
            if(_wpTracker < GameManager.Instance._poi.Count - 1) _wpTracker++;
            else 
            {
                Destroy(gameObject);
                GameManager.Instance.RemoveEnemy(gameObject);
                
                if(_type == EnemyType.boss) GameManager.Instance._lives -= 3;
                else GameManager.Instance._lives--;
            }
        }
        
        if (obj.gameObject.CompareTag("Mud"))
            if(_type != EnemyType.flying) _isSlowed = true;
    }
    
    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Mud"))
            if(_type != EnemyType.flying) _isSlowed = false;
    }

    void Pathing()
    {
        _wp = GameManager.Instance._poi[_wpTracker];
       
        if (_wp)
        {
            Vector2 direction = _wp.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rsp * Time.deltaTime);

            float csp;
            if(!_isSlowed && !_isChilled) csp = _sp;
            else csp = _slsp;
            
            if(GameManager.Instance._gStatus) transform.Translate(Vector2.right * csp * Time.deltaTime);
            else transform.Translate(Vector2.right * 0 * Time.deltaTime);
        }
    }

    void Init(EnemyType type)
    {
        if(type == EnemyType.ground)
        {
            GetComponent<SpriteRenderer>().color = GameManager.Instance.HexToColor("#00FF00");
            _hp = 9f * GameManager.Instance._dMultiplier;
            _sp = 1f * GameManager.Instance._dMultiplier;
            if(_sp > 4f) _sp = 4f;
            _gDrop = 10;
            _sVal = 15;

        }
        if(type == EnemyType.flying)
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
            GetComponent<Transform>().localScale = new Vector3(0.35f,0.35f,0.35f);
            _hp = 7.5f * GameManager.Instance._dMultiplier;
            _sp = 1.5f * GameManager.Instance._dMultiplier;
            if(_sp > 5f) _sp = 5f;
            _gDrop = 15;
            _sVal = 15;
        }
        if(type == EnemyType.boss)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
            GetComponent<Transform>().localScale = new Vector3(0.8f,0.8f,0.8f);
            _hp = 12f * GameManager.Instance._dMultiplier;
            _sp = 1.2f * GameManager.Instance._dMultiplier;
            if(_sp > 4.25f) _sp = 4.25f;
            _gDrop = 30;
            _sVal = 50;
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if(_hp <= 0) 
        {
            _aM.PlaySFX(_aM._deathSFX);
            GameManager.Instance._gold += _gDrop;
            GameManager.Instance._score += _sVal;
            Destroy(gameObject);
            GameManager.Instance.RemoveEnemy(gameObject);
        }

    }

    void CheckStatus()
    {
        if(_isChilled) 
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            _slow = StartCoroutine(Slow());

        }
        if(_isBurned) GetComponent<SpriteRenderer>().color = Color.yellow;
        else if (!_isChilled && !_isBurned)
        {
            if(_type == EnemyType.ground) GetComponent<SpriteRenderer>().color = GameManager.Instance.HexToColor("#00FF00");
            if(_type == EnemyType.flying) GetComponent<SpriteRenderer>().color = Color.magenta;
            if(_type == EnemyType.boss) GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    IEnumerator Slow()
    {
        float sTime = 3f;
        _isChilled = true;
        while (sTime > 0)
        {
            if (GameManager.Instance._gStatus) 
            {
                sTime -= 1f;
                yield return new WaitForSeconds(1f);
            }
            else yield return null;
        }
        _isChilled = false;
        StopCoroutine(_slow);
    }
}
