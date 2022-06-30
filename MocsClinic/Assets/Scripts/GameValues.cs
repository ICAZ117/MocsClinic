using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour {
    public float temperature;
    public int hr;
    public int O2Sat;
    public int systolic;
    public int diastolic;
    public bool generateRandomValues;

    // Start is called before the first frame update
    void Start() {
        if (generateRandomValues) {
            System.Random rnd = new System.Random();
            temperature = (float)(35 + (rnd.NextDouble() * 5));
            hr = rnd.Next(40, 120);
            O2Sat = (rnd.Next(1, 5) <= 2) ? rnd.Next(90, 94) : rnd.Next(95, 100);
            systolic = rnd.Next(90, 140);
            diastolic = rnd.Next(60, 90);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
