using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using Autohand;

public class ThermometerController : MonoBehaviour {
    #pragma warning disable 0618
    public InputActionReference aButton;
    public InputActionReference xButton;
    public InputActionReference rightTrigger;
    public InputActionReference leftTrigger;
    public InputActionReference rightGrip;
    public InputActionReference leftGrip;
    public Hand rightHand;
    public Hand leftHand;
    public GameObject thermometer;
    public Grabbable thermometerGrabbable;
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
    } 

    // Update is called once per frame
    void Update() {
    }

    private void getTempRight(InputAction.CallbackContext context) {
        if (thermometerPower && isRightHandHolding()) {
            if (!coroutineRunning) {
                StartCoroutine(getTemp());
            }
        }
    }

    private void getTempLeft(InputAction.CallbackContext context) {
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
        }
    }

    private IEnumerator getTemp() {
        // Yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2);

        // Play a beep sound
        beep.Play();

        // Generate a random number
        System.Random rnd = new System.Random();
        double temp = 95.5 + (rnd.NextDouble() * 8.5);

        // Set the text to 37 C
        thermometerText.text = Math.Round(temp, 1) + " °F";
    }

    private bool isRightHandHolding() {
        Hand[] hands = thermometerGrabbable.GetHeldBy().ToArray();
        return hands.Length > 0 && hands[0] == rightHand;
    }

    private bool isLeftHandHolding() {
        Hand[] hands = thermometerGrabbable.GetHeldBy().ToArray();
        return hands.Length > 0 && hands[0] == leftHand;
    }

    private void rightHold(InputAction.CallbackContext context) {
        StartCoroutine(rightHoldCoroutine());
    }

    private IEnumerator rightHoldCoroutine() {
        yield return 0;
        yield return 0;

        if (isRightHandHolding()) {
            thermometer.transform.localRotation = Quaternion.Euler(51, 19, 96);
        }
    }

    private void leftHold(InputAction.CallbackContext context) {
        StartCoroutine(leftHoldCoroutine());
    }

    private IEnumerator leftHoldCoroutine() {
        yield return 0;
        yield return 0;

        if (isLeftHandHolding()) {
            thermometer.transform.localRotation = Quaternion.Euler(-42, -153, 64);
        }
    }
    #pragma warning restore 0618
}
