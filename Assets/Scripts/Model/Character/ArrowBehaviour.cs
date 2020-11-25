using System;
using Model.Enemy;
using UnityEngine;

namespace ExampleTemplate
{
    public sealed class ArrowBehaviour : MonoBehaviour
    {
        private CharacterData _characterData;
        private Vector3 _currentEulerAngles;

        private void Awake()
        {
            _characterData = Data.Instance.Character;
        }

        private void OnCollisionEnter(Collision other)
        {
            var enemy = other.transform.root.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemy.Die();
                var rb =  other.transform.GetComponent<Rigidbody>();
                rb.AddForce(transform.TransformDirection(Vector3.forward) * _characterData.GetForceimpulse());
                _characterData.CharacterBehaviour.SetGameMode(GameModeType.Ragdoll);
                gameObject.SetActive(false);
            }
        }

        public void Fly()
        {
            transform.Translate(Vector3.forward * Data.Instance.Character.GetSpeed());
        }

        public void Turn(Vector3 axis)
        {
            _currentEulerAngles = transform.eulerAngles;
            _currentEulerAngles.x -= axis.x * _characterData.GetTurnSensivity();
            _currentEulerAngles.y += axis.y * _characterData.GetTurnSensivity();
            transform.eulerAngles = _currentEulerAngles;
        }
        
        
    }
}