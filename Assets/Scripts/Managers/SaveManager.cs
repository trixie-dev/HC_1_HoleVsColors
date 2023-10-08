
using UnityEngine;

public static class SaveManager
    
{
    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
    }
    
    public static void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 0) + 1);
    }
    
    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt("Level", 0);
    }

}
