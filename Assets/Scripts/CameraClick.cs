using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CameraClick : MonoBehaviour
{
    public GameObject UI;
    public Animator animator;
    public CamerSwitch cameraswitch;
    [SerializeField]
    private List<Cam> cameras = new List<Cam>();
    public Button camButton;
    public int CamAnimation;
    public BookAppear book;
    public VhsPlayer player;

    public bool lookingatScreen;

    private void Start()
    {
        UI.SetActive(false);
        SetCameraState(0); // Set the initial camera to the one with number 0
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (Camera.main != null)
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a collider
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the collider's GameObject has the specified tag
                if (hit.collider.CompareTag("Screen"))
                {
                    Onscreen();
                }
            }
        }
    }

    public void Onscreen()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (stateInfo.IsName("New State"))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (book.BookIsVisible == false && hit.collider.CompareTag("Screen"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        lookingatScreen = true;
                        Debug.Log("testt");
                        animator.SetInteger("IsPressingScreen", 1);
                        CamAnimation = 1;
                    }
                }
            }

            UI.SetActive(false);
            SetCameraState(0); // Ensure only the camera with number 0 is active
            camButton.enabled = true;
        }

        if (stateInfo.IsName("Camera"))
        {
            animator.SetInteger("IsPressingScreen", 2);
            CamAnimation = 2;
           // animator.applyRootMotion = false;
        }

        if (stateInfo.IsName("lookingat"))
        {
            UI.SetActive(true);
            SetCameraState(player.number); // Enable the camera based on the player's number
            //animator.applyRootMotion = false;
        }

        if (stateInfo.IsName("Back"))
        {
            UI.SetActive(false);
            SetCameraState(0); // Switch back to camera 0
            lookingatScreen = false;
            //animator.applyRootMotion = false;
        }

/*        if (stateInfo.IsName("New State"))
        {
            animator.applyRootMotion = true;
        }*/
    }

    public void OnClick()
    {
        SetCameraState(0); // Switch to camera 0 when clicking
        animator.SetInteger("IsPressingScreen", 3);
        CamAnimation = 3;
        UI.SetActive(false);
        camButton.enabled = false;
    }

    private void SetCameraState(int activeCameraNumber)
    {
        foreach (Cam cam in cameras)
        {
            if (cam.camera != null)
            {
                if (cam.number == 0)
                {
                    // Always keep camera 0 active
                    cam.camera.enabled = true;
                    cam.camera.gameObject.SetActive(true);
                    cam.isOn = true;
                    animator.applyRootMotion = true;
                }
                else if (cam.number == activeCameraNumber)
                {
                    cam.camera.enabled = true;
                    cam.camera.gameObject.SetActive(true);
                    cam.isOn = true; // Enable the active camera
                }
                else
                {
                    cam.camera.enabled = false;
                    cam.camera.gameObject.SetActive(false);
                    cam.isOn = false; // Disable all other cameras
                }
            }
        }
    }

}

[Serializable]
class Cam
{
    public string name;
    public int number;
    public Camera camera;
    public bool isOn;
}
