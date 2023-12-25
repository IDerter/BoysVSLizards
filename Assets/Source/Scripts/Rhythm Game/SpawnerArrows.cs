using System;
using UnityEngine;

namespace BoysVsLizards
{
    public class SpawnerArrows : SingletonBase<SpawnerArrows>
    {
        public event Action EndMusicBattle;

        [SerializeField] private GameObject _spawnArrowObject;
        [SerializeField] private bool _hasStarted;
        private float _timer = 0f;
        [SerializeField] private float _startTimeEndFight;
        public float StartTimeEndFight => _startTimeEndFight;
        private float _timeEndFight;
        public float TimeEndFight { get { return _timeEndFight; } set { _timeEndFight = value; } }

        private void Start()
        {
            _timeEndFight = _startTimeEndFight;
        }

        private void Update()
        {
            if (MusicController.Instance.GetStartingPlaying)
            {
                if (_timeEndFight > 0)
                {
                    _timeEndFight -= Time.deltaTime;
                }
                else
                {
                    EndMusicBattle?.Invoke();
                    gameObject.GetComponent<SpawnerArrows>().enabled = false;
                }

                if (_timer >= 0)
                {
                    _timer -= Time.deltaTime;
                }
                else
                {
                    var coefAngle = UnityEngine.Random.Range(0, 3);
                    var obj = Instantiate(_spawnArrowObject, gameObject.transform);

                    Vector3 rotate = obj.transform.eulerAngles;
                    rotate.z = 90 * coefAngle;
                    obj.transform.rotation = Quaternion.Euler(rotate);

                    _timer = UnityEngine.Random.Range(0.5f, 1f);
                }
            }
        }
    }
}