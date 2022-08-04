using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GradeController : MonoBehaviour {
    [HideInInspector]
    public int subjectiveGrade;
    [HideInInspector]
    public int objectiveGrade;

    private string gradeFile;
    
    // Start is called before the first frame update
    void Start() {
        subjectiveGrade = 65;
        objectiveGrade = 100;

        Directory.CreateDirectory(Application.streamingAssetsPath + "/Grades");

        gradeFile = Application.streamingAssetsPath + "/Grades/grade.txt";

        if (!File.Exists(gradeFile)) {
            File.WriteAllText(gradeFile, "MYNAMEISJEFF");
        }
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
