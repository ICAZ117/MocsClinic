using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMeshController : MonoBehaviour {
    public GameObject pelvis;
    public GameObject LeftHumerus;
    public GameObject RightHumerus;
    public GameObject LeftArm;
    public GameObject RightArm;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Debug.Log("Pelvis: " + pelvis.transform.localRotation.eulerAngles);
    }

}
