using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float rotateForce = 70.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime, Space.Self);
        transform.Rotate(new Vector3(0, rotateForce, 0) * Time.deltaTime, Space.World);
    }
}