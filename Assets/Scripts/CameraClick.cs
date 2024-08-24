using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClick : MonoBehaviour
{
  
    public Animator animator;

    private void Start()
    {
     
    }
    void Update()
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

    public void Onscreen()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("testt");
            animator.SetInteger("IsPressingScreen", 1);
        }
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Camera"))
        {
            animator.SetInteger("IsPressingScreen", 2);
        }
    }
}
