using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    void Start()
    {
        Debug.Log(question.GetQuestion());
        Debug.Log(questionText.text);
        questionText.text = question.GetQuestion();
    }

}
