using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private UIHandler _uiH;
    private AudioManager _aM;
    [SerializeField] public List<Transform> _poi = new List<Transform>();
    public List<GameObject> _enemies = new List<GameObject>();
    public Cursor _cursor { get; set; }
    public int _gold, _score, _lives, _wave;
    public float _dMultiplier = 1f;
    public bool _gStatus, _tStatus;

    public Color HexToColor(string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color; 
    }
    public float TruncateFloat(float value)
    {
        float fVal = value;
        float tVal = Mathf.Pow(10f, 2) * fVal;
        tVal = Mathf.Round(tVal);
        tVal /= Mathf.Pow(10f, 2);
        return tVal;
    }

    public void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    void Awake()
    {
        Instance = this;
        _gold = 0;
        _score = 0;
        _lives = 1;
        _wave = 0;
        _gStatus = false;
    }
    
    void Start()
    {
        _uiH = UIHandler.Instance;
        _aM = AudioManager.Instance;
    }
    void Update()
    {
        OpenTowerMenu();
        OpenUpgradeMenu();
        PauseGame();
        GameOver();
        if(_gStatus)
        {
            if (_enemies.Count == 0) _aM.ChangeBGM(_aM._buildBGM);
            else _aM.ChangeBGM(_aM._battleBGM);
        }
    }

    void OpenTowerMenu()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _uiH._mState == MenuState.off)
            if(_cursor.GetComponent<Cursor>().GetBuildState()) _uiH._mState = MenuState.tower;
    }
    void OpenUpgradeMenu()
    {
         if (Input.GetKeyUp(KeyCode.Space) && _uiH._mState == MenuState.off)
            if(_cursor.GetComponent<Cursor>().GetTowerState()) _uiH._mState = MenuState.upgrade;
    }

    void PauseGame()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && _gStatus && _uiH._mState == MenuState.off)
        {
            _gStatus = false;
            _uiH._mState = MenuState.pause;
        }
    }
    void GameOver()
    {
       if(_lives <= 0) 
        {
            _gStatus = false;            
            if(_enemies.Count != 0) foreach (GameObject enemy in _enemies) Destroy(enemy);

            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            foreach (GameObject tower in towers) Destroy(tower);

            _enemies.Clear();
            _uiH._mState = MenuState.gameover;
        }
    }

}
