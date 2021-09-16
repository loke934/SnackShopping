using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
   [SerializeField] private Sprite upSprite;
   [SerializeField] private Sprite downSprite;
   [SerializeField] private Sprite rightSprite;
   [SerializeField] private Sprite leftSprite;

   private Dictionary<MoveState, Sprite> _moveStateDictionary;

   public enum MoveState {
      Up,
      Down,
      Right,
      Left
   }
   
   public void ChangeSprite(MoveState direction) {
      gameObject.GetComponent<SpriteRenderer>().sprite = SelectSprite(direction);
      
      if (direction == MoveState.Left) {
         gameObject.GetComponent<SpriteRenderer>().flipX = false;
      }
      else if (direction == MoveState.Right) {
         gameObject.GetComponent<SpriteRenderer>().flipX = true;
      }
   }
   
   private Sprite SelectSprite(MoveState direction) {
      return _moveStateDictionary[direction];
   }
   
   private void Awake() {
      _moveStateDictionary = new Dictionary<MoveState, Sprite>();
      _moveStateDictionary.Add(MoveState.Up, upSprite);
      _moveStateDictionary.Add(MoveState.Down, downSprite);
      _moveStateDictionary.Add(MoveState.Right, rightSprite);
      _moveStateDictionary.Add(MoveState.Left, leftSprite);
   }
}
