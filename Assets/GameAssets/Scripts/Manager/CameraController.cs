using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CameraController : Singleton<CameraController> 
{
    [SerializeField] private Transform cameraTransform;
    private float adding = 0.5f;

    public void SetCameraFocusPoint(Transform focusPoint)
    {
        cameraTransform.position = new Vector3(focusPoint.localScale.x / 2f + adding, -focusPoint.localScale.z / 2f - adding, cameraTransform.position.z);
    }

    public void SetCameraFocusPoint(Vector3 focusPoint)
    {
        cameraTransform.position = new Vector3(focusPoint.x, focusPoint.y, cameraTransform.position.z);
    }
}

