using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerController : MonoBehaviour
{
    public float rotationSpeed = 5f;
   
    private float colorChangeStartTime;

    private void Start()
    {
        colorChangeStartTime = Time.time;
    }

    private void Update()
    {
        // Rotate the object on the Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}