using Model.Enemy;
using UnityEngine;

namespace ExampleTemplate
{
    public sealed class BodyPoint : MonoBehaviour
    {
        [SerializeField] private int BaseScore = 100;
        private EnemyBehaviour _enemy;
        private int _lastCollisionObject;

        private void Awake()
        {
            _enemy = transform.root.GetComponent<EnemyBehaviour>();
        }

        private void OnCollisionEnter(Collision other)
        {
            var barrier = other.transform.GetComponent<BarrierCoefficient>();
            if (barrier != null)
            {
                if (other.GetHashCode() == _lastCollisionObject) return;
                var points = BaseScore * barrier.GetCoefficient();
                _lastCollisionObject = other.GetHashCode();
                _enemy.AddPoint((int)points);
            }
        }
    }
}