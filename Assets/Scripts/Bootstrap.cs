using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private HoleMovement holeMovement;
    
    private void Awake()
    {
        holeMovement.Initialize();
    }
}
