using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
    #region Singleton class: UIManager

    public static PrefabsManager Instance;

    public void InitializeSingleton ()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    #endregion
    
    [SerializeField] private List<GameObject> levelsPrefabs = new List<GameObject>();
    [SerializeField] private List<ColorSetup> colorSetups = new List<ColorSetup>();
    
    public GameObject GetLevelPrefab(int index = default)
    {
        if(index == default || index >= levelsPrefabs.Count)
            return levelsPrefabs[Random.Range(0, levelsPrefabs.Count)];
        
        return levelsPrefabs[index];
    }
    
    public ColorSetup GetColorSetup(int index = default)
    {
        if(index == default || index >= colorSetups.Count)
            return colorSetups[Random.Range(0, colorSetups.Count)];
        
        return colorSetups[index];
    }
}
