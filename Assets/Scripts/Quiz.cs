using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions;
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    [SerializeField] Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] ScoreKeeper scoreKeeper;

    void Start()
    {
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        if (timer.displayAnswer)
        {
            DisplayAnswer(-1);
            timer.displayAnswer = false;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        questions.Remove(currentQuestion);
    }

    void SetDefaultButtonSprites()
    {
        foreach (var button in answerButtons)
            button.GetComponent<Image>().sprite = defaultAnswerSprite;
    }

    public void OnAnswerSelected(int index)
    {
        Debug.Log(index);
        DisplayAnswer(index);
        timer.CancelTimer();
        scoreText.text = $"Score: {scoreKeeper.CalculateScore()}%";
    }

    public void DisplayAnswer(int index)
    {
        SetButtonState(false);

        Image buttonImage;
        buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;

        Debug.Log(currentQuestion.GetCorrectAnswerIndex());

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was\n" + correctAnswer;
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetAnswer(i);
    }

    void SetButtonState(bool state)
    {
        foreach (var button in answerButtons)
            button.GetComponent<Button>().interactable = state;
    }
}
