using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{

    public Transform Target;
    public float YDistance = 20f;

    private Vector3 _initialEulerAngles;
    Camera cam;
        
    void Start()
    {
        _initialEulerAngles = transform.eulerAngles;
    }

   
    void LateUpdate()
    {
        Vector3 targetPosition = Target.position;
        targetPosition.y = YDistance;

        transform.position = targetPosition;

        Vector3 targetEulerAngles = Target.eulerAngles;
        targetEulerAngles.x = _initialEulerAngles.x;
        targetEulerAngles.z = _initialEulerAngles.z;
        transform.eulerAngles = targetEulerAngles;

    }
}
