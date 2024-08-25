using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameObject selectedObject;
    private Rigidbody selectedRigidbody;
    public int tapeNumber = 1;
    public GameObject tray;

    public float spaceAboveMouse = 1f;

    private VhsPlayer vhsPlayer;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Transform cameraTransform;

    private void Start()
    {
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;

        cameraTransform = Camera.main.gameObject.transform;
        vhsPlayer = tray.GetComponent<VhsPlayer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Drag"))
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    selectedRigidbody = selectedObject.GetComponent<Rigidbody>();

                    if (selectedRigidbody != null)
                    {
                        // Temporarily disable physics by making the Rigidbody kinematic
                        selectedRigidbody.isKinematic = true;
                    }

                    Cursor.visible = false;
                }
            }
            else
            {
                Vector3 newPosition = GetMouseWorldPosition();
                selectedObject.transform.position = new Vector3(newPosition.x, newPosition.y + spaceAboveMouse, newPosition.z);

                if (selectedRigidbody != null)
                {
                    // Re-enable physics by making the Rigidbody non-kinematic
                    selectedRigidbody.isKinematic = false;
                }

                selectedObject = null;
                selectedRigidbody = null;
                Cursor.visible = true;
            }
        }

        if (selectedObject != null && selectedObject == this.gameObject)
        {
            Vector3 newPosition = GetMouseWorldPosition();
            selectedObject.transform.position = new Vector3(newPosition.x, newPosition.y + spaceAboveMouse, newPosition.z);

            // Get the direction vector from the object to the camera
            Vector3 directionToCamera = cameraTransform.position - selectedObject.transform.position;
            directionToCamera.y = 0; // Keep only the horizontal direction

            // Create a rotation that only affects the Y axis
            Quaternion rotationToCamera = Quaternion.LookRotation(directionToCamera);

            // Apply the rotation to the selected object
            selectedObject.transform.rotation = rotationToCamera;

            if (Input.GetMouseButtonDown(1))
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x,
                    selectedObject.transform.rotation.eulerAngles.y + 90f,
                    selectedObject.transform.rotation.eulerAngles.z));
            }
        }
    }


private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FloorTrigger")
        {
            this.transform.position = initialPosition;
            this.transform.rotation = initialRotation;
        }

/*        if (other.tag == "VHS_Player")
        {
            Debug.Log("VHS_Player in");
            vhsPlayer.SetVhs(this.gameObject);
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "VHS_Player")
        {
            Debug.Log("VHS_Player Out");
        }
    }
    private RaycastHit CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
    }

    private Vector3 GetMouseWorldPosition()
    {
        RaycastHit hit = CastRay();

        if (hit.collider != null)
        {

          
            if (!hit.collider.CompareTag("Drag"))
            {
                return hit.point; // Return the point where the ray hits an object in the world
            }
         }
                

        return Vector3.zero; // Return a default value if no hit
    }
}
