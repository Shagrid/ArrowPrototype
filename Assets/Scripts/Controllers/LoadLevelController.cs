namespace ExampleTemplate
{
    public class LoadLevelController : IInitialization
    {
        public void Initialization()
        {
            Services.Instance.LevelLoadService.LoadLevel(LevelType.TestLevelAlex, EnemyType.Kobayashi, CharacterType.Archer);
        }
    }
}