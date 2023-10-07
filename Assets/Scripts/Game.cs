public class Game
{
    public static bool IsGameOver = false;
    public static bool IsGameStarted = false;
    public static bool IsGamePaused = false;
    public static bool IsMoving = false;
    
    public static void StartGame()
    {
        IsGameStarted = true;
        IsGameOver = false;
        IsMoving = false;
    }
}