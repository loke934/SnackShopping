using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera _myCamera;
    
    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float zoomAmount = Input.GetAxis("Mouse ScrollWheel") * 5f;
            float newPosition = _myCamera.orthographicSize += zoomAmount;
            _myCamera.orthographicSize = Mathf.Clamp(_myCamera.orthographicSize, 2f, 6f);
        }
    }

    void Start()
    {
        _myCamera = transform.GetComponent<Camera>();
    }
    
    void Update()
    {
        Zoom();
    }
}
