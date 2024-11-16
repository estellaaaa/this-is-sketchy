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
        // Ensure the text box is initially disabled
        textBox.SetActive(false);
    }

    public void ShowTextBox(string text)
    {
        Debug.Log("Showing text box with text: " + text);
        if (textBox == null)
        {
            Debug.LogError("textBox is not assigned!");
            return;
        }
        if (textField == null)
        {
            Debug.LogError("textField is not assigned!");
            return;
        }
        textBox.SetActive(true);
        textField.text = text;
        Debug.Log("Text box should now be active" + textBox.activeSelf);
    }

    public void HideTextBox()
    {
        Debug.Log("Hiding text box");
        if (textBox != null)
        {
            textBox.SetActive(false);
            Debug.Log("Text box should now be inactive. Active state: " + textBox.activeSelf);
        }
        else
        {
            Debug.LogError("textBox is not assigned!");
        }
    }

    public bool IsTextBoxActive()
    {
        return textBox != null && textBox.activeSelf;
    }
}