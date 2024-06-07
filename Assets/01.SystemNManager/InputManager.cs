using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float GetInputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }
    public static float GetInputVertical()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public static Vector3 GetInputMouse()
    {
        return Input.mousePosition;
    }
}
