using UnityEngine;
using TMPro;

public class Phase_1_Script : MonoBehaviour
{
    private string[] phaseTexts = new string[]
    {
        "oh you're finally awake",
        "sorry that that i cannot greet you in person...",
        "but i got a little exhausted creating you",
        "tsk tsk tsk... i got old",
        "<i>who are you?</i>",
        "oh, i'm just a humble wizard of this world, nothing special",
        "but you, you are special",
        "you are the hero of this world",
        "you are the one who will save us all",
        "you are the one who will defeat the evil that plagues this land",
        "you are the one who will bring peace to this world",
        "so please, before you can start your journey",
        "find me so i can explain you everything",
        "please excuse me for giving you such a rough start",
        "but i have no other choice",
        "i will rest now till you find me",
        "...",
        "ahh... i almost forgot",
        "you can move with the w,s,a,d keys",
        "and you will find me in the north, next to a tower",
    };

    private int currentTextIndex = 0;
    private bool isPhaseTextActive = true;

    void Start()
    {
        // Display the first text immediately when the scene starts
        DisplayText();
    }

    void Update()
    {
        if (Input.anyKeyDown && isPhaseTextActive) // Advance text on any key press
        {
            currentTextIndex++;
            if (currentTextIndex < phaseTexts.Length)
            {
                DisplayText();
            }
            else
            {
                // Hide the text box to allow player movement
                TextBoxController.Instance.HideTextBox();
                ShowTaskBox();
                isPhaseTextActive = false;
            }
        }
    }

    void DisplayText()
    {
        TextBoxController.Instance.ShowTextBox(phaseTexts[currentTextIndex]);
    }

    void ShowTaskBox()
    {
        // Check if the text box is inactive and show the task box
        if (!TextBoxController.Instance.IsTextBoxActive())
        {
            TaskBoxController.Instance.ShowTaskBox("Find the wizard in the north, next to a tower and press 'E' to interact with him.");
        }
    }
}