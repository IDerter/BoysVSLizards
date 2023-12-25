using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoysVsLizards
{
    public class NoteObject : MonoBehaviour
    {
        [SerializeField] private float _beatTemp;
        [SerializeField] private bool _canPressed;

        [SerializeField] private KeyCode _keyToPress;
        private Animator _arrowAnimator;

        private float _timeDie = 3f;

        private void Start()
        {
            _beatTemp /= 60f;
            _arrowAnimator = GetComponent<Animator>();
            SpawnerArrows.Instance.EndMusicBattle += EndMusicBattle;
        }

        private void OnDestroy()
        {
            SpawnerArrows.Instance.EndMusicBattle -= EndMusicBattle;
        }

        private void EndMusicBattle()
        {
            _arrowAnimator.enabled = true;
            Destroy(gameObject, _timeDie);
        }

        private void Update()
        {
            transform.position -= new Vector3(0f, _beatTemp * Time.deltaTime, 0f);

            if (Input.GetKeyDown(_keyToPress))
            {
                if (_canPressed)
                {
                    _canPressed = false;
                    _arrowAnimator.enabled = true;

                    MusicController.Instance.NoteHit();
                }

            }
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Activator")
            {
                _canPressed = true;

                MusicController.Instance.InTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.tag == "Activator")
            {
              
                Debug.Log("Вышел из триггера!");
                if (_canPressed)
                {
                    MusicController.Instance.NoteMissed();
                }
                _canPressed = false;

                MusicController.Instance.InTrigger = false;
            }
        }
    }
}