using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootingReflexController : MonoBehaviour {
    public Collider rightIndex;
    public Collider leftIndex;
    public GameObject head;
    private bool isCoroutineRunning = false;
    
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other == rightIndex || other == leftIndex) {
            switch (name) {
                case "RightCheek":
                    if (!isCoroutineRunning) {
                        StartCoroutine(rootingReflex(head.transform, 40f, 1f));
                    }
                    break;
                case "LeftCheek":
                    if (!isCoroutineRunning) {
                        StartCoroutine(rootingReflex(head.transform, -40f, 1f));
                    }
                    break;
            }
        }
    }

    IEnumerator rootingReflex(Transform transform, float rotation, float duration) {
        isCoroutineRunning = true;
        
        Quaternion initialRotation = transform.localRotation;
        
        for (float time = 0; time < duration; time += Time.deltaTime) {
            float progress = Mathf.PingPong(time, duration);
            transform.localRotation = Quaternion.Slerp(initialRotation, Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z + rotation), progress);
            yield return null;
        }

        rotation *= -1;
        initialRotation = transform.localRotation;

        for (float time = 0; time < duration; time += Time.deltaTime) {
            float progress = Mathf.PingPong(time, duration);
            transform.localRotation = Quaternion.Slerp(initialRotation, Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z + rotation), progress);
            yield return null;
        }
        
        isCoroutineRunning = false;
    }
}
