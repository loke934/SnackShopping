using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Music : MonoBehaviour
{
   [Header("MUSIC AND SOUNDS")]
   [SerializeField] private AudioSource music;
   [SerializeField] private AudioSource goodItems;
   [SerializeField] private AudioSource badItems;

   private Dictionary<Sound, AudioSource> _soundEffectsDictionary; 

   public enum Sound {
      Good,
      Bad,
   }

   private void Awake() {
      _soundEffectsDictionary = new Dictionary<Sound, AudioSource>();
      _soundEffectsDictionary.Add(Sound.Good, goodItems);
      _soundEffectsDictionary.Add(Sound.Bad, badItems);
   }

   private void Start() {
      if (!music.isPlaying) {
         music.Play();
      }
   }

   public void PlaySound(Sound sound) {
      AudioSource effect = GetSound(sound);
      if (!effect.isPlaying) {
         effect.Play();
      }
   }

   private AudioSource GetSound(Sound sound) {
      AudioSource effect = _soundEffectsDictionary[sound];
      return effect;
   }
}
