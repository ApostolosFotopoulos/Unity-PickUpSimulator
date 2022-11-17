using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyChange : MonoBehaviour
{
    [Header("Mask Object")]
	[SerializeField] GameObject mask;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Equals) && mask.transform.localScale.x<1f) {
            mask.transform.localScale += new Vector3(0.2f,0f,0f);
            Globals.difficultyLevel += 1f;
        }
        if (Input.GetKeyDown(KeyCode.Minus) && mask.transform.localScale.x>0.1f) {
            mask.transform.localScale -= new Vector3(0.2f,0f,0f);
            Globals.difficultyLevel -= 1f;
        }
    }
}
