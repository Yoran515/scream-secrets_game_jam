using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VhsPlayer : MonoBehaviour
{
    public Transform stickPositionObject;
    private Vector3 stickPosition;
    private Quaternion stickRotation; 

    private Pickup pickupObj;

    private GameObject currentSnappedObject; 
    private bool isOccupied; 


    void Start()
    {
        stickPosition = stickPositionObject.position;
        stickRotation = stickPositionObject.rotation; 
        isOccupied = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drag"))
        {
            if (isOccupied)
            {
                return; 
            }

            pickupObj = other.GetComponent<Pickup>();

            
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                otherRigidbody.isKinematic = true;
            }

            other.gameObject.transform.position = stickPosition;
            other.gameObject.transform.rotation = stickRotation;
            
            currentSnappedObject = other.gameObject;
            isOccupied = true;

            Debug.Log(pickupObj.tapeNumber);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Drag"))
        {
            if (isOccupied)
            {
                if(other.gameObject == currentSnappedObject)
                {
                    ClearPosition();
                }
            }
        }
    }
    public void ClearPosition()
    {
        
        isOccupied = false;
        currentSnappedObject = null;
    }
}
