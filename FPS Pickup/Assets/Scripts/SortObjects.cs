using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortObjects : MonoBehaviour {
    [Header("Camera")]
	[SerializeField] Camera cam;
    // Start is called before the first frame update
    void Update() {
        cam.transparencySortMode = TransparencySortMode.Orthographic;
    }
}
