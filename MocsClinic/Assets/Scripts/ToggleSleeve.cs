using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSleeve : MonoBehaviour {
    public GameObject cuff;
    public GameObject cylinder;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void EnableCylinder() {
        cuff.SetActive(false);
        cylinder.SetActive(true);
    }

    public void EnableCuff() {
        cuff.SetActive(true);
        cylinder.SetActive(false);
    }
}
