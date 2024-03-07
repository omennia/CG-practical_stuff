using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float turnSpeed = 4.0f;

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        var vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(horizontal, 0, vertical);

        if (!Input.GetMouseButton(0)) return; // Check if the left mouse button is held down (uncomment to always move the camera)
        var yaw = turnSpeed * Input.GetAxis("Mouse X");
        var pitch = turnSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(-pitch, yaw, 0);
    }
}