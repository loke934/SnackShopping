using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{ 
    [Header("CAMERA MOVEMENT")]
    [SerializeField] private Transform target;
    [SerializeField, Range(1f, 10f)] private float moveSpeed = 8f;
    
    private Camera _myCamera;
    public Transform Target {
        set { target = value; }
    }

    private void Movement()
    {
        Vector3 positionToFollow = target.transform.position;
        positionToFollow.z = transform.position.z;
        
        Vector3 movingDirection = (positionToFollow - transform.position).normalized;
        float distance = Vector3.Distance(positionToFollow, transform.position);
        if (distance > 1f)
        {
            Vector3 newCameraPosition = transform.position + movingDirection * distance * moveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, positionToFollow);

            if (distanceAfterMoving > distance)
            {
                newCameraPosition = positionToFollow;
            }

            transform.position = newCameraPosition;
        }

        /*transform.position = new Vector3(target.transform.position.x, target.transform.position.y,
            target.transform.position.z + -10f);*/
    }

    private void Zoom1()
    {
        float zoomMax = 1f;
        float zoomMin = 5f;
        float zoomAmount =0f;
        float zoomSpeed = 20f;
        
        if (Input.GetAxis("Mouse ScrollWheel")> 0)
        {
            zoomAmount = zoomMax - _myCamera.orthographicSize;
            //_myCamera.orthographicSize += cameraZoomDifference  * zoomSpeed * Time.deltaTime;
            
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            zoomAmount = zoomMin - _myCamera.orthographicSize;
    
        }
        _myCamera.orthographicSize += zoomAmount  * zoomSpeed * Time.deltaTime;

    }
    private void Zoom()
    {
        float cameraZoom = 1f;
        float cameraZoomDifference = cameraZoom - _myCamera.orthographicSize;
        float zoomSpeed = 30f;
        
        _myCamera.orthographicSize += cameraZoomDifference  * zoomSpeed * Time.deltaTime;

        if (cameraZoomDifference > 0)
        {
            if (_myCamera.orthographicSize > cameraZoom)
            {
                _myCamera.orthographicSize = cameraZoom;
            }
        }
        else
        {
            if (_myCamera.orthographicSize < cameraZoom)
            {
                _myCamera.orthographicSize = cameraZoom;
            }
        }
    }
    
    private void Start()
    {
        _myCamera = transform.GetComponent<Camera>();
    }
    
    void LateUpdate()
    {
       Movement();
    }

}
