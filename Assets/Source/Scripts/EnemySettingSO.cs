using UnityEngine;

namespace BoysVsLizards
{
    [CreateAssetMenu()]
    public class EnemySettingSO : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _damage;

        public float Damage => _damage;
        public float Health => _health;
    }
}