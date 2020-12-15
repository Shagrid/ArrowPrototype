using UnityEngine;
using UnityEngine.UI;
using Model.Enemy;

namespace ExampleTemplate
{
    public class EndGameMenuBehaviour : BaseUi
    {
        #region Fields

        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Text _scoreCount;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _menuButton.onClick.AddListener(Call);
            _restartLevelButton.onClick.AddListener(RestartLevel);
            EnemyBehaviour.OnScoreChangedUi += TextScore;
        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(Call);
            _restartLevelButton.onClick.RemoveListener(RestartLevel);
            EnemyBehaviour.OnScoreChangedUi -= TextScore;
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
            ScreenInterface.GetInstance().Execute(ScreenType.GameMenu);
        }
        private void TextScore(int score)
        {
            _scoreCount.text = score.ToString();
        }

        #endregion
    }
}
