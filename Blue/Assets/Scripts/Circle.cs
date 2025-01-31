using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private float rotationSpeed = 20f; 

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); //rotate
    }

    public void ChangeRotationBeastTamer()
    {
        rotationSpeed *= -1;
    }

    public void respawn()
    {
        rotationSpeed = 20f;
    }
}
