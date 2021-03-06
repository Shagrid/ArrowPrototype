﻿using UnityEngine;
using UnityEngine.UI;
using Model.Enemy;

namespace ExampleTemplate
{
    public sealed class GameMenuBehaviour : BaseUi
    {
        #region Fields

        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Text _scoreCount;
        [SerializeField] private Image _healthImage;
        
        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            Time.timeScale = 1f;
            _menuButton.onClick.AddListener(Call);
            _restartLevelButton.onClick.AddListener(RestartLevel);
            EnemyBehaviour.OnScoreChangedUi += TextScore;
            EnemyBehaviour.OnHealthChangedUi += HealthShow;
            EnemyBehaviour.OnDeath += EndGameMenuCall;
        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(Call);
            _restartLevelButton.onClick.RemoveListener(RestartLevel);
            EnemyBehaviour.OnScoreChangedUi -= TextScore;
            EnemyBehaviour.OnHealthChangedUi -= HealthShow;
            EnemyBehaviour.OnDeath -= EndGameMenuCall;
        }

        #endregion
        

        #region Methods

        public override void Show()
        {
            gameObject.SetActive(true);
            ShowUI.Invoke();
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            HideUI.Invoke();
        }

        private void Call()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.MainMenu);
        }
        private void RestartLevel()
        {
            Services.Instance.LevelLoadService.LoadLevel();
        }
        private void TextScore(int score)
        {
            _scoreCount.text = score.ToString();
        }
        private void HealthShow(float health)
        {
            _healthImage.fillAmount = health;
        }
        private void EndGameMenuCall()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.EndGameMenu);
        }

        #endregion
    }
}
