using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ThermometerController : MonoBehaviour {
    public InputActionReference aButton;
    public InputActionReference xButton;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject thermometer;
    private XRDirectInteractor rightInteractor;
    private XRDirectInteractor leftInteractor;
    private bool thermometerPower = false;

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

    void togglePowerRight(InputAction.CallbackContext context) {
        // Check if right hand is holding thermometer
        if (rightInteractor.selectTarget != null && rightInteractor.selectTarget.name == "EarThermometer") {
            thermometerPower = !thermometerPower;
            Debug.Log((thermometerPower) ? "Right on" : "Right off");
        }
    }

    void togglePowerLeft(InputAction.CallbackContext context) {
        // Check if left hand is holding thermometer
        if (leftInteractor.selectTarget != null && leftInteractor.selectTarget.name == "EarThermometer") {
            thermometerPower = !thermometerPower;
            Debug.Log((thermometerPower) ? "Left on" : "Left off");
        }
    }
}
