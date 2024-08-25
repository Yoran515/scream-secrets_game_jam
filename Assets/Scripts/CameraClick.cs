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
    public GameObject Camera1;
    public Button camButton;

    public BookAppear book;
    private void Start()
    {
        UI.SetActive(false);
    
    
    }
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
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

    public void Onscreen()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (stateInfo.IsName("New State"))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if(book.BookIsVisible == false)
                {
                    if (hit.collider.CompareTag("Screen"))
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Debug.Log("testt");
                            animator.SetInteger("IsPressingScreen", 1);
                        }
                    }
                }
              
               

            }
         
            UI.SetActive(false);
            Camera1.SetActive(false);
            camButton.enabled = true;
        }
    

        if (stateInfo.IsName("Camera"))
        {
            animator.SetInteger("IsPressingScreen", 2);
        
        }
        if (stateInfo.IsName("lookingat"))
        {
            UI.SetActive(true);
            Camera1.SetActive(true);
        }
        if (stateInfo.IsName("Back"))
        {
            UI.SetActive(false);
            Camera1.SetActive(false);
        }
       
    }

    public void OnClick()
    {
            Camera1.SetActive(false);
            animator.SetInteger("IsPressingScreen", 3);
            UI.SetActive(false);
            camButton.enabled = false;
    }

  
}
