using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //GameObject door;

    public Vector3 transformChange;

    public bool doorIsClosed = true;
    float closedPosY = -3.66f;
    float openPosY = 2.9f;


    // Start is called before the first frame update
    private void Start()
    {
        //door = GetComponent<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (doorIsClosed == true && transform.position.y != closedPosY)
        {
            transform.position -= transformChange * Time.deltaTime;
        }
        else if (doorIsClosed == false && transform.position.y != openPosY)
        {
            transform.position += transformChange * Time.deltaTime;
        }

        if (transform.position.y > closedPosY)
        {
            //RB.velocity.x 
            doorIsClosed = false;
        }

        if (transform.position.y < openPosY)
        {
            doorIsClosed = true;
        }
    }
}
