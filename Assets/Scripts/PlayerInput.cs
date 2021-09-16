using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [Header("PLAYER MOVEMENT")]
    [SerializeField, Range(0.05f, 1f)] private float speed = 0.1f; //used as time in coroutine

    private float _timer;
    private float _timeToEndGame;
    private ScenerySpawner _scenerySpawner;
    private TimeManager _timeManager;
    private Vector2Int _direction;
    private Vector2Int _newPosition;
    
    public UnityEvent<PlayerAnimation.MoveState> onChangeSprite;

    public ScenerySpawner ScenerySpawner {
        set { _scenerySpawner = value; }
    }
    public TimeManager TimeManager {
        set { _timeManager = value; }
    }

    private void SetNewPosition() {
        Vector2Int currentPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        
        if (Input.GetKey(KeyCode.W)) {
            _direction = Vector2Int.up;
            onChangeSprite.Invoke(PlayerAnimation.MoveState.Up);
        }
        else if (Input.GetKey(KeyCode.S)) {
            _direction = Vector2Int.down;
            onChangeSprite.Invoke(PlayerAnimation.MoveState.Down);
        }
        if (Input.GetKey(KeyCode.D)) {
            _direction = Vector2Int.right;
            onChangeSprite.Invoke(PlayerAnimation.MoveState.Right);
        }
        else if (Input.GetKey(KeyCode.A)) {
            _direction = Vector2Int.left;
            onChangeSprite.Invoke(PlayerAnimation.MoveState.Left);
        }
        _newPosition = currentPosition + _direction;
    }
    private IEnumerator AutoMovement() {
        while (_timer < _timeToEndGame) {
            yield return new WaitForSeconds(speed);
            
            if (_timer < 0f)
            {
                break;
            }
            if (InBounds(_newPosition)) {
                if (_scenerySpawner.GridArray[_newPosition.x,_newPosition.y] != null) {
                    transform.position = new Vector2(_newPosition.x, _newPosition.y);
                }
            }
            else {
                transform.position = transform.position;
            }
        }
    }
    private bool InBounds(Vector2Int newPosition) {
        
        if (newPosition.x >= 0 && newPosition.x < _scenerySpawner.GridArray.GetLength(0) && 
            newPosition.y >= 0 && newPosition.y < _scenerySpawner.GridArray.GetLength(1)) {
            return true;
        }
        return false;
    }

    private void Start() {
        onChangeSprite = new UnityEvent<PlayerAnimation.MoveState>();
        onChangeSprite.AddListener(GetComponent<PlayerAnimation>().ChangeSprite);
        _timeToEndGame = _timeManager.GameTime;
        transform.position = new Vector2((int)transform.position.x, (int)transform.position.y);
        StartCoroutine(AutoMovement());
    }
    private void Update() {
        _timer = _timeManager.Timer;
        SetNewPosition();
    }
}
