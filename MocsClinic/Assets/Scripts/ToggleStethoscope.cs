using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class ToggleStethoscope : MonoBehaviour {
    public GameObject tubing;
    public GameObject staticChestPiece;
    public GameObject dynamicChestPiece;
    public Grabbable chestPieceGrabbable;
    public Hand primaryHand;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void EnableStaticStethoscope() {
        tubing.SetActive(true);
        staticChestPiece.SetActive(true);
        dynamicChestPiece.SetActive(false);
        primaryHand.Release();
    }

    public void EnableDynamicStethoscope() {
        tubing.SetActive(false);
        staticChestPiece.SetActive(false);
        dynamicChestPiece.SetActive(true);
        primaryHand.TryGrab(chestPieceGrabbable);
    }
}
