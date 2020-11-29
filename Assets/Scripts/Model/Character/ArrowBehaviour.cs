using System;
using Model.Enemy;
using UnityEngine;

namespace ExampleTemplate
{
    public sealed class ArrowBehaviour : MonoBehaviour
    {
        private CharacterData _characterData;
        private Vector3 _currentEulerAngles;
        [SerializeField] private Camera _arrowCamera;
        private Camera _mainCamera;
        private Transform _arrowMesh;
        private Rigidbody _myrb;

        private void Awake()
        {
            _characterData = Data.Instance.Character;
            _arrowMesh = GetComponentInChildren<MeshRenderer>().transform;
            _myrb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            var enemy = other.transform.root.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemy.Die();
                FollowArrow(false);
                var rb =  other.transform.GetComponent<Rigidbody>();
                rb.AddForce(transform.TransformDirection(Vector3.forward) * _characterData.GetForceimpulse());
                _characterData.CharacterBehaviour.SetGameMode(GameModeType.Ragdoll);
                gameObject.SetActive(false);
                return;
            }

            if (other.transform.CompareTag("Ground"))
            {
                _characterData.CharacterBehaviour.SetGameMode(GameModeType.None);
            }
        }

       

        public void Fly()
        {
            var speedRotate = Data.Instance.Character.GetSpeedRotateArrow();
            transform.Translate(Vector3.forward * Data.Instance.Character.GetSpeed());
            _arrowMesh.Rotate(Vector3.forward, speedRotate * Time.deltaTime, Space.Self);
        }

        public void Turn(Vector3 axis)
        {
            _currentEulerAngles = transform.eulerAngles;
            _currentEulerAngles.x -= axis.x * _characterData.GetTurnSensivity();
            _currentEulerAngles.y += axis.y * _characterData.GetTurnSensivity();
            transform.eulerAngles = _currentEulerAngles;
        }

        public void FollowArrow(bool isActive)
        {
            if (isActive)
            {
                _mainCamera = Services.Instance.CameraServices.CameraMain;
                _mainCamera.enabled = false;
                _arrowCamera.enabled = true;
                Services.Instance.CameraServices.SetCamera(_arrowCamera);
            }
            else
            {
                
                  _mainCamera.enabled = true;
                  _mainCamera.transform.position = _arrowCamera.transform.position;
                  _mainCamera.transform.eulerAngles = _arrowCamera.transform.eulerAngles;
                 _arrowCamera.enabled = false;
                 Services.Instance.CameraServices.SetCamera(_mainCamera);
                
            }
        }
        
        
    }
}