using System;
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

        public void Fly()
        {
            transform.Translate(Vector3.forward * Data.Instance.Character.GetSpeed());
        }

        public void Turn(Vector3 axis)
        {
            _currentEulerAngles = transform.eulerAngles;
            _currentEulerAngles.x += axis.x * _characterData.GetTurnSensivity();
            _currentEulerAngles.y -= axis.y * _characterData.GetTurnSensivity();
            transform.eulerAngles = _currentEulerAngles;
        }
    }
}