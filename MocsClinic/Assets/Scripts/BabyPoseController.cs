using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPoseController : MonoBehaviour {
    public GameObject pelvis;
    public GameObject leftHumerus; // 0%: 18, 48, 5    100%: -31, 50, 0
    public GameObject rightHumerus; // 0%: 18, -48, -5    100%: -31, -50, 5
    public GameObject leftFemur; // -0.1229999 0.0160567 -0.08458216
    public GameObject rightFemur; // 0.1229999 0.0160567 -0.08458216
    public GameObject placedBaby;
    public GameObject carriedBaby;
    
    // Start is called before the first frame update
    void Start() {

    }

    public void placeBaby() {
        placedBaby.SetActive(true);
        carriedBaby.SetActive(false);

        Debug.Log("PLACE");
    }

    public void carryBaby() {
        placedBaby.SetActive(false);
        carriedBaby.SetActive(true);
        Debug.Log("CARRY");
    }

    // Update is called once per frame
    void Update() {
        //leftFemur.transform.localPosition = new Vector3(-0.1229999f, 0.0160567f, -0.08458216f);
        //rightFemur.transform.localPosition = new Vector3(0.1229999f, 0.0160567f, -0.08458216f);
        //Debug.Log("leftFemur: " + leftFemur.transform.localPosition);
        //Debug.Log("rightFemur: " + rightFemur.transform.localPosition);

        // Calculate current percentage of rotation
        float pelvisRotation = pelvis.transform.localRotation.eulerAngles.x; // 0%: x = 100    100%: x = 10

        if (pelvisRotation > 360) {
            pelvisRotation -= 360;
        }

        //Debug.Log(pelvisRotation);

        // Calculate current percentage of rotation
        float percentage = (pelvisRotation / 90);


        //Debug.Log(pelvisRotation);

        // Calculate new x rotations for each joint
        float leftHumerusX = calcRotation(18, -31, percentage);
        float rightHumerusX = calcRotation(18, -31, percentage);

        // Calculate new y rotations for each joint
        float leftHumerusY = calcRotation(48, 50, percentage);
        float rightHumerusY = calcRotation(-48, -50, percentage);

        // Calculate new z rotations for each joint
        float leftHumerusZ = calcRotation(5, 0, percentage);
        float rightHumerusZ = calcRotation(-5, 5, percentage);


        // Set new rotations for each joint
        leftHumerus.transform.localRotation = Quaternion.Euler(leftHumerusX, leftHumerusY, leftHumerusZ);
        rightHumerus.transform.localRotation = Quaternion.Euler(rightHumerusX, rightHumerusY, rightHumerusZ);

    }

    private float calcRotation(float start, float end, float percentage) {
        return (start + (percentage * (end - start)));
    }

    
}
