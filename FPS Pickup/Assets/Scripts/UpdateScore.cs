using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour {
    [Header("Score Text")]
	[SerializeField] TextMeshProUGUI score;

    // Update is called once per frame
    void Update() {
        score.text = Globals.score;
    }
}
