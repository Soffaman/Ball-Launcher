using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            return;
        }
        Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(touchPos);

        Debug.Log(worldPos);

    }
}
