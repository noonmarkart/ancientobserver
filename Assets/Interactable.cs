using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This script triggers a UI message and/or a sound when the player enters an area and looks at the object.
 * This script has to be attached to an object with a collider marked as trigger (the area) and a regular collider the player has to look at (unless areaOnly = true)
 * */

public class Interactable : MonoBehaviour
{

    [Tooltip("The player. It must have a collider to trigger events. If blank assumes an object named \"Player\"")]
    public GameObject player;
    public DialogueManager manager;

    [Tooltip("Set to true if you want the interaction to be possible regardless of the player direction")]
    public bool areaOnly = false;

    [Tooltip("The name of the knot associated to it")]
    public string id = "";

    [Tooltip("The action that designated the interaction")]
    public string actionText = "Interact with cube";

    [Tooltip("The sound(s) produced when typing")]
    public AudioClip[] typeSound;

    [Tooltip("The audiosource of the sound")]
    public AudioSource audioSource;

    void Start()
    {

        //check if there is a collider
        Collider2D[] cols = transform.GetComponentsInChildren<Collider2D>();
        bool oneTrigger = false;

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].isTrigger)
                oneTrigger = true;
        }

        if (cols.Length == 0)
        {
            print("Warning: the interactable " + gameObject.name + " doesn't have any colliders attached");
        }
        else if (!oneTrigger)
        {
            print("Warning: the interactable " + gameObject.name + " doesn't have any colliders set on trigger attached");
        }

        //if no audiosource check this game object
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    //for as long as the player stays in the area
    private void OnTriggerStay2D(Collider2D other)
    {
        //if the dialogue isn't on and the other thing in range is the player
        if (other.gameObject == player && manager.dialogueState == manager.DIALOGUE_OFF)
        {

            //if area only tell the manager this interactable is in range
            if (areaOnly)
            {
                manager.InteractableInRange(this);
            }
            else
            {
                /*
                otherwise do a raycast to see if the player is looking at the thing. This check truly depends on the game view
                you should raycast from the center of the camera in first person or from the direction of the player in a 3rd person view
                */
                RaycastHit objectHit;
                bool inRange = false;

                //the raycast starts from the center of the player and shoots in the forward direction
                Vector3 fwd = player.transform.TransformDirection(Vector3.forward);
                //draw a debug ray
                Debug.DrawRay(player.transform.position, fwd * 4, Color.green);

                //shoots the actual raycast 
                if (Physics.Raycast(player.transform.position, fwd, out objectHit, 1000))
                {
                    //do something if hit object is this one
                    if (objectHit.collider.gameObject == gameObject)
                    {
                        manager.InteractableInRange(this);
                        inRange = true;
                    }

                }

                //not hit = out of range
                if (!inRange)
                {
                    manager.InteractableOutOfRange(this);
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       
            manager.InteractableOutOfRange(this);
       
    }


}
