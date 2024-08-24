using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerSwitch : MonoBehaviour
{
    public Camera[] cameras; // Array of cameras in the scene
    private int currentCameraIndex = 0; // Index to keep track of the current camera
    public CameraClick cameraInt;

    void Start()
    {
        // Ensure only the first camera is enabled initially
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    public void SwitchCamera(int cameraIndex)
    {
        if (cameraIndex >= 0 && cameraIndex < cameras.Length)
        {
            cameras[currentCameraIndex].gameObject.SetActive(false); // Disable current camera
            currentCameraIndex = cameraIndex;
            cameras[currentCameraIndex].gameObject.SetActive(true); // Enable selected camera
        }

    }
}
