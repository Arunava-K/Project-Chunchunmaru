using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_Manupulationn : MonoBehaviour
{
    public GameObject playerObject;
    public FPS_Controller player_Controller;

    public float regionGravity;
    public float normalGravity;
    private void Start() 
    {
        player_Controller = playerObject.GetComponent<FPS_Controller>();    
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            player_Controller.gravity = regionGravity;
            Debug.Log("Player entered gravity mod region");
        }    
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            player_Controller.gravity = normalGravity;
            Debug.Log("Player left gravity mod region");
        }
    }
}
