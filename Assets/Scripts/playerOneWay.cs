using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerOneWay : MonoBehaviour
{
    public bool fallThrough;
    
    void Update()
    {
        if(Keyboard.current.downArrowKey.isPressed())
        {
            fallThrough = true;
        } else
        {
            fallThrough = false;
        }
    }
    
}
