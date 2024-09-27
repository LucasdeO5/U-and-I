using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    public KeyCode mouseButton;

    [SerializeField] private Balance upperArm;
    [SerializeField] private Balance lowerArm;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 difference = playerpos - transform.position;
        float rotationZ = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;

        if (Input.GetKey(mouseButton))
        {
            if (upperArm != null)
                upperArm.targetRotation = rotationZ;
            if (lowerArm != null)
                lowerArm.targetRotation = rotationZ;
        }
        else
        {
            if (upperArm != null)
                upperArm.targetRotation = 0f;
            if (lowerArm != null)
                lowerArm.targetRotation = 0f;
        }
    }
}
