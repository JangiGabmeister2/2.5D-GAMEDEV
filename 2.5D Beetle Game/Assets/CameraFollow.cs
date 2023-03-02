using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //the target which the camera is following and focusing on
    [Tooltip("The target which the camera is following and focusing on.")]
    public Transform target;
    //smoothly follows the target
    [Tooltip("How smooth the camera follows the target.")]
    public float smoothing = 5f;
    //distance between camera and target
    Vector3 _distance;

    private void Start()
    {
        _distance = transform.position - target.position;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 targetCamPos = target.position + _distance;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
