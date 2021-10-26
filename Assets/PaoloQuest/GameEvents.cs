using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEvents : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
    }

    void Update()
    {
    }

    //
    public void Event(string id)
    {
        print("Event " + id + " just happened");

        switch(id)
        {
            case "test":
                //
                print("testing");
                break;

            default:
                print("Event " + id + " not implemented");
                break;
        }
        
    }
    

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
