using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UndergroundCollisions: MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(Game.IsGameOver) return;
        
        if (other.gameObject.CompareTag("Object"))
        {
            GameManager.Instance.objectsInScene--;
            UIManager.Instance.UpdateLevelProgress ();

            //Make sure to remove this object from Magnetic field
            Magnet.Instance.RemoveFromMagnetField (other.attachedRigidbody);

            Destroy (other.gameObject);

            //check if win
            if (GameManager.Instance.objectsInScene == 0) {
                //no more objects to collect (WIN)
                UIManager.Instance.ShowLevelCompletedUI ();
                GameManager.Instance.PlayWinFx ();

                //Load Next level after 2 seconds
                Invoke ("NextLevel", 2f);
            }
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            Game.IsGameOver = true;
            Destroy (other.gameObject);

            //Shake the camera for 1 second
            Camera.main.transform
                .DOShakePosition (1f, .2f, 20, 90f)
                .OnComplete (() => {
                    //restart level after shaking complet
                    GameManager.Instance.RestartLevel ();
                });
        }
    }
    
    void NextLevel ()
    {
        GameManager.Instance.LoadNextLevel ();
    }
}
