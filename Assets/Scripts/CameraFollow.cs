using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 destination = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}