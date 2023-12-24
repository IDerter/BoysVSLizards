using Fungus;
using UnityEngine;

namespace BoysVsLizards
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private SlidingFight _slidingFight;
        [SerializeField] private EnemySettingSO _lizardEnemy;
        [SerializeField] private EnemySettingSO _lizardGeneralEnemy;


        public void StartLizardBattle(string victoryBlockName, string loseBlockName)
        {
            _slidingFight.gameObject.SetActive(true);
            _slidingFight.Init(_lizardEnemy.Health, _lizardEnemy.Damage, victoryBlockName, loseBlockName);
        }

        public void StartGeneralLizardBattle(string victoryBlockName, string loseBlockName)
        {
            _slidingFight.Init(_lizardGeneralEnemy.Health, _lizardGeneralEnemy.Damage, victoryBlockName, loseBlockName);
        }
    }
}