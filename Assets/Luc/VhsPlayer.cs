using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VhsPlayer : MonoBehaviour
{
    public Transform stickPositionObject;
    private Vector3 stickPosition;

    // Start is called before the first frame update
    void Start()
    {
        stickPosition = stickPositionObject.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drag"))
        {
            // Set the object's Rigidbody to kinematic
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                otherRigidbody.isKinematic = true;
            }

            // Move the object to the stick position
            other.gameObject.transform.position = stickPosition;
            Debug.Log("VHS_Player Collided, snapped object to stick position, and set Rigidbody to kinematic");
        }
    }
}
