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
        Vector3 mouseVec = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        return mouseVec;
    }
}
