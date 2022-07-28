using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recognissimo.Components;

public class CommunicationController : MonoBehaviour {

    public GameObject voiceControl;
    [HideInInspector]
    public int currentQuestion;
    private VoiceControl script;
    //private List<string> phrases = ;
    
    // Start is called before the first frame update
    void Start() {
        currentQuestion = 0;
        script = voiceControl.GetComponent<VoiceControl>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void questionDetected(int questionNumber) {
        if (questionNumber == currentQuestion) {
            Debug.Log("Question " + currentQuestion++ + " detected, moving on to question " + currentQuestion);
        }
    }

    //public void NextQuestion() {
    //    currentQuestion++;
    //    script.commands[0].phrases[0] = "Hello World";

    //    // Deactivate the script
    //    script.enabled = false;
    //    // Activate the script
    //    script.enabled = true;
    //    Debug.Log("HEEEE SAAAID WATERMELLONNNNN");
    //}
}
