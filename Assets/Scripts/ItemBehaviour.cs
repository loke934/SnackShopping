using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemBehaviour : MonoBehaviour
{
    [Header("SCORES")]
    [SerializeField,Range(-10,-200)] private int scoreBadStuff = -150;
    [SerializeField,Range(10,200)] private int scoreItem = 100;
    
    private int _typeOfScore;
    
    public UnityEvent<Music.Sound> onPlaySound = new UnityEvent<Music.Sound>();
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && gameObject.CompareTag("Bad Stuff")) {
            _typeOfScore = scoreBadStuff;
            onPlaySound.Invoke(Music.Sound.Bad);
        }
        else if (other.CompareTag("Player") && gameObject.CompareTag("Shop Item")) {
            _typeOfScore = scoreItem;
            onPlaySound.Invoke(Music.Sound.Good);
        }
        other.gameObject.GetComponent<Score>().TotalScore = _typeOfScore;
        Destroy(gameObject);
    }
}
