using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField, Range(10f, 25f)] private float gameTime = 15f;
    [SerializeField] private UI canvasUI;
    private float _timer;

    public UI CanvasUI {
        set {canvasUI = value;} 
    }
    public float Timer { 
        get {return _timer;}  
    }
    public float GameTime {
        get { return gameTime; }
    }

    private void Awake()
    {
        _timer = gameTime;
    }

    void Update() {
        _timer -= Time.deltaTime;
        if (_timer <= 0f) {
            canvasUI.UpdateScore();
        }
    }
}
