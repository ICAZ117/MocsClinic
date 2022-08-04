using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeController : MonoBehaviour {
    [HideInInspector]
    public int subjectiveGrade;
    [HideInInspector]
    public int objectiveGrade;

    // Start is called before the first frame update
    void Start() {
        subjectiveGrade = 65;
        objectiveGrade = 100;
    }

    // Update is called once per frame
    void Update() {

    }

    public void decrementSubjectiveGrade() {
        subjectiveGrade--;
        // Log new grade
        Debug.Log("Subjective grade: " + subjectiveGrade);
    }
}
