using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class StethoscopeController : MonoBehaviour {
    public PlacePoint head;
    public GameObject tubing;
    public GameObject staticChestPiece;
    public GameObject dynamicChestPiece;
    public PlacePoint rightArm;
    public PlacePoint leftArm;
    public GameObject needle;
    public GameObject gameController;
    public AudioSource korotkoffSound;
    public Grabbable chestPieceGrabbable;
    public Hand primaryHand;

    [HideInInspector]
    public bool inAorticRegion = false;
    [HideInInspector]
    public bool inPulmonaryRegion = false;
    [HideInInspector]
    public bool inTricuspidRegion = false;
    [HideInInspector]
    public bool inMitralRegion = false;
    [HideInInspector]
    public bool onRightArm = false;
    [HideInInspector]
    public bool onLeftArm = false;

    private bool isHeartSoundPlaying = false;
    private bool isKorotkoffSoundPlaying = false;
    private bool continuePlayingKorotkoffSound = false;
    private float systolic;
    private float diastolic;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(init());
    }

    private IEnumerator init() {
        yield return null;
        yield return null;
        systolic = mmHgToDegrees(gameController.GetComponent<GameValues>().systolic);
        diastolic = mmHgToDegrees(gameController.GetComponent<GameValues>().diastolic);
    }

    // Update is called once per frame
    void Update() {
        if ((inAorticRegion || inPulmonaryRegion || inTricuspidRegion || inMitralRegion) && !isHeartSoundPlaying) {
            isHeartSoundPlaying = true;
            StartCoroutine(playHeartSound());
        }

        // IF...
        //// 1. The chest piece is on the right arm AND the sleeve is on the right arm, OR if the chest piece is on the left arm AND the sleeve is on the left arm
        //if ((rightArm.placedObject != null && onRightArm) || (leftArm.placedObject != null && onLeftArm)) {
        //    // 2. The Korotkoff coroutine isn't running
        //    if (!isKorotkoffSoundPlaying) {
        //        // 3. The air is being released from the blood pressure sleeve
        //        if (gameController.GetComponent<BloodPressureController>().isNeedleRunning) {
        //            // 4. The rotation of the pointer is within our systolic-diastolic range defined in GameValues
        //            if (needle.transform.rotation.eulerAngles.z <= systolic && needle.transform.rotation.eulerAngles.z >= diastolic) {
        //                Debug.Log("EVERYTHING IS TRUE");
        //            }
        //        }

        //    }
        //}

        // If the stars align and the oculus is struck with a cosmic ray
        if (((rightArm.placedObject != null && onRightArm) || (leftArm.placedObject != null && onLeftArm)) && (gameController.GetComponent<BloodPressureController>().isNeedleRunning) && (needle.transform.rotation.eulerAngles.z <= systolic && needle.transform.rotation.eulerAngles.z >= diastolic)) {
            continuePlayingKorotkoffSound = true;
            
            if ((!isKorotkoffSoundPlaying)) {
                isKorotkoffSoundPlaying = true;
                StartCoroutine(playKorotkoffSound());
            }
        }
        else {
            continuePlayingKorotkoffSound = false;
        }



        // THEN, start the Korotkoff coroutine
    }

    private float mmHgToDegrees(int mmHg) {
        return ((mmHg - 5) / 5) * 4.49152542f;
    }

    private IEnumerator playKorotkoffSound() {
        // Play sound
        korotkoffSound.Play();

        // Loop till end
        while (continuePlayingKorotkoffSound) {
            yield return null;
        }

        // Stop sound
        korotkoffSound.Stop();

        // Reset flag
        isKorotkoffSoundPlaying = false;
    }

    private IEnumerator playHeartSound() {
        gameController.GetComponent<GameValues>().activeHeartSound.Play();

        while (inAorticRegion || inPulmonaryRegion || inTricuspidRegion || inMitralRegion) {
            yield return null;
        }

        gameController.GetComponent<GameValues>().activeHeartSound.Stop();
        isHeartSoundPlaying = false;
    }

    // Wrapper method to launch coroutine which releases and deactivated the chestpiece
    public void EnableStaticStethoscopeWrapper() {
        Debug.Log("EnableStaticStethoscopeWrapper");

        StartCoroutine(EnableStaticStethoscope());
    }

    private IEnumerator EnableStaticStethoscope() {
        Debug.Log("EnableStaticStethoscope");

        // Release the grabLocked chest piece
        primaryHand.Release();

        // Wait a frame just in case the chest piece is still being grabbed
        yield return null;

        // Reactive the tubing and immovable chestpiece from the stethoscope
        tubing.SetActive(true);
        staticChestPiece.SetActive(true);

        // Deactivate the dynamic chestpiece grabbable
        dynamicChestPiece.SetActive(false);
    }

    // Wrapper method to launch coroutine which activates and TryGrabs the chestpiece
    public void EnableDynamicStethoscopeWrapper() {
        StartCoroutine(EnableDynamicStethoscope());
    }

    public IEnumerator EnableDynamicStethoscope() {
        // Deactivate the tubing and immovable chestpiece from the stethoscope
        tubing.SetActive(false);
        staticChestPiece.SetActive(false);

        // Activate the dynamic chestpiece grabbable
        dynamicChestPiece.SetActive(true);

        // Wait one frame for the chestpiece to be activated, and then TryGrab it
        yield return null;
        primaryHand.TryGrab(chestPieceGrabbable);
    }
}
