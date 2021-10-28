using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private WhiteFlashController whiteFlashController;

    public delegate void OnAction();
    public OnAction onTeleport;

    public string prompt = "Would you like to enter the past?";
    public Transform spawnDestination;
    public bool executeOnTrigger = false;

    private void Start()
    {
        whiteFlashController = FindObjectOfType<WhiteFlashController>();
        if (whiteFlashController != null)
            onTeleport += whiteFlashController.ExecuteTransition;
    }

    public void ExecuteTeleport(GameObject targetObject)
    {
        targetObject.transform.position = spawnDestination.position;

        onTeleport?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (executeOnTrigger)
            ExecuteTeleport(collider.gameObject);
    }
}
