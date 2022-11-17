using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PickUpController : MonoBehaviour{
	[Header("Pickup Settings")]
	[SerializeField] GameObject removeItemCheck;
	[SerializeField] float removeItemRange = 0.18f;
	private GameObject heldObj;
	private Rigidbody heldObjRB;
	private Collider heldObjCOL;

	[Header("Pickup Objects")]
	[SerializeField] GameObject[] objectsToPickUp;
	int removedObjectsCount = 0;
	
	[Header("Physics Parameters")]
	[SerializeField] float pickUpRange = 0.4f;
	[SerializeField] float pickUpForce = 150.0f;
	
    
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
		if (removedObjectsCount < objectsToPickUp.Length) {
			GameObject objectToPickUp = FindClosestObject();
			float dist = Vector3.Distance(objectToPickUp.transform.position,transform.position);
			for (int i = 0; i < objectsToPickUp.Length; i++) {
				if (objectsToPickUp[i] != null) {
					if (objectsToPickUp[i] != objectToPickUp) {
						Outline outline = objectsToPickUp[i].GetComponent<Outline>();
						outline.outlineColor = Color.red;
						outline.UpdateMaterialProperties();
					}
					else {
						Outline outline = objectToPickUp.GetComponent<Outline>();
						if (dist<ChangeDistanceFromDifficulty()) {
							outline.outlineColor = Color.blue;
						}
						else {
							outline.outlineColor = Color.red;
						}
						outline.UpdateMaterialProperties();
					}
				}
				
			}

			if (Input.GetMouseButtonDown(0)) {
				if (heldObj == null) {
					if (dist<ChangeDistanceFromDifficulty()) {
						PickUpObject(objectToPickUp);
					}
				}
				else {
					//DropObject();
				}
			}
			
			if (heldObj != null) {
				if (removePickedObjectCheck()) {
					removedObjectsCount++;
					Destroy(heldObj);
					Globals.score = removedObjectsCount + "/3";
				}
				else {
					MoveObject();
				}
				
			}
		}
		else {
			//next scene?
		}
		
    }

	bool removePickedObjectCheck() {
		float dist = Vector3.Distance(heldObj.transform.position,removeItemCheck.transform.position);
		Debug.Log(dist);
		if (dist<removeItemRange) {
			return true;
		}
		else {
			return false;
		}
	}

    void MoveObject() {
		if (Vector3.Distance(heldObj.transform.position, transform.position) > 0.1f) {
			Vector3 moveDirection = transform.position - heldObj.transform.position;
			heldObjRB.AddForce(moveDirection*pickUpForce);
		}
	}
	
	void PickUpObject(GameObject pickObj) {
		if (pickObj.GetComponent<Rigidbody>()) {
			heldObjRB = pickObj.GetComponent<Rigidbody>();
			heldObjCOL = pickObj.GetComponent<Collider>();
			heldObjRB.isKinematic = true;
			heldObjCOL.enabled = false;
			
			heldObjRB.transform.parent = transform;
			heldObj = pickObj;
			heldObj.transform.localPosition = new Vector3(-0.053f,-0.059f,-0.01f);
		}
	}
	
	void DropObject() {
		heldObjRB.isKinematic = false;
		heldObjCOL.enabled = true;
		
		heldObjRB.transform.parent = null;
		heldObj = null;
	}

	float ChangeDistanceFromDifficulty() {
		float extraDistance = 0;
		if (Globals.difficultyLevel == 0f) {
			extraDistance = 0.20f;
		}
		else if (Globals.difficultyLevel == 1f) {
			extraDistance = 0.15f;
		}
		else if (Globals.difficultyLevel == 2f) {
			extraDistance = 0.10f;
		}
		else if (Globals.difficultyLevel == 3f) {
			extraDistance = 0.05f;
		}
		else {
			extraDistance = 0f;
		}

		return pickUpRange + extraDistance;
	}

	GameObject FindClosestObject() {
		float[] distances = new float[objectsToPickUp.Length];

		for (int i = 0; i < objectsToPickUp.Length; i++) {
			if (objectsToPickUp[i] != null) {
				distances[i] = Vector3.Distance(objectsToPickUp[i].transform.position,transform.position);
			}
			else {
				distances[i] = 10000f;
			}
		}

		float min = distances[0];
		int pos = 0;
		for (int i = 0; i < distances.Length; i++) {
			if (distances[i] < min) {
				min = distances[i];
				pos = i;
			}
		}

		return objectsToPickUp[pos];
	}
}
