using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 0.1f;

    private Rigidbody2D rb;
    private float startingY;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        startingY = transform.position.y;
    }

    void FixedUpdate()
    {
        //rb.AddForce(-Vector3.up * Physics.gravity.y, ForceMode2D.Force);
        //Vector3 desiredAccel = new Vector3(0, -frequency*frequency * amplitude * Mathf.Sin(frequency * Time.time), 0);
        //rb.AddForce(desiredAccel, ForceMode2D.Force);

        rb.velocity = new Vector2(rb.velocity.x, frequency * amplitude * Mathf.Cos(frequency * Time.time));
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, startingY - amplitude, startingY + amplitude), transform.position.z);
    }
}
