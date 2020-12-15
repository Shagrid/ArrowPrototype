using Model.Enemy;
using UnityEngine;

namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "Enemies", menuName = "Data/Enemies/EnemiesData")]
    public class EnemiesData : ScriptableObject
    {
        [SerializeField] private int _health; 

        [HideInInspector] public EnemyBehaviour EnemyBehaviour;
        
        public void Initialization(EnemyType enemyType, Transform point)
        {
            var enemyBehaviour = CustomResources.Load<EnemyBehaviour>
                (AssetsPathEnemiesGameObject.EnemyGameObject[enemyType]);
            EnemyBehaviour = Instantiate(enemyBehaviour, point.position, point.rotation);
        }

        public int GetHealth()
        {
            return _health;
        }
    }
}