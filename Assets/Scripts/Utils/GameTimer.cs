using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameTimer : MonoBehaviour {
    private float Timer;
    private int SecondsInt;
    private float SecondsFloat;
    public string TIMER_LABEL = "Timer: ";

    public Text TimerString;

    public float TickRate = 0.5f;

    // Start is called before the first frame update
    void Start() {
        this.InitGameTimer();
        TimerString = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        this.RunTimer();
    }

    public void RunTimer() {
        SecondsFloat = (TickRate += Time.deltaTime);
        SecondsInt = (int) SecondsFloat;
        this.ChangeTimerText();
    }

    public void ChangeTimerText() {
        TimerString.text = TIMER_LABEL + SecondsInt.ToString();
    }

    public void InitGameTimer() {
        this.SetTimer(0.0f);
        this.SetSecondsInt(0);
    }

    public void ResetGameTimer() {
        this.SetTimer(0.0f);
        this.SetSecondsInt(0);
    }

    public void SetTimer(float timer) {
        this.Timer = timer;
    }

    public float GetTimer() {
        return this.Timer;
    }

    public void SetSecondsInt(int seconds) {
        this.SecondsInt = seconds;
    }

    public float GetSecondsInt() {
        return this.SecondsInt;
    }

    public void SetSecondsFloat(float secondsFloat) {
        this.SecondsFloat = secondsFloat;
    }

    public float GetSecondsFloat() {
        return this.SecondsFloat;
    }

}
