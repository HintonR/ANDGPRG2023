using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnControl : MonoBehaviour
{
    [SerializeField] int _sLimit;
    [SerializeField] GameObject _enemy;

    private int _sCount = 0;
    private float _sSpeed;
    private Coroutine _eSpawn;
    void Start()
    {
        _sSpeed = 1f;
    }

    void Update()
    {

    }
    IEnumerator SpawnEnemies()
    {
        int mixedIndex = 0;
        int types;
        EnemyType[] eTypes;

        while (_sCount < _sLimit)
        {
            yield return new WaitForSeconds(_sSpeed);
            if (GameManager.Instance._gStatus)
            {
                GameObject _unit = Instantiate(_enemy, GetComponent<Transform>().position, transform.rotation);
                _sCount++;
                if (_unit.GetComponent<Enemy>().GetEnemyType() == EnemyType.mixed || _unit.GetComponent<Enemy>().GetEnemyType() == EnemyType.bossmixed)
                {
                    if (_unit.GetComponent<Enemy>().GetEnemyType() == EnemyType.mixed)
                    {
                        types = 2;
                        eTypes = new EnemyType[] { EnemyType.ground, EnemyType.flying };
                    }
                    else
                    {
                        types = 3;
                        eTypes = new EnemyType[] { EnemyType.ground, EnemyType.flying, EnemyType.boss };
                    }
                    EnemyType enemyType = eTypes[mixedIndex];
                    _unit.GetComponent<Enemy>().SetEnemyType(enemyType);
                    mixedIndex = (mixedIndex + 1) % types;
                }
            }
            else yield return null;
        }
        yield return new WaitForSeconds(_sSpeed + 1f);
        GameManager.Instance._wave++;
        if (GameManager.Instance._wave < 5) GameManager.Instance._dMultiplier += 0.25f;
        else if (GameManager.Instance._wave >= 5 && GameManager.Instance._wave < 10) GameManager.Instance._dMultiplier += 0.33f;
        else if (GameManager.Instance._wave >= 10 && GameManager.Instance._wave < 20) GameManager.Instance._dMultiplier += 0.66f;
        else if (GameManager.Instance._wave >= 20 && GameManager.Instance._wave < 30) GameManager.Instance._dMultiplier += 0.75f;
        else if (GameManager.Instance._wave >= 30 && GameManager.Instance._wave < 50) GameManager.Instance._dMultiplier += 0.9f;
        else if (GameManager.Instance._wave >= 50) GameManager.Instance._dMultiplier += 1.2f;
        StopCoroutine(_eSpawn);
    }
    public void StartSpawn() 
    {
        if (GameManager.Instance._enemies.Count == 0)
        {
            _sCount = 0;
            _eSpawn = StartCoroutine(SpawnEnemies());
        }
    }
}
