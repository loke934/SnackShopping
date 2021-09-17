using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private SpawnGrid spawnGrid;
    Vector2Int direction;
    private Vector2Int newPosition;
    
    private void SetPosition()
    {
        Vector2Int currentPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        
        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2Int.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2Int.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2Int.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2Int.left;
        }
        newPosition = currentPosition + direction;
    }

    private IEnumerator AutoMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (InBounds(newPosition))
            {
                if (spawnGrid.gridArray[newPosition.x,newPosition.y] != null)
                {
                    transform.position = new Vector2(newPosition.x, newPosition.y);
                }
            }
            else
            {
                transform.position = transform.position;
            }
        }
    }

    private bool InBounds(Vector2Int newPosition)
    {
        if (newPosition.x >= 0 && newPosition.x < spawnGrid.gridArray.GetLength(0) && newPosition.y >= 0 && newPosition.y < spawnGrid.gridArray.GetLength(1))
        {
            return true;
        }
        return false;
    }
    private void Start()
    {
        spawnGrid = FindObjectOfType<SpawnGrid>().GetComponent<SpawnGrid>();
        transform.position = new Vector2((int)transform.position.x, (int)transform.position.y);
        StartCoroutine(AutoMovement());
    }
    
    void Update()
    {
        SetPosition();
     
    }
    
    
    
    
    
    
    
    
    
    
    
    
    private void MoveOneTileAtATime()
    {
        Vector2Int currentPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2Int.up;
            newPosition = currentPosition + direction;
            if (InBounds(newPosition))
            {
                if (spawnGrid.gridArray[newPosition.x,newPosition.y] != null)
                {
                    transform.position = new Vector2(newPosition.x, newPosition.y);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2Int.down;
            newPosition = currentPosition + direction;
            if (InBounds(newPosition))
            {
                if (spawnGrid.gridArray[newPosition.x,newPosition.y] != null)
                {
                    transform.position = new Vector2(newPosition.x, newPosition.y);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2Int.right;
            newPosition = currentPosition + direction;
            if (InBounds(newPosition))
            {
                if (spawnGrid.gridArray[newPosition.x,newPosition.y] != null)
                {
                    transform.position = new Vector2(newPosition.x, newPosition.y);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2Int.left;
            newPosition = currentPosition + direction;
            if (InBounds(newPosition))
            {
                if (spawnGrid.gridArray[newPosition.x,newPosition.y] != null)
                {
                    transform.position = new Vector2(newPosition.x, newPosition.y);
                }
            }
        }
 
    }
}
