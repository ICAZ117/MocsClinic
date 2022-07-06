using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMeshController : MonoBehaviour {
    public SkinnedMeshRenderer meshRenderer;
    public MeshCollider collider;

    // Start is called before the first frame update
    void Start() {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        collider.sharedMesh = null;
        collider.sharedMesh = colliderMesh;
    }

    // Update is called once per frame
    void Update() {

    }

}
