using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //what you want to follow
    public Transform target;
    //the offset from the target origin (eg in platformers you want more space above the characters)
    public Vector3 offset;
    //the smoothing factor
    public float smoothSpeed = 0.125f;

    //shift the camera according to the player direction so it doesn't lag behind
    //it assumes the player object is facing the direction
    public float lookAhead = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //to the fixed offset add another offset dependent on the player direction
        Vector3 predictiveOffset = target.transform.forward* lookAhead;
        
        //this is a vector operation, the 3 components of the vectors are added
        Vector3 desiredPosition = target.position + offset + predictiveOffset;
        //linearly interpolate between the current camera position and the desired position so it lags behind
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
    }
}
