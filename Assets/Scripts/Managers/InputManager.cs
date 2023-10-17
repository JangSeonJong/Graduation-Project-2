using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    static public Action<Define.MouseAction> MouseAction = null;
    static public Action<Define.KeyAction> KeyAction = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            KeyAction.Invoke(Define.KeyAction.E);

        if (Input.GetKeyDown(KeyCode.M))
            KeyAction.Invoke(Define.KeyAction.M);

        if (Input.GetKeyDown(KeyCode.I))
            KeyAction.Invoke(Define.KeyAction.I);

        if (Input.GetKeyDown(KeyCode.Escape))
            KeyAction.Invoke(Define.KeyAction.Escape);

        if (Input.GetMouseButtonDown(0))
            MouseAction.Invoke(Define.MouseAction.LeftClick);

        if (Input.GetMouseButton(0))
            MouseAction.Invoke(Define.MouseAction.LeftPress);

        if (Input.GetMouseButtonUp(0))
            MouseAction.Invoke(Define.MouseAction.LeftButtonUp);

        if (Input.GetMouseButtonDown(1))
            MouseAction.Invoke(Define.MouseAction.RightClick);

        if (Input.GetMouseButton(1))
            MouseAction.Invoke(Define.MouseAction.RightPress);

        if (Input.GetMouseButtonUp(1))
            MouseAction.Invoke(Define.MouseAction.RightButtonUp);

    }
}
