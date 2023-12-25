using UnityEngine;

namespace BoysVsLizards
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private SlidingFight _slidingFight;
        [SerializeField] private MusicController _rhythmGame;
        [SerializeField] private EnemySettingSO _lizardHvostEnemy;
        [SerializeField] private EnemySettingSO _lizardKirpichEnemy;
        [SerializeField] private EnemySettingSO _lizardGeneralEnemy;
        [SerializeField] private EnemySettingSO _boysSettings;


        public void StartHvostLizardBattle(string victoryBlockName, string loseBlockName)
        {
            _slidingFight.gameObject.SetActive(true);
            _slidingFight.Init(_lizardHvostEnemy.Health, _lizardHvostEnemy.Damage, victoryBlockName, loseBlockName);
        }
        
        public void StartKirpichLizardBattle(string victoryBlockName, string loseBlockName)
        {
            _slidingFight.gameObject.SetActive(true);
            _slidingFight.Init(_lizardKirpichEnemy.Health, _lizardKirpichEnemy.Damage, victoryBlockName, loseBlockName);
        }


        public void StartDanceBattle(string victoryBlockName, string loseBlockName)
        {
            _rhythmGame.enabled = true;
            _rhythmGame._prefabGame.SetActive(true);
            _rhythmGame.Init(victoryBlockName, loseBlockName);
        }

        public void StopDanceBattle()
        {
            _rhythmGame.enabled = false;
        }

        public void StartBoysBattle(string victoryBlockName, string loseBlockName)
        {
            _slidingFight.gameObject.SetActive(true);
            _slidingFight.Init(_boysSettings.Health, _boysSettings.Damage, victoryBlockName, loseBlockName);
        }

        public void StartGeneralLizardBattle(string victoryBlockName, string loseBlockName)
        {
            _slidingFight.Init(_lizardGeneralEnemy.Health, _lizardGeneralEnemy.Damage, victoryBlockName, loseBlockName);
        }

        public void InitMusic()
        {
            _slidingFight.InitMusic();
        }

        public void StopMusic()
        {
            _slidingFight.StopMusic();
        }
    }
}