﻿using ExampleTemplate;
using UnityEngine;

namespace ExampleTemplate
{
    public class LoadLevelFromEditor : MonoBehaviour
    {
        [SerializeField] private LevelType _levelType;
        [SerializeField] private EnemyType _enemyType;

        public void Load()
        {
            if (Application.isPlaying)
                Services.Instance.LevelLoadService.LoadLevel(_levelType, _enemyType);
        }
    }
}