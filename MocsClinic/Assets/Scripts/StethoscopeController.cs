using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class StethoscopeController : MonoBehaviour {
    public PlacePoint head;
    public GameObject tubing;
    public GameObject staticChestPiece;
    public GameObject dynamicChestPiece;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private bool isStethoscopeOn() {
        return head.placedObject != null;
    }
}
