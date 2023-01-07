using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action onTimerEnd = delegate { };

    public float timerStart;
    private float timeLeft;

    public float durationInStep;

    private void Start()
    {
        timeLeft = timerStart;
        durationInStep = timerStart / Time.fixedDeltaTime;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) {
            onTimerEnd?.Invoke();
        }
    }

    public void resetTimer() {
        timeLeft = timerStart;
    }

}
