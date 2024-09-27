using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private bool hold;
    public KeyCode mouseButton;
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(mouseButton))
        {
            hold = true;
        }
        else
        {
            hold = false;
            Destroy(GetComponent<FixedJoint2D>());
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (hold)
        {
            if (col.gameObject.CompareTag("Orb"))
            {
                SetPlayerColor(col.gameObject.GetComponent<SpriteRenderer>().color);
            }
            else
            {
                Rigidbody2D rb = col.transform.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                    fj.connectedBody = rb;
                }
                else
                {
                    FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                }
            }
        }
    }
    
    void SetPlayerColor(Color color)
    {
        SpriteRenderer[] bodyParts = player.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer bodyPart in bodyParts)
        {
            bodyPart.color = color;
        }

        UnityEngine.U2D.SpriteShapeRenderer[] customBodyParts = player.GetComponentsInChildren<UnityEngine.U2D.SpriteShapeRenderer>();
        foreach (UnityEngine.U2D.SpriteShapeRenderer bodyPart in customBodyParts)
        {
            bodyPart.color = color;
        }
    }
}
