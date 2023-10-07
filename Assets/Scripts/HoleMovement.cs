using System;
using System.Collections;
using System.Collections.Generic;
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
        
        FindHoleVertices();
    }

    void Update()
    {
        Game.IsMoving = Input.GetMouseButton(0);

        if (!Game.IsGameOver && Game.IsMoving)
        {
            MoveHole();
            UpdateHoleVerticesPosition();
        }
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
