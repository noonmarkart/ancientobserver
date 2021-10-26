using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTwoAxis : MonoBehaviour
{
    //since it's 3d I had to freeze the rotation of the capsule on the rigidbody to prevent the capsule from tumbling around
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public bool frozen = false;

    // Start is called before the first frame update
    void Start()
    {
        //cache the rigidbody on this game object
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //I put the physics update here - fixed framerate = consistent
    //any physics in the Update() has to be multiplied by Time.deltaTime to be frame independent
    private void FixedUpdate()
    {
        if (!frozen)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(moveX, 0, moveZ);

            //due to vector math diagonal movements are faster, this limits the max speed while retaining the analog smoothing
            if (moveDirection.magnitude > 1)
                moveDirection = moveDirection.normalized;

            rb.velocity = moveDirection * moveSpeed;

            // Calculate a rotation a step closer to the target and applies rotation to this object
            if (moveDirection.magnitude > 0)
                transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    //this functions can be called by the dialogue manager and block the controls
    public void Freeze()
    {
        frozen = true;
    }

    public void UnFreeze()
    {
        frozen = false;
    }
}
