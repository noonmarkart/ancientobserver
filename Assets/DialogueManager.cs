using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using TMPro;
using System.Collections;
using RedBlueGames.Tools.TextTyper;
using UnityEngine.EventSystems;
/*
 This script works with Interactable.cs and integrates ink, 
 text mesh pro UI, a character avatar and a script for text effects

    ink+unity info here
    https://github.com/inkle/ink/blob/master/Documentation/RunningYourInk.md#getting-started-with-the-runtime-api
    
    text typer here
    https://github.com/redbluegames/unity-text-typer

 * */


public class DialogueManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    [Tooltip("The player object")]
    public GameObject player;

    [Tooltip("Assign your text mesh pro textfield here")]
    public TMP_Text line;
    [Tooltip("A button prefab for the choices")]
    [SerializeField]
    private Button buttonPrefab = null;

    [Tooltip("When done typing make a little ui element appear to advance")]
    public Image confirmImage;

    [Tooltip("Remove the TextTyper script on the textfield if you don't want any effects")]
    public TextTyper typer; //the typewriter effect script
    public Transform choicesContainer;
    
    //i keep track of the state of the dialogue with an int variable
    public int dialogueState = 0;

    //the state variable can have different values that I make human-readable with constants 
    public int DIALOGUE_OFF = 0; //no dialogue
    public int WAIT_CONFIRM = 1; //line has been typed wait to advance
    public int WAIT_CHOICE = 2; //the next step will be to display the choices without advancing the story
    public int CHOICE_DISPLAYED = 3; //choices are displayed wait for player choice

    //when busy typing
    public bool typing = false;

    [Tooltip("Set true if you want the choices to appear as a separate state after a confirm action")]
    public bool waitForChoices = true;
    public float typingDelay = 0.1f;

    public Interactable[] interactables;
    public Interactable currentInteractable;
    
    private bool justPressedUI = false;

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story story;

    
    void Awake()
    {
        if (player == null)
        {
            print("Warning: the player object is not assigned, collisions won't work");
        }

        LoadDialogues();
        line.text = "";

        if(confirmImage != null)
            confirmImage.enabled = false;

        //if null falls back to plain visualization
        typer = line.gameObject.GetComponent<TextTyper>();
        
        if (typer != null)
        {
            //listeners are functions called on each character and at the end of the typing
            //the actual functions are down below
            typer.PrintCompleted.AddListener(OnDoneTyping);
            typer.CharacterPrinted.AddListener(OnCharacterPrinted);
        }
    }



    // Creates a new Story object with the compiled story which we can then play!
    void LoadDialogues()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);

        /*
        bind the function declared in the ink file to a function here
        You can have as many functions as you want with different parameters
        I just made a generic one that calls a function in a separate script
        GameEvents because I don't want to "hard code" my game logic in this class
         */

        //yeah, the binding code is a bit unusual
        story.BindExternalFunction("gameEvent", (string name) => {

            if (gameObject.GetComponent<GameEvents>() != null)
                gameObject.GetComponent<GameEvents>().Event(name);
            else
                print("You need a GameEvents class to call a gameEvent");
        });

        //I don't start the dialogue here because the story bits will be activated by the player interaction
        //StartDialogue();
    }
    

    private void Start()
    {
        //the dialogue manager looks for all the interactables in the scene so you don't have to assign them 
        //load all interactables
        interactables = Resources.FindObjectsOfTypeAll<Interactable>();

        //reference the centralized stuff
        for (int i = 0; i < interactables.Length; i++)
        {
            interactables[i].player = player;
            interactables[i].manager = this;
            
        }
    }

    private void Update()
    {

        //action/click - go to settings > input to configure what "Fire1" is
        if (Input.GetButtonDown("Fire1"))
        {
            //if typing skip - justPressedUI is an inelegant way to avoid registering a choice event 
            //that just happened as a skip action
            if (typing && !justPressedUI)
            {
                typer.Skip();
                typing = false;
            }
            //dialogue is on but not on choices and not while typing: go to the next state
            else if (dialogueState != DIALOGUE_OFF && dialogueState != CHOICE_DISPLAYED && !typing)
            {
                ContinueDialogue();
            }
            //colliding with interactable and not in dialogue mode: start the dialogue
            else if (currentInteractable != null && dialogueState == DIALOGUE_OFF)
            {
                StartDialogue(currentInteractable.id);

                /*
                //is there an ink knot that matches the interactable id?
                if (story.KnotContainerWithName(currentInteractable.id) != null)
                {
                    //start dialogue
                    StartDialogue(currentInteractable.id);
                }
                else
                {
                    print("Error: there is no knot called " + currentInteractable.id);
                }
                */

            }


        }

        justPressedUI = false;
    }

    void StartDialogue(string id)
    {
        //freeze the controller by sending a message so I don't have to know the specific class
        //what "freezing means" depends on the control system
        player.SendMessage("Freeze");
        
        //set the story at the knot
        story.ChoosePathString(id);

        ContinueDialogue();
    }


    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void ContinueDialogue()
    {

        // Remove all choices and text from UI
        RemoveChoices();
        line.text = "";
        

        //if the option wait for choices is on add a dedicated step for displaying the choices
        if (waitForChoices && dialogueState == WAIT_CHOICE)
        {
            //display choice
            DisplayChoices();
        }
        else
        {

            if (!story.canContinue)
            {
                // If we've read all the content and there's no choices, the story is finished!
                EndDialogue();
            }
            else
            {
                // go to the next state
                story.Continue();

                //display a line if any
                if (story.currentText != "")
                {
                    DisplayLine(story.currentText);
                }

                //if there are choices attached to this block
                if (story.currentChoices.Count > 0)
                {

                    //if waitForChoices don't display them immediately, wait for a confirm
                    //make an exception for choices appearing without any lines first
                    if (waitForChoices && story.currentText != "")
                    {
                        //display choice next time
                        dialogueState = WAIT_CHOICE;
                    }
                    else
                    {
                        DisplayChoices();
                    }
                }
            }
        }


    }


    void EndDialogue()
    {
        print("Dialogue has ended");
        RemoveChoices();

        //presumably still in range?
        line.maxVisibleCharacters = int.MaxValue;
        line.text = currentInteractable.actionText;

        Cursor.lockState = CursorLockMode.Locked;
        
        dialogueState = DIALOGUE_OFF;

        //freeze the controller by sending a message so I don't have to know the specific class
        player.SendMessage("UnFreeze");
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        justPressedUI = true;
        story.ChooseChoiceIndex(choice.index);

        ContinueDialogue();
    }

    // Creates a textbox showing the the line of text
    void DisplayLine(string txt)
    {
        txt = txt.Trim();

        //if the typer isn't specified just display the line
        if (typer == null)
        {
            line.text = txt;
            dialogueState = WAIT_CONFIRM;
        }
        else
        {
            typing = true;
            typer.TypeText(txt, typingDelay);
            
        }

        if (confirmImage != null)
            confirmImage.enabled = false;
    }


    void DisplayChoices()
    {

        //clear selected ui element (unity UI quirk)
        EventSystem.current.SetSelectedGameObject(null);

        dialogueState = CHOICE_DISPLAYED;
        
        if (story.currentChoices.Count > 0)
        {

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoice(choice.text.Trim());
                
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate
                {
                    OnClickChoiceButton(choice);
                });

                //select the first button
                if (i == 0)
                {
                    
                    EventSystem.current.SetSelectedGameObject(button.gameObject);
                }
            }
        }

        if (confirmImage != null)
            confirmImage.enabled = false;

    }

    //listeners for the typer script

    //when a line has been typed
    private void OnDoneTyping()
    {
        //Debug.Log("TypeText Complete");

        //wait for advance confirmation unless there are choices at the end of this block 
        if (dialogueState != WAIT_CHOICE)
            dialogueState = WAIT_CONFIRM;

        typing = false;
        
        if (confirmImage != null)
            confirmImage.enabled = true;
    }

    private void OnCharacterPrinted(string printedCharacter)
    {
        //sound and audiosources are set
        if (currentInteractable.audioSource != null && currentInteractable.typeSound != null)
        {

            // Do not play a sound for whitespace
            if (printedCharacter == " " || printedCharacter == "\n")
            {
                return;
            }
            else
            {
                int index = UnityEngine.Random.Range(0, currentInteractable.typeSound.Length);
                AudioClip clip = currentInteractable.typeSound[index];
                currentInteractable.audioSource.PlayOneShot(clip);
            }


        }
        
    }


    // Creates a button showing the choice text
    Button CreateChoice(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(choicesContainer, false);

        // Gets the text from the button prefab
        TMP_Text choiceText = choice.GetComponentInChildren<TMP_Text>();
        choiceText.text = text;

        return choice;
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChoices()
    {

        int childCount = choicesContainer.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(choicesContainer.GetChild(i).gameObject);
        }
    }

    //call
    public void InteractableInRange(Interactable i)
    {
        if (currentInteractable == null)
        {
            currentInteractable = i;

            line.maxVisibleCharacters = int.MaxValue;
            line.text = currentInteractable.actionText;

        }
    }

    public void InteractableOutOfRange(Interactable i)
    {
        if (currentInteractable == i)
        {

            currentInteractable = null;

            line.maxVisibleCharacters = int.MaxValue;
            line.text = "";


        }
    }

    
}
