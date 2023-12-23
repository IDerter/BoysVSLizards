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
        [SerializeField] private float _timeEndFight = 30f;


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
                    gameObject.SetActive(false);
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

                    _timer = UnityEngine.Random.Range(0.35f, 0.8f);
                }
            }
        }
    }
}