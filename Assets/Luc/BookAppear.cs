using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAppear : MonoBehaviour
{

    public GameObject Book;
    public CameraClick click;
    public bool BookIsVisible;
    // Start is called before the first frame update
    void Start()
    {
        Book.SetActive(false);
    }

    void Update()
    {
        if (Camera.main != null) { 
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a collider
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the collider's GameObject has the specified tag
                if (hit.collider.CompareTag("File"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (click.animator.GetCurrentAnimatorStateInfo(0).IsName("New State"))
                        {
                            BookApp();
                        }

                    }
                }
            }
        }

       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Book.SetActive(false);
            BookIsVisible= false;
        }

    }

    private void BookApp()
    {
        Book.SetActive(true);
        BookIsVisible = true;
    }
}
