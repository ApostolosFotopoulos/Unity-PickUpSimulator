using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMovement : MonoBehaviour {

    [Header("Environment Variables")]
    [SerializeField] private  float zBorderMin = 0.8f;
    [SerializeField] private  float zBorderMax = 2.2f;
    [SerializeField] private  float xBorderMin = -1.2f;
    [SerializeField] private  float xBorderMax = 1.2f;
    [SerializeField] private  float height = 1.7f;
    public CharacterController controller;
	
    public Vector3 velocity;
    public float speed = 1f;

    void Update() {
        if (checkIfInBorders()) {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x , height, transform.position.z);
        }
        else {
            correctPos();
        }
        
    }

    bool checkIfInBorders () {
        if (transform.position.z>=zBorderMin && transform.position.z<=zBorderMax && transform.position.x>=xBorderMin && transform.position.x<=xBorderMax) {
            return true;
        }
        return false;
    }

    void correctPos() {
        if (transform.position.z<zBorderMin) {
            transform.position = new Vector3(transform.position.x , height, zBorderMin);
        }
        if (transform.position.z>zBorderMax) {
            transform.position = new Vector3(transform.position.x , height, zBorderMax);
        }
        if (transform.position.x<xBorderMin) {
            transform.position = new Vector3(xBorderMin , height, transform.position.z);
        }
        if (transform.position.x>xBorderMax) {
            transform.position = new Vector3(xBorderMax , height, transform.position.z);
        }
    }
}
