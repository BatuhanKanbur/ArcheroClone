﻿using System.Threading.Tasks;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Gamecore.AssetManager.Constants;
using Gamecore.GameManager.Interface;
using Gamecore.MobManager.Interface;
using Gamecore.UIManager.Interface;
using Gameplay.Character.Interface;
using Gameplay.Player.Interface;
using Gameplay.Skill.Behaviour;
using UnityEngine;
using static Gamecore.ObjectManager.ObjectManager;

namespace Gamecore.GameManager.Behaviour
{
    using MobManager = MobManager.Behaviours.MobManager;
    using UIManager = Gamecore.UIManager.Behaviour.UIManager;
    public class GameManager : MonoBehaviour, IGameManager, ITargetManager
    {
        [SerializeField] private SkillManager skillManager;
        [SerializeField] private MobManager mobManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        public Transform GetTargetTransform => _player.Transform;
        private IUIManager _uiManager;
        private IPlayer _player;
        private int _score;
        private int Score
        {
            get => _score;
            set
            {
                if(_score != value)
                    _uiManager.UpdateScore(value);
                _score = value;
            }
        }

        private async void Start()
        {
            _uiManager = uiManager;
            _uiManager.UpdateScore(Score);
            await SpawnPlayer();
            skillManager.InitSkills(_player.Character,_uiManager).Forget();
            mobManager.SpawnMobs(this,OnMobDefeated).Forget();
        }

        private void OnMobDefeated(IMob defeatedMob)
        {
            Score += defeatedMob.EarnedScore;
        }

        private async UniTask SpawnPlayer()
        {
            var createdPlayer = await GetObject(AssetConstants.PlayerAddress,Vector3.zero,Quaternion.identity);
            createdPlayer.transform.SetParent(null);
            _player = createdPlayer.GetComponent<IPlayer>();
            _player.Initialize(mobManager, OnPlayerDeath);
            virtualCamera.Follow = createdPlayer.transform;
        }

        private void OnPlayerDeath()
        {
            ShowEndGamePanel().Forget();
        }

        private async UniTaskVoid ShowEndGamePanel()
        {
            await UniTask.Delay(1500);
            _uiManager.ShowEndGamePanel(Score);
        }
        public Transform[] GetClosetTargetPositions(Transform originTransform, int targetCount, float range)
        {
            return new[] {_player.Transform};
        }
      
    }
}
