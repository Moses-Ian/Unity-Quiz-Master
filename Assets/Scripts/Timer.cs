using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public bool displayAnswer;
    public bool isAnsweringQuestion;
    public float fillFraction;

    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (timerValue > 0)
        {
            fillFraction = timerValue / (isAnsweringQuestion ? timeToCompleteQuestion : timeToShowCorrectAnswer);
        }
        else
        {
            if (isAnsweringQuestion)
            {
                timerValue = timeToShowCorrectAnswer;
                displayAnswer = true;
            }
            else
            {
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
            isAnsweringQuestion = !isAnsweringQuestion;
        }
    }
}
