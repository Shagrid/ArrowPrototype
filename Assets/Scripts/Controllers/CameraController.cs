using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enemy;
using Model.Camera;

namespace ExampleTemplate
{
    class CameraController : IExecute
    {
        #region Fields

        private CameraBehaviour _cameraBehaviour;
        private CameraServices _cameraServices;
        private EnemiesData _enemiesData;
        private CharacterData _characterData;

        #endregion


        #region ClassLifeCycles

        public CameraController()
        {
            _cameraServices = Services.Instance.CameraServices;
            _cameraBehaviour = _cameraServices.CameraMain.GetComponent<CameraBehaviour>();
            _enemiesData = Data.Instance.EnemiesData ;
            _characterData = Data.Instance.Character;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (_characterData.CharacterBehaviour.GameMode == GameModeType.Start)
            {
                _cameraBehaviour.StartCamera(_characterData.CharacterBehaviour.transform);
            }
            // if (_characterData.CharacterBehaviour.GameMode == GameModeType.ArrowFly)
            // {
            //     _cameraBehaviour.FollowToTarget(_characterData.ArrowBehaviour.transform);
            //     //_cameraBehaviour.TestCamera(_characterData.ArrowBehaviour.transform);
            // }
            if (_characterData.CharacterBehaviour.GameMode == GameModeType.Ragdoll)
            {
                //_cameraBehaviour.FollowToTarget(_enemiesData.EnemyBehaviour.Rigidbody[0].transform);
                _cameraBehaviour.TestCamera(_enemiesData.EnemyBehaviour.Rigidbody[0].transform);
                //_cameraBehaviour.FollowToRagdoll(_enemiesData.EnemyBehaviour.Rigidbody[0].transform);
            }

        }

        #endregion
    }
}
