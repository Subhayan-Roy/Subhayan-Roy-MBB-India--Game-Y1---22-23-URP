using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Keyboard controls for button down, hold and button up

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump Button has been pressed once");
        }

        if (Input.GetButton("Jump"))
        {
            Debug.Log("I am holding the button down");
        }

        if (Input.GetButtonUp("Jump"))
        {
            Debug.Log("I have lifted my finger");
        }

        //Keyboard controls for a specific keyboard key
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("G has been pressed once");
        }

        //Mouse Controls
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("I am touching screen");
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("I am holding touch on screen");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("I have released touch on screen");
        }
    }
    
}
