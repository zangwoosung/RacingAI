using UnityEngine;
using UnityEngine.UIElements;

public class StringComparer : MonoBehaviour
{
    [SerializeField] UIDocument _mainUI;

    public TextField inputField; // Assign via UI Builder or script
    public Label resultLabel;    // Label to show formatted result
    public string answer = "I love you too";
    VisualElement root;

    void Start()
    {

        root = _mainUI.rootVisualElement;
        inputField = root.Q<TextField>();
        resultLabel = root.Q<Label>();

        inputField.RegisterValueChangedCallback(evt =>
        {
            string userInput = evt.newValue;
            resultLabel.text = GetFormattedComparison(userInput, answer);
        });
    }

    string GetFormattedComparison(string input, string answer)
    {
        int maxLength = Mathf.Max(input.Length, answer.Length);
        string formatted = "";

        for (int i = 0; i < maxLength; i++)
        {
            char inputChar = i < input.Length ? input[i] : ' ';
            char answerChar = i < answer.Length ? answer[i] : ' ';

            if (inputChar == answerChar)
            {
                formatted += inputChar;
            }
            else
            {
                string mycolor = "yellow";
                formatted += $"<color={mycolor}>{inputChar}</color>";
            }
        }

        return formatted;
    }
}
