using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GradeController : MonoBehaviour {
    //[HideInInspector]
    public string studentFirstName;
    public string studentLastName;
    //[HideInInspector]
    public string studentID;
    [HideInInspector]
    public string date;

    [HideInInspector]
    public int subjectiveGrade;
    [HideInInspector]
    public int objectiveGrade;

    private string gradeFile;

    // Start is called before the first frame update
    void Start() {
        subjectiveGrade = 65;
        objectiveGrade = 100;

        date = System.DateTime.Now.ToString(new CultureInfo("en-US"));

        Directory.CreateDirectory(Application.streamingAssetsPath + "/Grades");

        gradeFile = Application.streamingAssetsPath + "/Grades/" + studentLastName + "-" + studentFirstName + "-" + System.DateTime.Now.ToString("MM-dd-yyyy", new CultureInfo("en-US")) + ".txt";

        File.WriteAllText(gradeFile, string.Format("NAME: {0} {1}\nID: {2}\nDATE: {3}\n--------------------------------------------------------------------------------\n",
                studentFirstName, studentLastName, studentID, date));
    }

    // Update is called once per frame
    void Update() {

    }

    public void decrementSubjectiveGrade() {
        subjectiveGrade--;
        // Log new grade
        Debug.Log("Subjective grade: " + subjectiveGrade);
    }

    public void submit() {
        Debug.Log("Submitting grade");
    }
}
