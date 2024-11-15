using UnityEngine;
using TMPro;

public class TextBoxController : MonoBehaviour
{
    public static TextBoxController Instance { get; private set; }

    public GameObject textBox; // Reference to the text box
    public TextMeshProUGUI textField; // Text display for the text

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("TextBoxController Start");
        // Ensure the text box is initially enabled
        textBox.SetActive(true);
    }

    public void ShowTextBox(string text)
    {
        Debug.Log("Showing text box with text: " + text);
        textBox.SetActive(true);
        textField.text = text;
    }

    public void HideTextBox()
    {
        Debug.Log("Hiding text box");
        textBox.SetActive(false);
    }

    public bool IsTextBoxActive()
    {
        return textBox.activeSelf;
    }
}