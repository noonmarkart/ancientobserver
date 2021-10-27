using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public string prompt = "Would you like to enter the past?";
    public Transform spawnDestination;

    public void ExecuteTeleport(GameObject targetObject)
    {
        targetObject.transform.position = spawnDestination.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ExecuteTeleport(collider.gameObject);
    }
}
