using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CacheComponent : MonoBehaviour
{
    public Transform Transform { get => transform; }
    public Quaternion Rotation { get => transform.rotation; set => transform.rotation = value; }
    public Vector3 WorldPosition { get => transform.position; set => transform.position = value; }
    public Vector3 LocalPosition { get => transform.localPosition; set => transform.localPosition = value; }
    public Vector3 LocalScale { get => transform.localScale; set => transform.localScale = value; }
    public Vector3 EulerLocalRotation { get => transform.localRotation.eulerAngles; }
    public Vector3 EulerAngles { get => transform.eulerAngles; set => transform.eulerAngles = value; }
    public float DeltaTime { get => Time.deltaTime; }
    public float FixedDeltaTime { get => Time.fixedDeltaTime; }

    public virtual void OnInit()
    {

    }
}
