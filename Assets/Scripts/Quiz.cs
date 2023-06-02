using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    void Start()
    {
        GetNextQuestion();
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    void SetDefaultButtonSprites()
    {
        foreach (var button in answerButtons)
            button.GetComponent<Image>().sprite = defaultAnswerSprite;
    }

    public void OnAnswerSelected(int index)
    {
        SetButtonState(false);

        Image buttonImage;
        buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
        
        if (index == question.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
        }
        else
        {
            correctAnswerIndex = question.GetCorrectAnswerIndex();
            string correctAnswer = question.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was\n" + correctAnswer;
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.GetAnswer(i);
    }

    void SetButtonState(bool state)
    {
        foreach (var button in answerButtons)
            button.GetComponent<Button>().interactable = state;
    }
}
