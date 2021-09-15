using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI scoreTextBox;
   [SerializeField] private TextMeshProUGUI timerTextBox;
   private Score _score;
   private float _timer;
   private float _gameTime;

   public Score Score {
      set { _score = value; }
   }

   public float Timer
   {
      set { _timer = value; }
   }
   
   public void UpdateScore() {
      gameObject.GetComponentInChildren<Image>(true).gameObject.SetActive(true);
      scoreTextBox.text = "Your score is " + _score.TotalScore;
   }

   private void Update()
   {
      _timer -= Time.deltaTime;
      float time = (int)_timer;
      if (_timer > 0f)
      {
         timerTextBox.text = "Timer: " + time;
      }
      
   }
}
