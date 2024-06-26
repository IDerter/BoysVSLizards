﻿using Fungus;
using UnityEngine;

namespace BoysVsLizards
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private SlidingFight _slidingFight;
        [SerializeField] private MusicController _rhythmGame;
        [SerializeField] private EnemySettingSO _lizardEnemy;
        [SerializeField] private EnemySettingSO _lizardGeneralEnemy;
        [SerializeField] private EnemySettingSO _boysSettings;


        public void StartLizardBattle(string victoryBlockName, string loseBlockName)
        {
            _slidingFight.gameObject.SetActive(true);
            _slidingFight.Init(_lizardEnemy.Health, _lizardEnemy.Damage, victoryBlockName, loseBlockName);
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
    }
}