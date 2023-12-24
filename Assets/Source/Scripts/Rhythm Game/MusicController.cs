using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoysVsLizards
{
    public class MusicController : SingletonBase<MusicController>
    {
        [SerializeField] private AudioSource _music;
        [SerializeField] private bool _startPlaying;
        public bool GetStartingPlaying => _startPlaying;

        [SerializeField] private int _currentScore;
        [SerializeField] private int _scorePerNote = 100;

        [SerializeField] private Text _scoreText;
        [SerializeField] private bool _inTrigger;
        public bool InTrigger {get { return _inTrigger; } set { _inTrigger = value; } }

        [SerializeField] private int _conditionWinScore;
        public int GetConditionWinScore => _conditionWinScore;

        private void Start()
        {
            _scoreText.text = "Очки: 0";
            SpawnerArrows.Instance.EndMusicBattle += EndMusicBattle;
            ButtonController.Instance.ButtonPressed += ButtonPressed;
        }

        private void ButtonPressed()
        {
            if (!_inTrigger)
            {
                NoteMissed();
            }

            if (!_startPlaying)
            {
                if (Input.anyKeyDown)
                {
                    _startPlaying = true;
                    _music.Play();
                }
            }
        }

        private void OnDestroy()
        {
            SpawnerArrows.Instance.EndMusicBattle -= EndMusicBattle;
            ButtonController.Instance.ButtonPressed -= ButtonPressed;
        }


        private void EndMusicBattle()
        {
            _startPlaying = false;
            _music.Stop();

            if (_currentScore > _conditionWinScore)
            {
                Debug.Log("You Win");

            }
            else
            {
                Debug.Log("You Lose!");

            }
            ButtonController.Instance.enabled = false;
        }

        public void NoteHit()
        {
            _currentScore += _scorePerNote;
            Debug.Log("Note - hit!");
            _scoreText.text = "Очки: " + _currentScore;
            
        }

        public void NoteMissed()
        {
            if (_startPlaying)
            {
                Debug.Log("Note - missed!");
                if (_currentScore - _scorePerNote >= 0)
                {
                    _currentScore -= _scorePerNote;
                }
                _scoreText.text = "Очки: " + _currentScore;
            }
        }
    }
}