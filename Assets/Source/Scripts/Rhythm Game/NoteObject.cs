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

        private void Start()
        {
            _beatTemp /= 60f;
        }


        private void Update()
        {
            transform.position -= new Vector3(0f, _beatTemp * Time.deltaTime, 0f);

            if (Input.GetKeyDown(_keyToPress))
            {
                if (_canPressed)
                {
                    _canPressed = false;
                    gameObject.SetActive(false);

                    MusicController.Instance.NoteHit();
                }
            }
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Activator")
            {
                _canPressed = true;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.tag == "Activator")
            {
              
                Debug.Log("Вышел из триггера!");
                if (_canPressed)
                    MusicController.Instance.NoteMissed();
            }
        }
    }
}