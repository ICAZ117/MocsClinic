using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPoseController : MonoBehaviour {
    public GameObject pelvis;
    public GameObject LeftHumerus; // 0%: 18, 48, 5    100%: -31, 50, 0
    public GameObject RightHumerus; // 0%: 18, -48, -5    100%: -31, -50, 5
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // Calculate current percentage of rotation
        float pelvisRotation = pelvis.transform.localRotation.eulerAngles.x + 90; // 0%: x = 100    100%: x = 10

        if (pelvisRotation > 360) {
            pelvisRotation -= 360;
        }

        //Debug.Log(pelvisRotation);

        // Calculate current percentage of rotation
        float percentage = ((100 - pelvisRotation) / 90);

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
        LeftHumerus.transform.localRotation = Quaternion.Euler(leftHumerusX, leftHumerusY, leftHumerusZ);
        RightHumerus.transform.localRotation = Quaternion.Euler(rightHumerusX, rightHumerusY, rightHumerusZ);

    }

    private float calcRotation(float start, float end, float percentage) {
        return (start + (percentage * (end - start)));
    }

    
}
