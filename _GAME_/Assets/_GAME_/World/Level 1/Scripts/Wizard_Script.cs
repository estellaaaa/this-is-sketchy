using UnityEngine;

public class WizardScript : MonoBehaviour
{
    private string[] wizardTexts = new string[]
    {
        "Welcome, hero!",
        "I have been waiting for you.",
        "Your journey is just beginning.",
        "You must defeat the evil that plagues this land.",
        "Good luck, brave hero!"
    };

    private int currentTextIndex = 0;
    private bool nearWizard = false;

    void Update()
    {
        // Check for interaction with the wizard
        if (nearWizard && Input.GetKeyDown(KeyCode.E))
        {
            DisplayWizardText();
        }
    }

    void DisplayWizardText()
    {
        if (currentTextIndex < wizardTexts.Length)
        {
            TextBoxController.Instance.ShowTextBox(wizardTexts[currentTextIndex]);
            currentTextIndex++;
        }
        else
        {
            TextBoxController.Instance.HideTextBox();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearWizard = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearWizard = false;
        }
    }
}