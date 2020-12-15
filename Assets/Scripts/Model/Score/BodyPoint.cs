using Model.Enemy;
using UnityEngine;
using System;

namespace ExampleTemplate
{
    public sealed class BodyPoint : MonoBehaviour
    {
        [SerializeField] private int BaseScore = 100;
        [SerializeField] private int _baseDamage = 50;
        private EnemyBehaviour _enemy;
        private int _lastCollisionObject;
        public static Action<Vector3, string, Color> OnScoreChanged;

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
                var damage = _baseDamage * barrier.GetDamageCoefficient();
                _lastCollisionObject = other.GetHashCode();
                _enemy.AddPoint((int)points);
                _enemy.HealthDecrease((int)damage);
                OnScoreChanged?.Invoke(gameObject.transform.position, _enemy.Score.ToString(), Color.red);
            }
        }
    }
}