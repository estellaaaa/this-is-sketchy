using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroTextController : MonoBehaviour
{
    public TextMeshProUGUI textField; // Textfeld (bei TextMeshPro) oder Text, wenn du UI.Text verwendest
    public string[] introTexts; // Liste der Textnachrichten
    private int currentTextIndex = 0;

    void Start()
    {
        if (introTexts.Length > 0)
        {
            DisplayText();
        }
        else
        {
            Debug.LogWarning("Keine Texte für das Intro festgelegt!");
        }
    }

    void Update()
    {
        if (Input.anyKeyDown) // Beliebige Taste
        {
            currentTextIndex++;
            if (currentTextIndex < introTexts.Length)
            {
                DisplayText();
            }
            else
            {
                EndIntro();
            }
        }
    }

    void DisplayText()
    {
        textField.text = introTexts[currentTextIndex];
    }

    void EndIntro()
    {
        // Aktion nach dem Intro, z. B. Szene wechseln
        Debug.Log("Intro abgeschlossen!");
        // UnityEngine.SceneManagement.SceneManager.LoadScene("Phase 1"); // Ersetze durch deinen Szenennamen
    }
}
