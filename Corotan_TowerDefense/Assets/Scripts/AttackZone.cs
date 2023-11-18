using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class AttackZone : MonoBehaviour
{
    [SerializeField] Transform _origin;
    [SerializeField] GameObject _b;
    private Enemy _tEnemy;
    private TowerType _type;
    private Coroutine _bSpawn;
    private float _rlsp, _arng, _dmg, _bsp, _srlsp;
    void Start()
    {
        _bsp = transform.parent.GetComponent<Tower>().GetBSP();
        _type = transform.parent.GetComponent<Tower>().GetTowerType();
    }

    void Update()
    {
        UpdateStats();
        if (_tEnemy && _bSpawn == null) 
        {
            if (_tEnemy.GetEnemyType() == EnemyType.flying && _type == TowerType.bomb) return;
            else _bSpawn = StartCoroutine(SpawnBullets());           
        }
    }

    void OnTriggerStay2D(Collider2D obj)
    {
        if(!_tEnemy) 
            if (obj.gameObject.CompareTag("Enemy")) 
                _tEnemy = obj.gameObject.GetComponent<Enemy>();

        if(obj.gameObject.CompareTag("Enemy"))
            if (obj.gameObject.GetComponent<Enemy>().GetEnemyType() == EnemyType.boss)
                transform.parent.GetComponent<Tower>().SetSlowStatus(true);
        

    }

    void OnTriggerExit2D(Collider2D obj)
    {
        //bool isSlowed = transform.GetComponentInParent<Tower>().GetSlowStatus();

        if (obj.gameObject.CompareTag("Enemy"))
            if(obj.GetComponent<Enemy>().GetEnemyType() == EnemyType.boss) 
                transform.parent.GetComponent<Tower>().SetSlowStatus(false);
        
        if(_tEnemy) 
            if(obj.gameObject.CompareTag("Enemy")) _tEnemy = null;


    }

    void UpdateStats()
    {
        _dmg = transform.parent.GetComponent<Tower>().GetDMG();
        _rlsp = transform.parent.GetComponent<Tower>().GetRLSP();
        _arng = transform.parent.GetComponent<Tower>().GetARNG();
        _srlsp = _rlsp * 2f;
        GetComponent<CircleCollider2D>().radius = _arng;
    }
    void StopSpawn()
    {
        if(_bSpawn != null)
        {
            StopCoroutine(_bSpawn);
            _bSpawn = null;
        }
    }

    IEnumerator SpawnBullets()
    {
        while (_tEnemy)
        {
            if(GameManager.Instance._gStatus)
            {
                GameObject _bullet = Instantiate(_b, _origin.position, transform.rotation);
                _bullet.GetComponent<Bullet>().SetTarget(_tEnemy);
                _bullet.GetComponent<Bullet>().SetBulletType(_type);
                _bullet.GetComponent<Bullet>().SetDMG(_dmg);
                _bullet.GetComponent<Bullet>().SetSP(_bsp);

                float crlsp;
                bool isSlowed = transform.parent.GetComponent<Tower>().GetSlowStatus();
                if (isSlowed) crlsp = _srlsp;
                else crlsp = _rlsp; 

                yield return new WaitForSeconds(crlsp);  
            }
            else yield return null;
        }
        yield return new WaitForSeconds(_rlsp);  
        StopSpawn();
    }
}
