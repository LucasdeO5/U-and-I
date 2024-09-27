using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    private Transform player;
    public float smoothingX = 1f;
    public float offsetY = 0f;
    public float backgroundScaling = .66f;

    void Start()
    {
        player = GameObject.Find("Hips").transform;

        transform.position = player.position * backgroundScaling + new Vector3(0, offsetY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = player.position * backgroundScaling + new Vector3(0, offsetY, 0);
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothingX * Time.deltaTime);
    }
}
