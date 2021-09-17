using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrid : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField, Range(10,30)]private int gridY = 15;
    [SerializeField, Range(10,30)]private int gridX = 15;
    //Problem that with higher density value I must change the range of spawning to match and also
    //the bigger range in grid size x and y difficult
    [SerializeField,Range(0f,1f)] float density = 0.15f;
    [SerializeField] private GameObject prefabTile;
    
    public GameObject[,] gridArray;
    
    private void MakeGrid()
    {
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector3 spawnPosition = new Vector3(x,y);
                Instantiate(prefabTile, spawnPosition, Quaternion.identity).transform.SetParent(transform);
                gridArray[x,y] = prefabTile;
            }
        }
    }
    private void SpawnIrregularSize()
    {
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                float num = Mathf.PerlinNoise(x * density, y * density);
               
                if (num > 0.3 && num < 0.65)
                {
                    Vector3 spawnPosition = new Vector3(x ,y);
                    Instantiate(prefabTile, spawnPosition, Quaternion.identity).transform.SetParent(transform);
                    gridArray[x,y] = prefabTile;
                }
            }
        }
    }

    private void Compare()
    {
        Vector2Int position = new Vector2Int(0, 1);
        Vector2Int direction = Vector2Int.right;
        Vector2Int newPosition = position + direction;

        if (gridArray[newPosition.x,newPosition.y] != null)
        {
            Debug.Log(gridArray[newPosition.x,newPosition.y]);
        }
    }

    private void Awake()
    {
        gridArray = new GameObject[gridX, gridY];
    }

    void Start()
    {
        SpawnIrregularSize();
    }

  
}
