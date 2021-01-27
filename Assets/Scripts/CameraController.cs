using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition = new Vector3(-2.85f, 0.75f, -5.0f);


    [SerializeField]
    private Space offsetPositionSpace = Space.World;

    [SerializeField]
    private bool lookAt = true;


    // Update is called once per frame
    private void Update()
    {
        if (target == null)
        {
            //Debug.LogWarning("Missing target ref!", this);
            return;
        }
        // compute position 
        if (offsetPositionSpace == Space.Self)
            transform.position = target.TransformPoint(offsetPosition);
        else
            transform.position = target.position + offsetPosition;
        // compute rotation
        if (lookAt)
            transform.LookAt(target);
    }
}
