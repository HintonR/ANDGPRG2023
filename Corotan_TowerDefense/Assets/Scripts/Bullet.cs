using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject _dZone;
    private float _sp, _rsp, _dmg;
    private Enemy _target;
    private TowerType _type;

    public void SetTarget(Enemy obj) { _target = obj; }
    public void SetBulletType(TowerType type) { _type = type; }
    public void SetSP(float sp) { _sp = sp; }
    public void SetDMG(float dmg) { _dmg = dmg; }
    void Start()
    {
        Init();
        _rsp = 90f;
        if(_target) CalculateDamage(_target);
    }

    void Update()
    {
        Pathing();
        if(!_target) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (_target)
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                if(_type == TowerType.bomb || _type == TowerType.ice) AreaDamage();
                else if (_type == TowerType.fire) StartCoroutine(DamageOverTime());
                else enemy.GetComponent<Enemy>().TakeDamage(_dmg);

                if (_type != TowerType.fire) Destroy(gameObject);
                else GetComponent<SpriteRenderer>().enabled = false;
            }
    }

    void Pathing() 
    {       
        if(_target)
        {         
            Vector2 direction = _target.GetComponent<Transform>().position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rsp * Time.deltaTime);

            if(GameManager.Instance._gStatus) transform.Translate(Vector2.right * _sp * Time.deltaTime);
            else transform.Translate(Vector2.right * 0 * Time.deltaTime);
        }
    }

    void CalculateDamage(Enemy enemy)
    {
        if(enemy.GetEnemyType() == EnemyType.flying && _type == TowerType.arrow) _dmg *= 1.2f;
    }

    void Init()
    {
        if(_type == TowerType.arrow) 
        {
           GetComponent<SpriteRenderer>().color = Color.black;
        }
        if(_type == TowerType.bomb) 
        {
            GetComponent<SpriteRenderer>().color = GameManager.Instance.HexToColor("#9A9A9A");
            GetComponent<Transform>().localScale = new Vector3(0.33f,0.33f,0.33f);
            _dZone.GetComponent<CircleCollider2D>().radius = 2.25f;
        }
        if(_type == TowerType.ice) 
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            GetComponent<Transform>().localScale = new Vector3(0.25f,0.25f,0.25f);
            _dZone.GetComponent<CircleCollider2D>().radius = 1.5f;
        }    
        if(_type == TowerType.fire) 
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<Transform>().localScale = new Vector3(0.2f,0.2f,0.2f);
        }
    } 
    void AreaDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _dZone.GetComponent<CircleCollider2D>().radius);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Enemy")) 
            {
                if(_type == TowerType.ice) enemy.GetComponent<Enemy>().SetChillFlag(true);                
                
                if (enemy.GetComponent<Enemy>().GetEnemyType() == EnemyType.flying && _type == TowerType.bomb) 
                    enemy.GetComponent<Enemy>().TakeDamage(0);
                else 
                    enemy.GetComponent<Enemy>().TakeDamage(_dmg);
            }
        }
    }
      
    IEnumerator DamageOverTime()
    {
        float dmgTick = _dmg;
        _target.gameObject.GetComponent<Enemy>().SetBurnFlag(true);
        while (dmgTick > 0)
        {
            if (GameManager.Instance._gStatus) 
            {
                if(_target) _target.GetComponent<Enemy>().TakeDamage(1);
                dmgTick -= 1f;
                yield return new WaitForSeconds(1f);
            }
            else yield return null;    
        }
        if(_target) _target.gameObject.GetComponent<Enemy>().SetBurnFlag(false);
        Destroy(gameObject);
    }



}
