using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    private bool isMainMenuActive = false;

    void Start()
    {
        DisplayText();
    }

    void Update()
    {
        if (Input.anyKeyDown && isPhaseTextActive)
        {
            currentTextIndex++;
            if (currentTextIndex < phaseTexts.Length)
            {
                DisplayText();
            }
            else
            {
                TextBoxController.Instance.HideTextBox();
                ShowTaskBox();
                isPhaseTextActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMainMenu();
        }
    }

    void DisplayText()
    {
        TextBoxController.Instance.ShowTextBox(phaseTexts[currentTextIndex]);
    }

    void ShowTaskBox()
    {
        if (!TextBoxController.Instance.IsTextBoxActive())
        {
            TaskBoxController.Instance.ShowTaskBox("Find the wizard in the north, next to a tower and press 'E' to interact with him.");
        }
    }

    public void ToggleMainMenu()
    {
        if (isMainMenuActive)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("Main Menu");
            isMainMenuActive = false;
        }
        else
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
            isMainMenuActive = true;
        }
    }
}