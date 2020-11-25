using System;
using UnityEngine;


namespace ExampleTemplate
{
    public sealed class InputController : IExecute
    {
         private readonly CharacterData _characterData;
         private Rigidbody _arrowRb;
         
         public InputController()
        {
            _characterData = Data.Instance.Character;
            
        }
        
         #region IExecute
        
        public void Execute()
        {
            if (!Services.Instance.LevelLoadService.IsLvlLoaded())
            {
                return;
            }

            switch (_characterData.CharacterBehaviour.GameMode)
            {
                case GameModeType.None:
                    break;
                case GameModeType.Start: Touch();
                    break;
                case GameModeType.ArrowFly: Move();
                    break;
                case GameModeType.Ragdoll:
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
                 }
                 
         }

         private void Move()
         {
             _characterData.ArrowBehaviour.Fly();
             if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
             { 
                 Vector2 inputAxis;
                 inputAxis.x = Input.GetAxis("Mouse Y");
                 inputAxis.y = Input.GetAxis("Mouse X");
                 if (inputAxis.x != 0 || inputAxis.y != 0)
                 {
                     _characterData.ArrowBehaviour.Turn(inputAxis);
                 }         
                 
                 return;
             }

             if (Application.platform == RuntimePlatform.Android)
             {
                 
                 //TODO Сделать управлялку под андроид
             }
         }
    }
}
