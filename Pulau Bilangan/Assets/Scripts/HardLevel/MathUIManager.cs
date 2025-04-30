using UnityEngine;
using TMPro;

public class MathUIManager : MonoBehaviour
{
    public TextMeshProUGUI answerText;
    private int currentAnswer = 0;

    public void UpdateAnswer(int newAnswer)
    {
        currentAnswer = newAnswer;
        answerText.text = currentAnswer.ToString();
    }
}
