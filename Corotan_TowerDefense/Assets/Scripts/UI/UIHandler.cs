using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    start,
    pause,
    gameover,
    tower,
    upgrade,
    off
}

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance;

    [SerializeField] GameObject _sMenu, _pMenu, _tMenu, _uMenu, _gMenu, _HUD;
    
    public MenuState _mState;
    public bool _hState;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _mState = MenuState.start;
    }

    void Update()
    {
        if(_mState == MenuState.off) _hState = true;
        else _hState = false;

        ToggleMenu();
        ToggleHUD();

    }

    void ToggleMenu()
    {
        if(_mState == MenuState.start) _sMenu.SetActive(true);
        else _sMenu.SetActive(false);
        if(_mState == MenuState.pause) _pMenu.SetActive(true);
        else _pMenu.SetActive(false);
        if(_mState == MenuState.gameover) _gMenu.SetActive(true);
        else _gMenu.SetActive(false);   
        if(_mState == MenuState.tower) _tMenu.SetActive(true);
        else _tMenu.SetActive(false);   
        if(_mState == MenuState.upgrade) _uMenu.SetActive(true);
        else _uMenu.SetActive(false);
    }

    void ToggleHUD()
    {
        if(_hState) _HUD.SetActive(true);
        else _HUD.SetActive(false);
    }
}
