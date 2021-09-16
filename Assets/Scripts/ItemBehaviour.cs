using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemBehaviour : MonoBehaviour
{
    [Header("ITEM POINTS")]
    [SerializeField,Range(-100,-200)] private int scoreBadStuff = -75;
    [SerializeField,Range(10,200)] private int scoreStoreItem = 50;
    
    private int _typeOfScore;
    
    public UnityEvent<Music.Sound> onPlaySound = new UnityEvent<Music.Sound>();
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && gameObject.CompareTag("Bad Stuff")) {
            _typeOfScore = scoreBadStuff;
            onPlaySound.Invoke(Music.Sound.Bad);
        }
        else if (other.CompareTag("Player") && gameObject.CompareTag("Shop Item")) {
            _typeOfScore = scoreStoreItem;
            onPlaySound.Invoke(Music.Sound.Good);
        }
        other.gameObject.GetComponent<Score>().TotalScore = _typeOfScore;
        Destroy(gameObject);
    }
}
