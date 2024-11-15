using System.Collections;
using UnityEngine;
using TMPro;

public class Phase_1_Script : MonoBehaviour
{
    public TextMeshProUGUI textField; // Text display for the phase
    public GameObject textBox; // Reference to the text box
    public string[] phaseTexts = new string[]
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

    void Start()
    {
        // Display the first text
        DisplayText();
    }

    void Update()
    {
        if (Input.anyKeyDown) // Advance text on any key press
        {
            currentTextIndex++;
            if (currentTextIndex < phaseTexts.Length)
            {
                DisplayText();
            }
            else
            {
                // Hide the text box to allow player movement
                textBox.SetActive(false);
            }
        }

        // Check if the text box is inactive and show the task box
        if (!textBox.activeSelf && currentTextIndex >= phaseTexts.Length)
        {
            TaskBoxController.Instance.ShowTaskBox("Current Task: Find the wizard in the north, next to a tower.");
        }
    }

    void DisplayText()
    {
        textField.text = phaseTexts[currentTextIndex];
    }
}