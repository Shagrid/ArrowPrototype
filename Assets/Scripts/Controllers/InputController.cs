using System;
using UnityEngine;


namespace ExampleTemplate
{
    public sealed class InputController : IExecute,IListenerScreen
    {
        private readonly CharacterData _characterData;
        private readonly EnemiesData _enemiesData;
        private Rigidbody _arrowRb;
        private bool _isActive;

        public InputController()
        {
            _characterData = Data.Instance.Character;
            _enemiesData = Data.Instance.EnemiesData;
            ScreenInterface.GetInstance().AddObserver(ScreenType.GameMenu, this);
        }
        
         #region IExecute
        
        public void Execute()
        {
            if (!Services.Instance.LevelLoadService.IsLvlLoaded())
            {
                return;
            }
            if (!_isActive) { return; }

            switch (_characterData.CharacterBehaviour.GameMode)
            {
                case GameModeType.None:
                    break;
                case GameModeType.Start: Touch();
                    break;
                case GameModeType.ArrowFly: Move();
                    break;
                case GameModeType.Ragdoll:Impulse();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
         #endregion

         private void Touch()
         {
             
                 if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
                 {
                     _characterData.CharacterBehaviour.SetGameMode(GameModeType.ArrowFly);
                     _characterData.ArrowBehaviour.transform.SetParent(null);
                     _arrowRb = _characterData.ArrowBehaviour.transform.GetComponent<Rigidbody>();
                     _arrowRb.isKinematic = false;
                     _characterData.ArrowBehaviour.FollowArrow(true);
                 }
                 
         }

         private void Move()
         {
             _characterData.ArrowBehaviour.Fly();
             Vector2 inputAxis = new Vector2();
             if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
             { 
                 
                 inputAxis.x = Input.GetAxis("Mouse Y");
                 inputAxis.y = Input.GetAxis("Mouse X");
             }

             if (Application.platform == RuntimePlatform.Android)
             {
                 var touch = Input.GetTouch(0);
                 if(touch.deltaPosition.x != 0f)
                 {
                     inputAxis.y = touch.deltaPosition.x;
                 }
                 if(touch.deltaPosition.y != 0f){
                     inputAxis.x = touch.deltaPosition.y;
                 }
             }
             
             if (inputAxis.x != 0 || inputAxis.y != 0)
             {
                 _characterData.ArrowBehaviour.Turn(inputAxis);
             }   
         }

        private void Impulse()
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                _enemiesData.EnemyBehaviour.TossUp();
            }
        }

        #region IListenerScreen

        public void ShowScreen()
        {
            _isActive = true;
        }

        public void HideScreen()
        {
            _isActive = false;
        }

        #endregion
    }
}
