using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PrefabsManager prefabsManager;
    [SerializeField] private HoleMovement holeMovement;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Magnet magnet;
    
    
    private void Awake()
    {
        gameManager.InitializeSingleton();
        uiManager.InitializeSingleton();
        prefabsManager.InitializeSingleton();
        magnet.InitializeSingleton();
        
        gameManager.Initialize();
        holeMovement.Initialize();
        magnet.Initialize();
        uiManager.Initialize();
    }
}
