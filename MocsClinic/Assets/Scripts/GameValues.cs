using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameValues : MonoBehaviour {
    public string parentName;
    public string childName;
    public string dob;
    public string age;
    public string sex;
    public float temperature;
    public int hr;
    public int O2Sat;
    public int systolic;
    public int diastolic;
    public int respiratoryRate;
    public int heartSound;
    public bool generateRandomValues;
    public AudioSource regularNSR;
    public AudioSource regularS3;
    public AudioSource regularS4;
    public AudioSource fastNSR;
    public AudioSource fastS3;
    public AudioSource fastS4;
    public AudioSource slowNSR;
    public AudioSource slowS3;
    public AudioSource slowS4;
    public Text parentNameText;
    public Text childNameText;
    public Text dobText;
    public Text ageText;
    public Text sexText;

    [HideInInspector]
    public AudioSource activeHeartSound;

    // Start is called before the first frame update
    void Start() {
        parentNameText.text = "Accompanied by: " + parentName;
        childNameText.text = "Patient name: " + childName;
        dobText.text = "DOB: " + dob;
        ageText.text = "Age: " + age;
        sexText.text = "Patient Sex: " + sex;

        if (generateRandomValues) {
            System.Random rnd = new System.Random();
            temperature = (float)(35 + (rnd.NextDouble() * 5));
            hr = rnd.Next(40, 120);
            O2Sat = (rnd.Next(1, 5) <= 2) ? rnd.Next(90, 94) : rnd.Next(95, 100);
            systolic = rnd.Next(90, 140);
            diastolic = rnd.Next(60, 90);
            // Heart Sound Chart:
            // 1 = NSR (Normal Sinus Rhythm)
            // 2 = S3
            // 3 = S4
            heartSound = rnd.Next(1, 3);
        }

        if (hr < 60) {
            switch (heartSound) {
                case 1:
                    activeHeartSound = slowNSR;
                    break;
                case 2:
                    activeHeartSound = slowS3;
                    break;
                case 3:
                    activeHeartSound = slowS4;
                    break;
            }
        }
        else if (hr > 100) {
            switch (heartSound) {
                case 1:
                    activeHeartSound = fastNSR;
                    break;
                case 2:
                    activeHeartSound = fastS3;
                    break;
                case 3:
                    activeHeartSound = fastS4;
                    break;
            }
        }
        else {
            switch (heartSound) {
                case 1:
                    activeHeartSound = regularNSR;
                    break;
                case 2:
                    activeHeartSound = regularS3;
                    break;
                case 3:
                    activeHeartSound = regularS4;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
