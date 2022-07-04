using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Autohand;


public class BloodPressureController : MonoBehaviour {
    public InputActionReference rightGrip;
    public InputActionReference leftGrip;
    public InputActionReference AButton;
    public InputActionReference XButton;
    public Hand rightHand;
    public Hand leftHand;
    public Finger rightThumb;
    public Finger rightIndex;
    public Finger rightMiddle;
    public Finger rightRing;
    public Finger rightPinky;
    public Finger leftThumb;
    public Finger leftIndex;
    public Finger leftMiddle;
    public Finger leftRing;
    public Finger leftPinky;
    public GameObject BPPump;
    public GameObject needle;
    public PlacePoint rightArm;
    public PlacePoint leftArm;
    public Grabbable BPPumpGrabbable;
    public AudioSource pumpSound;
    public AudioSource releaseSound;
    private bool isPumpRunning = false;
    [HideInInspector]
    public bool isNeedleRunning = false;
    private bool AButtonHeld = false;
    private bool XButtonHeld = false;
    private Coroutine resetCoroutine = null;
    // Start is called before the first frame update
    void Start() {
        rightGrip.action.started += pumpRight;
        leftGrip.action.started += pumpLeft;
        AButton.action.started += AButtonPress;
        AButton.action.canceled += AButtonRelease;
        XButton.action.started += XButtonPress;
        XButton.action.canceled += XButtonRelease;

    }

    // Update is called once per frame
    void Update() {
    }

    private void AButtonPress(InputAction.CallbackContext context) {
        AButtonHeld = true;
    }

    private void AButtonRelease(InputAction.CallbackContext context) {
        AButtonHeld = false;
    }

    private void XButtonPress(InputAction.CallbackContext context) {
        XButtonHeld = true;
    }

    private void XButtonRelease(InputAction.CallbackContext context) {
        XButtonHeld = false;
    }

    public void releaseWrapper() {
        // Bruh, this some 3380 garbage
        StartCoroutine(startRelease());
    }

    IEnumerator startRelease() {
        if (isSleeveOn() && !isPumpRunning && !isNeedleRunning && needle.transform.localRotation.eulerAngles.z > 0) {
            yield return null;
            if ((isRightHandHolding() && AButtonHeld) || (isLeftHandHolding() && XButtonHeld)) {
                isNeedleRunning = true;
                releaseSound.Play();
                resetCoroutine = StartCoroutine(resetNeedle(needle.transform));
            }
        }   
    }

    // This is a wrapper, you can tell its a wrapper by the way that it is
    public void releaseWrapperTwo() {
        StartCoroutine(endRelease());
    }

    IEnumerator endRelease() {
        yield return null; // <-- stupid
        if (((isRightHandHolding() && !AButtonHeld) || (isLeftHandHolding() && !XButtonHeld)) && resetCoroutine != null) {
            releaseSound.Stop();
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
            isNeedleRunning = false;
        }
    }

    private void pumpRight(InputAction.CallbackContext context) {
        System.Random rnd = new System.Random();
        if (isRightHandHolding() && isSleeveOn() && !isPumpRunning && !isNeedleRunning && needle.transform.localRotation.eulerAngles.z < 264) {
            isPumpRunning = true;
            StartCoroutine(doPump(BPPump.transform, new Vector3(0.7f, 1.25f, 1.25f), 0.25f));
            isNeedleRunning = true;
            StartCoroutine(rotateNeedle(needle.transform, (float)((rnd.NextDouble() * 10) + 40), 0.25f)); 
        }
            
    }

    private void pumpLeft(InputAction.CallbackContext context) {
        System.Random rnd = new System.Random();
        if (isLeftHandHolding() && isSleeveOn() && !isPumpRunning && !isNeedleRunning && needle.transform.localRotation.eulerAngles.z < 264) {
            isPumpRunning = true;
            StartCoroutine(doPump(BPPump.transform, new Vector3(0.7f, 1.25f, 1.25f), 0.25f));
            isNeedleRunning = true;
            StartCoroutine(rotateNeedle(needle.transform, (float)((rnd.NextDouble() * 10) + 40), 0.25f)); 
        }
    }

    IEnumerator doPump(Transform transform, Vector3 upScale, float duration) {
        Vector3 initialScale = transform.localScale;
        pumpSound.Play();
        for (float time = 0; time < duration * 2; time += Time.deltaTime) {
            rightThumb.BendFingerUntilHit(100, 31);
            rightMiddle.BendFingerUntilHit(100, 31);
            rightRing.BendFingerUntilHit(100, 31);
            rightPinky.BendFingerUntilHit(100, 31);
            float progress = Mathf.PingPong(time, duration) / duration;
            transform.localScale = Vector3.Lerp(initialScale, upScale, progress);
            yield return null;
        }
        transform.localScale = initialScale;
        isPumpRunning = false;
    }

    IEnumerator rotateNeedle(Transform transform, float rotation, float duration) {
        Quaternion initialRotation = transform.localRotation;
        if (initialRotation.eulerAngles.z + rotation > 265) {
            rotation = 265 - initialRotation.eulerAngles.z;
        }
        for (float time = 0; time < duration; time += Time.deltaTime) {
            float progress = Mathf.PingPong(time, duration);
            transform.localRotation = Quaternion.Slerp(initialRotation, Quaternion.Euler(0,0,initialRotation.eulerAngles.z + rotation), progress);
            yield return null;
        }
        isNeedleRunning = false;
    }

    // Trying to get the needle to rotate the correct direction when deflating
    // Weird error where after deflating it requires a lot more pumps to inflate / won't inflate right away
    IEnumerator resetNeedle(Transform transform) {
        while (transform.localRotation.eulerAngles.z > 0.31f && transform.localRotation.eulerAngles.z < 266) {
            transform.localRotation = Quaternion.Euler(0,0,transform.localRotation.eulerAngles.z - 0.1f);
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(0,0,0);
        releaseSound.Stop();
        isNeedleRunning = false;
    }

    private bool isSleeveOn() {
        return (rightArm.placedObject != null || leftArm.placedObject != null);
    }

    private bool isRightHandHolding() {
        Hand[] hands = BPPumpGrabbable.GetHeldBy().ToArray();
        return hands.Length > 0 && hands[0] == rightHand;
    }

    private bool isLeftHandHolding() {
        Hand[] hands = BPPumpGrabbable.GetHeldBy().ToArray();
        return hands.Length > 0 && hands[0] == leftHand;
    }
}
