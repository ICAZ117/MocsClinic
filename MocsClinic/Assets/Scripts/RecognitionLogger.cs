using UnityEngine;
using Recognissimo.Core; // PartialResult, Result

public class RecognitionLogger : MonoBehaviour {

    public bool log;

    public void OnPartialResult(PartialResult partialResult) {
        if (log) {
            Debug.Log($"<color=yellow>{partialResult.partial}</color>");
        }
    }

    public void OnResult(Result result) {
        if (log) {
            Debug.Log($"<color=green>{result.text}</color>");
        }
    }

}