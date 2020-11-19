using System;
using ExampleTemplate;
using UnityEngine;

namespace Model.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Die()
        {
            _animator.enabled = false;
        }
    }
}