﻿using System;
using ExampleTemplate;
using UnityEngine;
using UnityEngine.UI;

namespace Model.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] SetScore _uiscore;

        private Animator _animator;
        private Rigidbody[] _rigidbodies;
        private EnemiesData _enemiesData;
        private RaycastHit _hit;
        private int _enemyLayer = 1 << 9;
        private int _score = 0;
        private int _health;

        public static event Action<int> OnScoreChangedUi;
        public static event Action<float> OnHealthChangedUi;
        public static event Action OnDeath;



        #region Properties

        public Rigidbody[] Rigidbody => _rigidbodies;
        public int Score => _score;

        #endregion


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            SetRagdollState(false);
            SetMainPhysics(true);
            _enemiesData = Data.Instance.EnemiesData;
            _health = _enemiesData.GetHealth();

            //_uiscore = GameObject.FindWithTag("Score").GetComponent<SetScore>();
        }

        public void Update()
        {
            //_uiscore.InstantiateScore(_score);
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

        private bool IsGround()
        {
            if (Physics.Raycast(_rigidbodies[0].transform.position, Vector3.down, out _hit, 0.3f, ~_enemyLayer))
            {
                return true;
            }
            else return false;
        }

        public void AddPoint(int points)
        {
            _score += points;
            OnScoreChangedUi?.Invoke(_score);
        }

        public void HealthDecrease(int damage) 
        {
            _health -= damage;
            if (_health <= 0) { Death(); }
            OnHealthChangedUi?.Invoke((float)_health / _enemiesData.GetHealth());
        }
        public void Death()
        {
            OnDeath?.Invoke();
        }

        public void TossUp()
        {
            if (!_animator.isActiveAndEnabled && IsGround())
            {
                gameObject.GetComponentInChildren<Rigidbody>().AddForce(0, 400, 0, ForceMode.Impulse);
            }
        }

        //private void OnGUI()
        //{
        //    GUILayout.BeginArea(new Rect(Screen.width - 150, 0, 150, 150));
        //    GUILayout.Label("SCORE: " + _score);
        //    GUILayout.EndArea();    
        //}
    }
}