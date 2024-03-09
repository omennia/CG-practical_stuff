using UnityEngine;

public class CameraField : MonoBehaviour
{
    public Camera[] cameras;
    public int currentCameraIndex;
    private float _xRotation;
    float _mouseSensitivity;


    void MoveMouseCamera(int index)
    {
        var mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        var curTransform = cameras[index].transform;
        curTransform.localEulerAngles = new Vector3(_xRotation, curTransform.localEulerAngles.y + mouseX, 0f);
        
        
        // We would use this switch statement if we wanted only to move the player cameras
        /*
        switch (index)
        {
            case 0:
            {
                curTransform.localEulerAngles = new Vector3(_xRotation, curTransform.localEulerAngles.y + mouseX, 0f);
                break;
            }
            case 1:
            {
                curTransform.localEulerAngles = new Vector3(_xRotation, curTransform.localEulerAngles.y + mouseX, 0f);
                break;
            }
        }
    */
    }
    
    void Start()
    {   
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen.
        Cursor.visible = false; // Makes the cursor invisible.
        _mouseSensitivity = 1000f;
        currentCameraIndex = 0;
        ActivateCamera(currentCameraIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCameraIndex++;
            if (currentCameraIndex >= cameras.Length)
            {
                currentCameraIndex = 0;
            }
            ActivateCamera(currentCameraIndex);
        }
        if (cameras.Length > 0 && cameras[currentCameraIndex].gameObject.activeInHierarchy)
        {
            MoveMouseCamera(currentCameraIndex);
        }
    }

    void ActivateCamera(int index)
    {
        for (var i = 0; i < cameras.Length; ++i)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
        MoveMouseCamera(index);
    }
}