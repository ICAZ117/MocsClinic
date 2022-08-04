using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response {
    public string description;
    [SerializeField]
    public AudioClip[] audioClips;
}

public class CommunicationController : MonoBehaviour {
    [HideInInspector]
    public int currentQuestion;

    public GameObject gameController;
    public GameObject head;
    private AudioSource audioSource;

    public List<Response> responses;

    public int[] defaultResponses;

    // Start is called before the first frame update
    void Start() {
        currentQuestion = 0;
        audioSource = head.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void showNextQuestion() {
        gameController.GetComponent<TabletController>().addQuestion(currentQuestion);
        gameController.GetComponent<GradeController>().decrementSubjectiveGrade();
    }

    public void questionDetected(string question) {
        // Initialize variables
        int questionNumber = -1, responseNumber = -1;

        // Extract the question number from the string
        int.TryParse(question.Split(' ')[0], out questionNumber);

        // If there is a response number, extract it as well
        if (question.Split(' ').Length > 1) {
            int.TryParse(question.Split(' ')[1], out responseNumber);
        }

        // If the question number matches the current question, launch the response sequence
        if (questionNumber == currentQuestion) {
            // Log question detection to console
            Debug.Log("Question " + currentQuestion + " detected");

            // Make question appear on tablet
            gameController.GetComponent<TabletController>().addQuestion(questionNumber);

            // Launch coroutine to play response audio
            StartCoroutine(respond(questionNumber, responseNumber));
        }
    }

    private IEnumerator respond(int questionNumber, int responseNumber) {
        // Save current question
        int temp = currentQuestion;

        // Prevent user from triggering next question until this response is finished
        currentQuestion = -1;

        // If not response was provided, use the default response for the current question
        if (responseNumber == -1) {
            responseNumber = defaultResponses[temp];
        }

        // Set the audio clip to a random clip from the specified response category
        audioSource.clip = responses[responseNumber].audioClips[Random.Range(0, responses[responseNumber].audioClips.Length)];

        // Play audio
        audioSource.Play();

        // Wait for audio to finish
        yield return new WaitForSeconds(audioSource.clip.length + 1);

        currentQuestion = ++temp;
    }
}


