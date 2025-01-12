using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private float rotationSpeed = 40f; 

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); //rotate
    }

    public void ChangeRotation(float tmp)
    {
        rotationSpeed = tmp;
    }

    public void respawn()
    {
        rotationSpeed = 40f;
    }
}
