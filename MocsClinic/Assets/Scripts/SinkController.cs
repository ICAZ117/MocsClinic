using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkController : MonoBehaviour {
    public GameObject leftHandle;
    public GameObject rightHandle;
    public GameObject sinkWater;

    // Start is called before the first frame update
    void Start() {
        // Deactivate the water
        sinkWater.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        // Get the rotation of the left handle
        Quaternion leftHandleRotation = leftHandle.transform.rotation;

        // Get the rotation of the right handle
        Quaternion rightHandleRotation = rightHandle.transform.rotation;

        // Print the rotation of the left handle
        Debug.Log("LEFT: " + leftHandleRotation.y);

        // Print the rotation of the right handle
        Debug.Log("RIGHT: " + rightHandleRotation.y);

        // Check if either handle has been rotated
        if (rightHandleRotation.y < 0.6 || leftHandleRotation.y > 0.8) {
            // Activate the water
            sinkWater.SetActive(true);
        }
        else {
            // Deactivate the water
            sinkWater.SetActive(false);
        }
    }
}
