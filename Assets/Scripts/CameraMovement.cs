using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{ 
    [Header("CAMERA MOVEMENT")]
    [SerializeField] private Transform player;
    [SerializeField, Range(2f, 10f)] private float cameraMoveSpeed = 4f;

    private float _radius = 3f;
    
    public Transform Target {
        set { player = value; }
    }

    private void Movement()
    {
        Vector3 positionToFollow = player.transform.position;
        positionToFollow.z = transform.position.z;
        
        Vector3 movingDirection = (positionToFollow - transform.position).normalized;
        float distance = Vector3.Distance(positionToFollow, transform.position);
        if (distance > _radius)
        {
            Vector3 newCameraPosition = transform.position + movingDirection * distance * cameraMoveSpeed * Time.deltaTime;
            
            float distanceAfterMoving = Vector3.Distance(newCameraPosition, positionToFollow);
            //overshooting player
            if (distanceAfterMoving > distance)
            {
                newCameraPosition = positionToFollow;
            }
            transform.position = newCameraPosition;
        }
    }
    
    void LateUpdate()
    {
       Movement();
    }

}
