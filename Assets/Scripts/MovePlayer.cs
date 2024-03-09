using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MovePlayer : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float horizontalInput;
    public float verticalInput;
    public float turnSpeed = 12.0f;
    
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * (movementSpeed * (verticalInput * Time.deltaTime)));
        // transform.Translate(Vector3.right * (horizontalInput * Time.deltaTime));
        transform.Rotate(Vector3.up * (horizontalInput * turnSpeed * Time.deltaTime));
    }
}