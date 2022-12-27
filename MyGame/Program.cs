
internal class Program
{
    private static void Main(string[] args)
    {
        using var game = new MyGame.Game1();
        game.Run();
    }
}