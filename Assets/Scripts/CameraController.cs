using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offsetPosition = new Vector3(0.0f, 4.0f, -5.0f);

    private void Update()
    {
        if (target is null)
        {
            Debug.LogWarning("Missing target ref!", this);
            return;
        }
        
        transform.position = target.position + offsetPosition;
    }
}
