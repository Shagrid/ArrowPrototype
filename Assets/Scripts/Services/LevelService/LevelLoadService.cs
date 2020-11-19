using UnityEngine;

namespace ExampleTemplate
{
    public class LevelLoadService : Service
    {
        private GameObject _currentLevel;

        public void LoadLevel(LevelType levelType, EnemyType enemyType, CharacterType characterType)
        {
            DestroyLevel();
            _currentLevel = GameObject.Instantiate(Data.Instance.LevelsData.GetPrefabLevel(levelType));
            var characterPosition = GameObject.FindWithTag(TagManager.GetTag(TagType.CharacterPosition)).transform;
            var enemyPosition = GameObject.FindWithTag(TagManager.GetTag(TagType.EnemyPosition)).transform;
            Data.Instance.Character.Initialization(characterType, characterPosition);
            Data.Instance.EnemiesData.Initialization(enemyType, enemyPosition);
            Data.Instance.Character.CharacterBehaviour.SetGameMode(GameModeType.Start);
            Time.timeScale = 1;
        }

        public void DestroyLevel()
        {
            if (_currentLevel == null) return;
            GameObject.Destroy(_currentLevel);
            GameObject.Destroy(Data.Instance.Character.CharacterBehaviour.gameObject);
            GameObject.Destroy(Data.Instance.EnemiesData.EnemyBehaviour.gameObject);
        }

        public bool IsLvlLoaded()
        {
            return _currentLevel != null;
        }
        
    }
}