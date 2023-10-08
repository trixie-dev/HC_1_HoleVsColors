using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    [Header ("Hole mesh")]
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshCollider meshCollider;
    
    [Header ("Hole vertices radius")]
    [SerializeField] private Vector2 moveLimit;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private Transform holeCenter;
    //rotating circle arround hole (animation)
    [SerializeField] Transform rotatingCircle;

    [Space]
    [SerializeField] private float moveSpeed;
    
    private Mesh _mesh;
    private List<int> _holeVertices;
    private List<Vector3> _offset;
    private int _holeVerticesCount = 0;

    private float x, y;
    private Vector3 touch, targetPos;

    public void Initialize()
    {
        Game.StartGame();
        _holeVertices = new List<int>();
        _offset = new List<Vector3>();
        
        _mesh = meshFilter.mesh;
        
        RotateCircleAnim();
        FindHoleVertices();
    }

    void Update()
    {
        #if UNITY_EDITOR 
            //isMoving=true whenever mouse is clicked 
            //isMoving=falseever mouse is released
            Game.IsMoving = Input.GetMouseButton (0);

            if (!Game.IsGameOver && Game.IsMoving) {
                //Move hole center
                MoveHole ();
                //Update hole vertices
                UpdateHoleVerticesPosition ();
            }


        //Touch
        #else
		    //TouchPhase.Moved to prevent hole from jumping at first touch
		    Game.isMoving = Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved;

		    if (!Game.isGameover && Game.isMoving) {
		    //Move hole center
		    MoveHole ();
		    //Update hole vertices
		    UpdateHoleVerticesPosition ();
		    }
        #endif
    }
    
    void RotateCircleAnim ()
    {
        //rotate circle arround Y axis by -90°
        //duration: 0.2 seconds
        //start: Vector3 (90f, 0f, 0f)
        //loop: -1 (infinite)
        rotatingCircle
            .DORotate (new Vector3 (90f, 0f, -90f), .2f)
            .SetEase (Ease.Linear)
            .From (new Vector3 (90f, 0f, 0f))
            .SetLoops (-1, LoopType.Incremental);
    }


    private void UpdateHoleVerticesPosition()
    {
        Vector3[] vertices = _mesh.vertices;
        for (int i = 0; i < _holeVerticesCount; i++)
        {
            vertices[_holeVertices[i]] = holeCenter.position + _offset[i];
        }
        
        // update mesh
        _mesh.vertices = vertices;
        meshFilter.mesh = _mesh;
        meshCollider.sharedMesh = _mesh;
    }

    private void MoveHole()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        var position = holeCenter.position;
        touch = Vector3.Lerp(
            position, 
            position + new Vector3(x, 0, y), 
            moveSpeed * Time.deltaTime);
        
        targetPos = new Vector3(
            Mathf.Clamp(touch.x, -moveLimit.x, moveLimit.x),
            touch.y,
            Mathf.Clamp(touch.z, -moveLimit.y, moveLimit.y));
        
        holeCenter.position = targetPos;
    }

    private void FindHoleVertices()
    {
        for (int i = 0; i < _mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(_mesh.vertices[i], holeCenter.position);
            if (distance <= radius)
            {
                _holeVertices.Add(i);
                _offset.Add(_mesh.vertices[i] - holeCenter.position);
                _holeVerticesCount++;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(holeCenter.position, radius);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(holeCenter.position, new Vector3(moveLimit.x * 2, 0, moveLimit.y * 2));
    }
}
