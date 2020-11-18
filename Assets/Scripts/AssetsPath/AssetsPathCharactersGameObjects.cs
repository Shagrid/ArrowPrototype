using System.Collections.Generic;

namespace ExampleTemplate
{
    public sealed class AssetsPathCharactersGameObjects
    {
        #region Fields

        public static readonly Dictionary<CharacterType, string> CharacterGameObject = new Dictionary<CharacterType, string>
        {
            {
                CharacterType.Test, "Prefabs/Character/Prefabs_Character_SphereCharacter"
            },
            {
                CharacterType.Archer, "Prefabs/Character/SM_Chr_Rider_01"
            },
            
        };

        #endregion
    }
}