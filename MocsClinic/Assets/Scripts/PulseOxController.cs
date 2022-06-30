using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using Autohand;

public class PulseOxController : MonoBehaviour {
#pragma warning disable 0618
    public InputActionReference aButton;
    public InputActionReference xButton;
    public Hand rightHand;
    public Hand leftHand;
    public GameObject pulseOx;
    public Grabbable pulseOxGrabbable;
    public TMP_Text pulseOxHR;
    public TMP_Text pulseOxO2;
    private XRDirectInteractor rightInteractor;
    private XRDirectInteractor leftInteractor;
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
        Hand[] hands = pulseOxGrabbable.GetHeldBy().ToArray();
        return hands.Length > 0 && hands[0] == rightHand;
    }

    private bool isLeftHandHolding() {
        Hand[] hands = pulseOxGrabbable.GetHeldBy().ToArray();
        return hands.Length > 0 && hands[0] == leftHand;
    }
#pragma warning restore 0618
}
