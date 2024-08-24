using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameObject selectedObject;
    private Rigidbody selectedRigidbody;
    public int tapeNumber;
    public GameObject tray;

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
                selectedObject.transform.position = new Vector3(newPosition.x, newPosition.y + 1f, newPosition.z);

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

        if (selectedObject != null)
        {
            Vector3 newPosition = GetMouseWorldPosition();
            selectedObject.transform.position = new Vector3(newPosition.x, newPosition.y + 1f, newPosition.z);

            if (Input.GetMouseButtonDown(1))
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x,
                    selectedObject.transform.rotation.eulerAngles.y + 90f,
                    selectedObject.transform.rotation.eulerAngles.z));
            }

            float distanceToTray = Vector3.Distance(selectedObject.transform.position, tray.transform.position);
            if (distanceToTray <= 20f)
            {
                // Object is within the 20-unit radius of the tray
                Debug.Log("Object is within 20 units of the tray.");
            }
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
