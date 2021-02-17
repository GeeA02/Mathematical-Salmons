using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    
    // Update is called once per frame
    private void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref!", this);
            return;
        }

        transform.position = target.TransformPoint(0f, 1.5f, -2f);

        transform.LookAt(target);
        transform.rotation *= Quaternion.Euler(-25f, 0f, 0f);
    }
}
