using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private float respawnDelay;
    [SerializeField] private float detachDelay;

    private Rigidbody2D currentBallRigidbody;
    private SpringJoint2D currentBallSpringJoint;
    private Camera mainCamera;

    private bool isDragging;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        SpawnNewBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBallRigidbody == null)
        {
            return;
         }

        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isDragging)
            {
                LauncheBall();
            }
             
            isDragging = false;
            
            return;
        }

        isDragging = true;
        currentBallRigidbody.isKinematic = true;

        Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(touchPos);

        currentBallRigidbody.position = worldPos;

    }

    private void SpawnNewBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);

        currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();

        currentBallSpringJoint.connectedBody = pivot;
    }

    private void LauncheBall()
    {
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;

        Invoke(nameof(DetachBall), detachDelay);
    }

    private void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;

        Invoke(nameof(SpawnNewBall), respawnDelay);
    }
}
