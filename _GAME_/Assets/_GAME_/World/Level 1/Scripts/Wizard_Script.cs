using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WizardScript : MonoBehaviour
{
    private List<string[]> taskTexts = new List<string[]>
    {
        new string[]
        {
            "Welcome, hero!",
            "I have been waiting for you.",
            "Your journey is just beginning.",
            "You must defeat the evil that plagues this land.",
            "For that, you will need to create tools",
            "and gather items",
            "Unfortunately, the pencil which I used to create you broke during the process.",
            "So you will have to find the pieces to repair it.",
            "Walk around to find the pieces of wood.",
        },
        new string[]
        {
            "Great job collecting the sti... wait NO!",
            "The evil are back leaving their mark.",
            "Be quick, we have to stop them!",
            "For that, we need to collect pieces of rubber.",
            "To craft a tool to defeat them.",
            "THE ERASER!",
            "QUICKLY!!! FIND THE PIECES!!!",
        }
        // Add more task-specific texts if needed
    };

    private int currentTextIndex = 0;
    private bool nearWizard = false;
    public float interactionDistance = 2.0f; // Distance within which the player can interact
    private Transform playerTransform;
    private bool isDisplayingText = false;
    private bool initialTaskStarted = false;

    public static WizardScript Instance { get; private set; }

    public ObjectSpawner objectSpawner; // Reference to the ObjectSpawner

    private List<Task> tasks = new List<Task>();
    private int currentTaskIndex = 0;

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
        Debug.Log("WizardScript Start");

        // Find the player GameObject by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            Debug.Log("Player found: " + player.name);
        }
        else
        {
            Debug.LogError("Player not found! Interactions will not work.");
        }

        // Initialize tasks
        InitializeTasks();

        if (tasks.Count == 0)
        {
            Debug.LogError("No tasks initialized. Please check the InitializeTasks method.");
            return; // Exit to prevent further errors
        }

        // Check if objectSpawner is assigned
        if (objectSpawner == null)
        {
            Debug.LogError("ObjectSpawner is not assigned in WizardScript!");
            return;
        }

        // Initialize task box and set it to the first task
        UpdateTaskBox();
    }


    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            nearWizard = distance <= interactionDistance;

            if (nearWizard && !isDisplayingText)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    CheckTaskCompletion();
                    DisplayWizardText();
                }
            }

            if (isDisplayingText && Input.anyKeyDown)
            {
                currentTextIndex++;
                if (currentTextIndex < taskTexts[currentTaskIndex].Length)
                {
                    DisplayWizardText();
                }
                else
                {
                    // End of text; hide and reset
                    TextBoxController.Instance.HideTextBox();
                    isDisplayingText = false;
                    currentTextIndex = 0;
                    UpdateTaskBox();
                }
            }
        }
    }

    void DisplayWizardText()
    {
        if (currentTaskIndex >= taskTexts.Count)
        {
            Debug.LogError($"No text defined for task index {currentTaskIndex}");
            return;
        }

        string[] activeTexts = taskTexts[currentTaskIndex];
        if (currentTextIndex < activeTexts.Length)
        {
            if (TextBoxController.Instance != null)
            {
                if (TaskBoxController.Instance != null && TaskBoxController.Instance.taskBox.activeSelf)
                {
                    TaskBoxController.Instance.HideTaskBox();
                }

                TextBoxController.Instance.ShowTextBox(activeTexts[currentTextIndex]);
                isDisplayingText = true;
            }
            else
            {
                Debug.LogError("TextBoxController Instance is null!");
            }
        }
    }


void CheckTaskCompletion()
{
    if (TaskBoxController.Instance.IsGoalReached(tasks[currentTaskIndex].itemName))
    {
        currentTextIndex = 0;

        if (TaskBoxController.Instance != null)
        {
            TaskBoxController.Instance.HideTaskBox();
        }

        // Advance to the next task
        AdvanceToNextTask();

        // Start spawning objects
        StartCoroutine(WaitForObjectsToFinishSpawning());
    }
    else
    {
        if (!initialTaskStarted)
        {
            initialTaskStarted = true;
            StartInitialTask();
        }
    }
}


    IEnumerator WaitForObjectsToFinishSpawning()
    {
        yield return StartCoroutine(objectSpawner.SpawnAndMoveObjects());

        float timeout = 10f;
        while (!objectSpawner.isSpawningComplete && timeout > 0f)
        {
            timeout -= Time.deltaTime;
            yield return null;
        }

        AdvanceToNextTask();  // This ensures that once the spawning is done, the next task is shown.
    }


    void UpdateTaskBox()
    {
        if (TaskBoxController.Instance != null)
        {
            TaskBoxController.Instance.ShowTaskBox(tasks[currentTaskIndex].description);
        }
    }

    void StartInitialTask()
    {
        ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
        if (itemSpawner != null)
        {
            Debug.Log("Starting to spawn sticks.");
            itemSpawner.StartSpawningSticks();
        }
        else
        {
            Debug.LogError("ItemSpawner not found!");
        }
    }


    void InitializeTasks()
    {
        tasks.Add(new Task("Stick", 4, "find the 4 sticks to fix the pencil"));
        tasks.Add(new Task("Rubber", 15, "look for the 15 rubber pieces to defeat the evil"));
    }

    public void AdvanceToNextTask()
    {
        if (currentTaskIndex < tasks.Count - 1)
        {
            currentTaskIndex++;
            TaskBoxController.Instance.SetItemGoal(tasks[currentTaskIndex].itemName, tasks[currentTaskIndex].goal);
            UpdateTaskBox();

            // Reset text index before displaying the new task's text
            currentTextIndex = 0;
            DisplayWizardText();  // Show next task text when advancing.
        }
        else
        {
            TaskBoxController.Instance.HideTaskBox();
            DisplayWizardText();  // Final text after completing all tasks
        }
    }



    public class Task
    {
        public string itemName;
        public int goal;
        public string description;

        public Task(string itemName, int goal, string description)
        {
            this.itemName = itemName;
            this.goal = goal;
            this.description = description;
        }
    }
}