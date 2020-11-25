using System;
using UnityEngine;

namespace ExampleTemplate
{
    [Serializable]
    public sealed class LevelData
    {
        public LevelType LevelType;
        public GameObject LocationPrefab;
    }
}