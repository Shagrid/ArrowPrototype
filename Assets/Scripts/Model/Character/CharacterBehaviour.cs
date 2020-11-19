using UnityEngine;


namespace ExampleTemplate
{
    public sealed class CharacterBehaviour : MonoBehaviour
    {
        
        private CharacterData _characterData;
        public GameModeType GameMode { get; private set; }
     

        private void Awake()
        {
            _characterData = Data.Instance.Character;
            //GameMode = GameModeType.None;
        }
        

        public void Move(Vector3 moveDirection)
         {
             //transform.Translate(transform.right * moveDirection.x * _characterData.GetSpeed());
             //transform.Translate(transform.up * moveDirection.y * _characterData.GetSpeed());
         }

         // public void Touch()
         // {
         //     if (GameMode == GameModeType.Start)
         //     {
         //         GameMode = GameModeType.ArrowFly;
         //         _arrow.SetParent(null);
         //     }
         // }

        public void SetGameMode(GameModeType gameModeType) => GameMode = gameModeType;
    }
}
