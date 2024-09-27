using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public float targetRotation;
    public Rigidbody2D rb;
    public float force;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, force * Time.fixedDeltaTime));
    }
}
