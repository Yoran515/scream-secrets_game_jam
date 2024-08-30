using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator animator;
    private int lever_animation = 0;
    public GameObject Lights;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Lever"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (lever_animation == 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("StayUp"))
                    {
                        animator.SetInteger("DownOrUp", 1);
                        lever_animation = 1;
                    }
                    else if (lever_animation == 1 && animator.GetCurrentAnimatorStateInfo(0).IsName("StayDown"))
                    {
                        animator.SetInteger("DownOrUp", 0);
                        lever_animation = 0;
                    }
                }
              
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("StayDown"))
        {
            Lights.SetActive(false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("StayUp"))
        {
            Lights.SetActive(true);
        }
    }
}
