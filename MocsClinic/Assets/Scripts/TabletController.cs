using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletController : MonoBehaviour {
    public GameObject gameController;
    public GameObject toggleUIPrefab;
    public GameObject content;
    public List<string> questions;
    private bool[] displayedQuestions;

    // Start is called before the first frame update
    void Start() {
        displayedQuestions = new bool[questions.Count];
    }

    // Update is called once per frame
    void Update() {

    }

    public void addQuestion(int idx) {
        if (idx >= questions.Count || questions[idx] == "" || displayedQuestions[idx]) {
            return;
        }

        GameObject newQuestion = Instantiate(toggleUIPrefab, content.transform);
        newQuestion.transform.GetChild(1).gameObject.GetComponent<Text>().text = questions[idx];
        displayedQuestions[idx] = true;
    }
}
