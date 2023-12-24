using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoysVsLizards
{
    public class ButtonController : SingletonBase<ButtonController>
    {
        public event Action ButtonPressed;

        private SpriteRenderer _theSR;

        [SerializeField] private Sprite _defaultImage;
        [SerializeField] private Sprite _pressedImage;

        [SerializeField] private KeyCode _keyToPress;

        private void Start()
        {
            _theSR = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(_keyToPress))
            {
                _theSR.sprite = _pressedImage;
                Debug.Log("TestPress");
                ButtonPressed?.Invoke();
            }

            if (Input.GetKeyUp(_keyToPress))
            {
                _theSR.sprite = _defaultImage;
            }
        }
    }
}