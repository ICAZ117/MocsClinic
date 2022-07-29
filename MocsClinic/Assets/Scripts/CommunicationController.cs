using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationController : MonoBehaviour {
    [HideInInspector]
    public int currentQuestion;

    public GameObject head;
    private AudioSource audioSource;
    public AudioClip audioClipOne;
    public AudioClip audioClipTwo;
    public AudioClip audioClipThree;
    public AudioClip audioClipFour;
    public AudioClip audioClipFive;
    public AudioClip audioClipSix;
    public AudioClip audioClipSeven;
    public AudioClip audioClipEight;
    public int[] responseNumber = {1, 2, 3, 4, 5, 6, 7, 7, 8, 7, 7, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 7, 8, 7, 7, 7};

    // Start is called before the first frame update
    void Start() {
        currentQuestion = 0;
        audioSource = head.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void questionDetected(int questionNumber) {
        if (questionNumber == currentQuestion) {
            Debug.Log("Question " + currentQuestion + " detected");

            // Make question appear on tablet
            
            StartCoroutine(respond(questionNumber));
        }
    }

    private IEnumerator respond(int questionNumber) {
        // Save current question
        int temp = currentQuestion;

        // Prevent user from triggering next question until this response is finished
        currentQuestion = -1;

        // Wait a second for realism
        yield return new WaitForSeconds(1);

        switch (responseNumber[temp]) {
            case 1:
                audioSource.clip = audioClipOne;
                break;
            case 2:
                audioSource.clip = audioClipTwo;
                break;
            case 3:
                audioSource.clip = audioClipThree;
                break;
            case 4:
                audioSource.clip = audioClipFour;
                break;
            case 5:
                audioSource.clip = audioClipFive;
                break;
            case 6:
                audioSource.clip = audioClipSix;
                break;
            case 7:
                audioSource.clip = audioClipSeven;
                break;
            case 8:
                audioSource.clip = audioClipEight;
                break;
            default:
                Debug.Log("Invalid question number");
                break;
        }
        // Play audio
        audioSource.Play();

        // Wait for audio to finish
        yield return new WaitForSeconds(audioSource.clip.length + 1);

        currentQuestion = ++temp;
    }
}
