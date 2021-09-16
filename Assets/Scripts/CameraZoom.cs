using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("ZOOM")]
    [SerializeField, Range(1f,6f)] private float zoomAmount = 5f;

    private float _maxZoom = 2f;
    private float _minZoom = 6f;
    private Camera _myCamera;
    
    private void Zoom() {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) {
            zoomAmount = Input.GetAxis("Mouse ScrollWheel") * 5f;
            _myCamera.orthographicSize += zoomAmount;
            _myCamera.orthographicSize = Mathf.Clamp(_myCamera.orthographicSize, _maxZoom, _minZoom);
        }
    }

    void Start() {
        _myCamera = transform.GetComponent<Camera>();
    }
    
    void Update() {
        Zoom();
    }
}
