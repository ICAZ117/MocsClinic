using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBreath : MonoBehaviour {
    public GameObject chest;
    public GameObject gameController;
    private Vector3 scaleChange, positionChange;
    // Start is called before the first frame update
    void Start() {
        // Cool math formula
        scaleChange = new Vector3(0f, (gameController.GetComponent<GameValues>().respiratoryRate / 4.5f) * -0.0001f, 0f);
    }

    // Update is called once per frame
    void Update() {
        chest.transform.localScale += scaleChange;

        if (chest.transform.localScale.y < 0.92f || chest.transform.localScale.y > 1.00f) {
            scaleChange = -scaleChange;
        }
    }
}