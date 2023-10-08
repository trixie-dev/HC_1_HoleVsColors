using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    #region Singleton class: Magnet

    public static Magnet Instance;

    public void InitializeSingleton ()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] float magnetForce;


    //to store objects inside magnetic field
    List<Rigidbody> affectedRigidbodies = new List<Rigidbody> ();
    Transform magnet;

    public void Initialize ()
    {
        magnet = transform;
        affectedRigidbodies.Clear ();
    }

    void FixedUpdate ()
    {
        if (!Game.IsGameOver && Game.IsMoving) {
            foreach (Rigidbody rb in affectedRigidbodies) {
                rb.AddForce ((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
            }
        }
    }

    //Object enters Magnetic field
    void OnTriggerEnter (Collider other)
    {
        string tag = other.tag;

        if (!Game.IsGameOver && (tag.Equals ("Obstacle") || tag.Equals ("Object"))) {
            AddToMagnetField (other.attachedRigidbody);
        }
    }

    //Object exits Magnetic field
    void OnTriggerExit (Collider other)
    {
        if (!Game.IsGameOver && (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Object"))) {
            RemoveFromMagnetField (other.attachedRigidbody);
        }
    }

    public void AddToMagnetField (Rigidbody rb)
    {
        affectedRigidbodies.Add (rb);
    }

    public void RemoveFromMagnetField (Rigidbody rb)
    {
        affectedRigidbodies.Remove (rb);
    }
}