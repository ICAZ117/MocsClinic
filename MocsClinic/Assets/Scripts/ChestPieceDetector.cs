using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPieceDetector : MonoBehaviour {
    public GameObject chestPiece;
    public GameObject gameController;
    private Collider boundsCollider;
    private Collider chestPieceCollider;

    // Start is called before the first frame update
    void Start() {
        boundsCollider = GetComponent<Collider>();
        chestPieceCollider = chestPiece.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnTriggerEnter(Collider other) {
        if (other == chestPieceCollider) {
            switch (name) {
                case "Aortic":
                    Debug.Log("Enter Aortic");
                    gameController.GetComponent<StethoscopeController>().inAorticRegion = true;
                    break;
                case "Pulmonary":
                    Debug.Log("Enter Pulmonary");
                    gameController.GetComponent<StethoscopeController>().inPulmonaryRegion = true;
                    break;
                case "Tricuspid":
                    Debug.Log("Enter Tricuspid");
                    gameController.GetComponent<StethoscopeController>().inTricuspidRegion = true;
                    break;
                case "Mitral":
                    Debug.Log("Enter Mitral");
                    gameController.GetComponent<StethoscopeController>().inMitralRegion = true;
                    break;
                case "RightArmChestPiece":
                    Debug.Log("Enter RightArm");
                    gameController.GetComponent<StethoscopeController>().onRightArm = true;
                    break;
                case "LeftArmChestPiece":
                    Debug.Log("Enter LeftArm");
                    gameController.GetComponent<StethoscopeController>().onLeftArm = true;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        //Debug.Log(other.name + " " + chestPieceCollider.name);
        if (other == chestPieceCollider) {
            switch (name) {
                case "Aortic":
                    Debug.Log("Exit Aortic");
                    gameController.GetComponent<StethoscopeController>().inAorticRegion = false;
                    break;
                case "Pulmonary":
                    Debug.Log("Exit Pulmonary");
                    gameController.GetComponent<StethoscopeController>().inPulmonaryRegion = false;
                    break;
                case "Tricuspid":
                    Debug.Log("Exit Tricuspid");
                    gameController.GetComponent<StethoscopeController>().inTricuspidRegion = false;
                    break;
                case "Mitral":
                    Debug.Log("Exit Mitral");
                    gameController.GetComponent<StethoscopeController>().inMitralRegion = false;
                    break;
                case "RightArmChestPiece":
                    Debug.Log("Exit RightArm");
                    gameController.GetComponent<StethoscopeController>().onRightArm = false;
                    break;
                case "LeftArmChestPiece":
                    Debug.Log("Exit LeftArm");
                    gameController.GetComponent<StethoscopeController>().onLeftArm = false;
                    break;
            }
        }
    }

}
