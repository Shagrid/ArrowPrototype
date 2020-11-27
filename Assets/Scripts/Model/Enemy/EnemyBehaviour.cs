using System;
using ExampleTemplate;
using UnityEngine;

namespace Model.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody[] _rigidbodies;
        private int _score = 0;

        #region Properties

        public Rigidbody[] Rigidbody => _rigidbodies;

        #endregion

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            SetRagdollState(false);
            SetMainPhysics(true);
        }

        public void Die()
        {
            SetRagdollState(true);
            SetMainPhysics(false);
        }
        
        private void SetRagdollState(bool activityState)
        {
            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].isKinematic = !activityState;
            }
        }

        private void SetMainPhysics(bool activityState)
        {
            _animator.enabled = activityState;
            //_rigidbodies[0].isKinematic = !activityState;
        }

        public void AddPoint(int points)
        {
            _score += points;
        }
        
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width - 150, 0, 150, 150));
            GUILayout.Label("SCORE: " + _score);
            GUILayout.EndArea();    
        }
    }
}