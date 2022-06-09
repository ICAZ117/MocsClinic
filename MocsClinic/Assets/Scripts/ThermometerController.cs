using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ThermometerController : MonoBehaviour {
    #pragma warning disable 0618
    public InputActionReference aButton;
    public InputActionReference xButton;
    public InputActionReference rightTrigger;
    public InputActionReference leftTrigger;
    public InputActionReference rightGrip;
    public InputActionReference leftGrip;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject thermometer;
    public TMP_Text thermometerText;
    public AudioSource beep;
    private XRDirectInteractor rightInteractor;
    private XRDirectInteractor leftInteractor;
    private bool coroutineRunning = false;
    private bool thermometerPower = false;

    // Start is called before the first frame update
    void Start() {
        aButton.action.started += togglePowerRight;
        xButton.action.started += togglePowerLeft;
        rightTrigger.action.started += getTempRight;
        leftTrigger.action.started += getTempLeft;
        rightGrip.action.started += rightHold;
        leftGrip.action.started += leftHold;
        rightInteractor = rightHand.GetComponent<XRDirectInteractor>();
        leftInteractor = leftHand.GetComponent<XRDirectInteractor>();
    } 

    // Update is called once per frame
    void Update() {
     
    }

    private void getTempRight(InputAction.CallbackContext context) {
        Debug.Log("Right trigger");
        if (thermometerPower && isRightHandHolding()) {
            if (!coroutineRunning) {
                StartCoroutine(getTemp());
            }
        }
    }

    private void getTempLeft(InputAction.CallbackContext context) {
        Debug.Log("Left trigger");
        if (thermometerPower && isLeftHandHolding()) {
            if (!coroutineRunning) {
                StartCoroutine(getTemp());
            }
        }
    }

    private void togglePowerRight(InputAction.CallbackContext context) {
        // Check if right hand is holding thermometer
        if (isRightHandHolding()) {
            thermometerPower = !thermometerPower;

            if (thermometerPower) {
                thermometerText.text = "00.0° F";
            }
            else {
                thermometerText.text = "";
            }
            Debug.Log((thermometerPower) ? "Right on" : "Right off");
        }
    }

    private void togglePowerLeft(InputAction.CallbackContext context) {
        // Check if left hand is holding thermometer
        if (isLeftHandHolding()) {
            thermometerPower = !thermometerPower;

            if (thermometerPower) {
                thermometerText.text = "00.0° F";
            }
            else {
                thermometerText.text = "";
            }
            Debug.Log((thermometerPower) ? "Left on" : "Left off");
        }
    }

    private IEnumerator getTemp() {
        // Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        // Yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2);

        // After we have waited 2 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        // Play a beep sound
        beep.Play();

        // Generate a random number
        System.Random rnd = new System.Random();
        double temp = 95.5 + (rnd.NextDouble() * 8.5);

        // Set the text to 37 C
        thermometerText.text = Math.Round(temp, 1) + " °F";
    }

    private bool isRightHandHolding() {
        return rightInteractor.selectTarget != null && rightInteractor.selectTarget.name == "TympanicThermometer";
    }

    private bool isLeftHandHolding() {
        return leftInteractor.selectTarget != null && leftInteractor.selectTarget.name == "TympanicThermometer";
    }

    private void rightHold(InputAction.CallbackContext context) {
        StartCoroutine(rightHoldCoroutine());
    }

    private IEnumerator rightHoldCoroutine() {
        yield return 0;
        yield return 0;

        Debug.Log("Right hold" + isRightHandHolding());
        if (isRightHandHolding()) {
            Debug.Log("Before: " + thermometer.transform.localRotation);
            thermometer.transform.localRotation = Quaternion.Euler(51, 19, 96);
            Debug.Log("After: " + thermometer.transform.localRotation);
        }
    }

    private void leftHold(InputAction.CallbackContext context) {
        StartCoroutine(leftHoldCoroutine());
    }

    private IEnumerator leftHoldCoroutine() {
        yield return 0;
        yield return 0;

        Debug.Log("Left hold" + isLeftHandHolding());
        if (isLeftHandHolding()) {
            Debug.Log("Before: " + thermometer.transform.localRotation);
            thermometer.transform.localRotation = Quaternion.Euler(-42, -153, 64);
            Debug.Log("After: " + thermometer.transform.localRotation);
        }
    }
    #pragma warning restore 0618
}
