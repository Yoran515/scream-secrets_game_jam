using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private List<Cam> cameras = new List<Cam>();

    public VhsPlayer player;

    void Start()
    {
        
    }

    
    void Update()
    {
        
        foreach (Cam cam in cameras)
        {
            if (cam.camera != null) 
            {
                if (cam.number == player.number)
                {
                    cam.camera.enabled = cam.isOn;
                }
                
            }
        }
    }
}

/*[Serializable]
class Cam
{
    public string name;
    public int number;
    public Camera camera;
    public bool isOn;
}
*/