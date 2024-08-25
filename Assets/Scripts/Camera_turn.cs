using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_turn : MonoBehaviour
{

    public Camera _camera;
    public Transform cameraRotation;
    public float rotationSpeed = 1.0f;
    public CameraClick Cameralook;
    private Quaternion targetRotation; 
    private bool isTurning = false;

    // Start is called before the first frame update
    void Start()
    { 
        targetRotation = cameraRotation.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Cameralook.animator.GetCurrentAnimatorStateInfo(0).IsName("New State"))
       {
            if (Input.GetKeyDown(KeyCode.A) && !isTurning && cameraRotation.rotation.y > -90f)
            {
                targetRotation = Quaternion.Euler(cameraRotation.eulerAngles + new Vector3(0, -90, 0));

                isTurning = true;
                _camera.fieldOfView = 52.2f;
            }
            if (Input.GetKeyDown(KeyCode.D) && !isTurning && cameraRotation.rotation.y < 90f)
            {
                targetRotation = Quaternion.Euler(cameraRotation.eulerAngles + new Vector3(0, 90, 0));
                isTurning = true;
                _camera.fieldOfView = 45.2f;
            }

            if (isTurning)
            {
                cameraRotation.rotation = Quaternion.Lerp(cameraRotation.rotation, targetRotation, Time.deltaTime * rotationSpeed);


                if (Quaternion.Angle(cameraRotation.rotation, targetRotation) < 0.1f)
                {
                    cameraRotation.rotation = targetRotation;
                    isTurning = false;
                }
            }
       }
        
    }
}
