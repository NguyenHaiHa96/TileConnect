using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CameraController : Singleton<CameraController> 
{
    [SerializeField] private Transform cameraTransform;

    private Vector3 startPosition;
    private float adding = 0.5f;

    public void SetStartPosition()
    {
        startPosition = cameraTransform.position;
    }

    public void SetCameraFocusPoint(Transform focusPoint)
    {
        cameraTransform.position = new Vector3(focusPoint.localScale.x / 2f + adding, -focusPoint.localScale.z / 2f - adding, cameraTransform.position.z);
    }

    public void SetCameraFocusPoint(Vector3 position)
    {
        cameraTransform.position = new Vector3(position.x, position.y, cameraTransform.position.z);
    }

    public void Shake()
    {

    }
}

