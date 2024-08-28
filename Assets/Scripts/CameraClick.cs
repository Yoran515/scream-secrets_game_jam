using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraClick : MonoBehaviour
{
    public GameObject UI;
    public Animator animator;
    public CamerSwitch cameraswitch;
    [SerializeField]
    private List<Cam> rooms = new List<Cam>();
    public Button camButton;
    public int CamAnimation;
    public BookAppear book;
    public VhsPlayer player;

    public bool lookingatScreen;

    private void Start()
    {
        UI.SetActive(false);
        SetCameraState(0);
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
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
            SetCameraState(0);
            camButton.enabled = true;
        }

        if (stateInfo.IsName("Camera"))
        {
            animator.SetInteger("IsPressingScreen", 2);
            CamAnimation = 2;
        }

        if (stateInfo.IsName("lookingat"))
        {
            UI.SetActive(true);
            SetCameraState(player.number);
        }

        if (stateInfo.IsName("Back"))
        {
            UI.SetActive(false);
            SetCameraState(0);
            lookingatScreen = false;
        }
    }

    public void OnClick()
    {
        SetCameraState(0);
        animator.SetInteger("IsPressingScreen", 3);
        CamAnimation = 3;
        UI.SetActive(false);
        camButton.enabled = false;
    }

    private void SetCameraState(int activeRoomNumber)
    {
        foreach (Cam room in rooms)
        {
            if (room.room != null)
            {
                if (room.number == 0)
                {
                    room.room.SetActive(true);
                    room.isOn = true;
                    animator.applyRootMotion = true;
                }
                else if (room.number == activeRoomNumber)
                {
                    room.room.SetActive(true);
                    room.isOn = true;
                }
                else
                {
                    room.room.SetActive(false);
                    room.isOn = false;
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
    public GameObject room;
    public bool isOn;
}
