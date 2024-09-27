using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Seesaw : MonoBehaviour
{
    [SerializeField] private TilemapCollider2D levelCol;
    
    void Awake()
    {
        Physics2D.IgnoreCollision(levelCol, GetComponent<CapsuleCollider2D>());
    }

    void OnEnable()
    {
        Physics2D.IgnoreCollision(levelCol, GetComponent<CapsuleCollider2D>());
    }
}
