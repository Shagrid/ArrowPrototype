namespace ExampleTemplate
{
    public class LoadLevelController : IInitialization
    {
        public void Initialization()
        {
            Services.Instance.LevelLoadService.LoadLevel(LevelType.TestLevel, EnemyType.Kobayashi, CharacterType.Archer);
        }
    }
}