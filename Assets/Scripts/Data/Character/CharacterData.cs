using UnityEngine;
using UnityEngine.Serialization;


namespace ExampleTemplate
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character/CharacterData")]
    public sealed class CharacterData : ScriptableObject
    {
        [SerializeField] private float _speedArrow = 1f;
        [SerializeField] private float _turnSensivity = 2f;
        [SerializeField] private float _forceImpuls = 3000f;

        [HideInInspector] public CharacterBehaviour CharacterBehaviour;
        [HideInInspector] public ArrowBehaviour ArrowBehaviour;

        private ITimeService _timeService;

        public void Initialization(CharacterType characterType, Transform point)
        {
            var characterBehaviour = CustomResources.Load<CharacterBehaviour>
                (AssetsPathCharactersGameObjects.CharacterGameObject[characterType]);
            CharacterBehaviour = Instantiate(characterBehaviour, point.position, point.rotation);
            ArrowBehaviour = CharacterBehaviour.GetComponentInChildren<ArrowBehaviour>();
            _timeService = Services.Instance.TimeService;
        }

        public float GetSpeed()
        {
            return _speedArrow * _timeService.DeltaTime();
        }

        public float GetTurnSensivity()
        {
            return _turnSensivity;
        }

        public float GetForceimpulse()
        {
            return _forceImpuls;
        }
    }
    
    
}