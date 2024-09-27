using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleLevel : MonoBehaviour
{
    public KeyCode switchButton;
    [SerializeField] bool canSwitch;
    [SerializeField] private List<Collider2D> bodyParts = new List<Collider2D>();

    [SerializeField] private GameObject frontLevel;
    [SerializeField] private GameObject backLevel;
    [SerializeField] private GameObject otherCollision;
    [SerializeField] private GameObject thisCollision;

    [SerializeField] private GameObject frontBackground;
    [SerializeField] private GameObject backBackground;


    void Start()
    {
        canSwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchButton) && canSwitch)
        {
            frontLevel.SetActive(false);
            backLevel.SetActive(true);

            frontBackground.SetActive(true);
            backBackground.SetActive(false);

            otherCollision.SetActive(true);
            thisCollision.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            int index = bodyParts.IndexOf(col);
            if (index < 0)
            {
                bodyParts.Add(col);
            }
            canSwitch = false;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            bodyParts.Remove(col);

            if (bodyParts.Count == 0)
            {
                canSwitch = true;
            }
        }
    }
}
