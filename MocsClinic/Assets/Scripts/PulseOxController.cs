using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class PulseOxController : MonoBehaviour {
    public InputActionReference aButton;
    public InputActionReference xButton;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject pulseOx;
    public TMP_Text pulseOxHR;
    public TMP_Text pulseOxO2;
    private XRDirectInteractor rightInteractor;
    private XRDirectInteractor leftInteractor;
    private bool coroutineRunning = false;
    private bool pulseOxPower = false;

    // Start is called before the first frame update
    void Start() {
        aButton.action.started += togglePowerRight;
        xButton.action.started += togglePowerLeft;
        rightInteractor = rightHand.GetComponent<XRDirectInteractor>();
        leftInteractor = leftHand.GetComponent<XRDirectInteractor>();
    } 

    // Update is called once per frame
    void Update() {
     
    }

    private void togglePowerRight(InputAction.CallbackContext context) {
        // Check if right hand is holding pulseOx

        if (isRightHandHolding()) {
            pulseOxPower = !pulseOxPower;

            if (pulseOxPower) {
                pulseOxHR.text = "97";
                pulseOxO2.text = "77";
            }
            else {
                pulseOxHR.text = "";
                pulseOxO2.text = "";
            }
            Debug.Log((pulseOxPower) ? "Right on" : "Right off");
        }
    }

    private void togglePowerLeft(InputAction.CallbackContext context) {
        // Check if left hand is holding pulseOx

        if (isLeftHandHolding()) {
            pulseOxPower = !pulseOxPower;

            if (pulseOxPower) {
                pulseOxHR.text = "-";
                pulseOxO2.text = "-";
            }
            else {
                pulseOxHR.text = "";
                pulseOxO2.text = "";
            }
            Debug.Log((pulseOxPower) ? "Left on" : "Left off");
        }
    }

    private bool isRightHandHolding() {
        return rightInteractor.selectTarget != null && rightInteractor.selectTarget.name == "PulseOximeter";
    }

    private bool isLeftHandHolding() {
        return leftInteractor.selectTarget != null && leftInteractor.selectTarget.name == "PulseOximeter";
    }

}
