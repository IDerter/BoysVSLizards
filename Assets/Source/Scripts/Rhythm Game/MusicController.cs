using Fungus;
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
        private string _victoryBlockName, _loseBlockName;

        public GameObject _prefabGame;

        [SerializeField] private Flowchart _flowchart;

        private void Start()
        {
            _scoreText.text = "西觇: 0";
            SpawnerArrows.Instance.EndMusicBattle += EndMusicBattle;
            ButtonController.Instance.ButtonPressed += ButtonPressed;
        }

        public void Init(string _victoryBlockName, string _loseBlockName)
        {
            this._victoryBlockName = _victoryBlockName;
            this._loseBlockName = _loseBlockName;
            _scoreText.text = "西觇: 0";
            SpawnerArrows.Instance.enabled = true;
            ButtonController.Instance.enabled = true;
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
                _flowchart.ExecuteBlock(_victoryBlockName);
            }
            else
            {
                Debug.Log("You Lose!");
                _flowchart.ExecuteBlock(_loseBlockName);
            }
            _prefabGame.SetActive(false);
            ButtonController.Instance.enabled = false;
            SpawnerArrows.Instance.TimeEndFight = SpawnerArrows.Instance.StartTimeEndFight;
        }

        public void NoteHit()
        {
            _currentScore += _scorePerNote;
            Debug.Log("Note - hit!");
            _scoreText.text = "西觇: " + _currentScore;
            
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
                _scoreText.text = "西觇: " + _currentScore;
            }
        }
    }
}