using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceType : MonoBehaviour
{
    public GameObject playerObject;
    public FPS_Controller playerController;
    private void Start() 
    {
        playerController = playerObject.GetComponent<FPS_Controller>();    
    }
    private void OnCollisionEnter(Collision other)
    {   
        if(other.gameObject.tag == "Player")
            playerController.isSlantedSurface = true;
    }
    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Player")
            playerController.isSlantedSurface = false;
    }
}
