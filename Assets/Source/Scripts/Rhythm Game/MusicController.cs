using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoysVsLizards
{
    public class MusicController : SingletonBase<MusicController>
    {
        [SerializeField] private AudioSource _music;
        [SerializeField] private bool _startPlaying;
        public bool GetStartingPlaying => _startPlaying;

        [SerializeField] private int _countHit;
        public int GetCountHit => _countHit;

        [SerializeField] private int _countMissed;
        public int GetCountMissed => _countMissed;

        private void Start()
        {
            SpawnerArrows.Instance.EndMusicBattle += EndMusicBattle;
        }

        private void OnDestroy()
        {
            SpawnerArrows.Instance.EndMusicBattle -= EndMusicBattle;
        }

        private void Update()
        {
            if (!_startPlaying)
            {
                if (Input.anyKeyDown)
                {
                    _startPlaying = true;
                    _music.Play();
                }
            }
        }

        private void EndMusicBattle()
        {
            _startPlaying = false;
            _music.Stop();

            if (_countHit > _countMissed * 2)
            {
                Debug.Log("You Win");

            }
            else
            {
                Debug.Log("You Lose!");

            }
        }

        public void NoteHit()
        {
            _countHit++;
            Debug.Log("Note - hit!");
        }

        public void NoteMissed()
        {
            _countMissed++;
            Debug.Log("Note - missed!");
        }
    }
}