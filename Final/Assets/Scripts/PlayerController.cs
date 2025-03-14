using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    private CharacterController characterController;

    [Header("Movement Settings")]
    public float Speed = 5f;

    [Header("Look Settings")]
    public float lookSpeed = 3f;

    //  vertical camera rotation tracker
    private float verticalRotation = 0f;
    
    // child camera reference
    private Camera playerCamera;

    void Start() {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        // Lock the mouse cursor to the center of the screen & hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        // ------------------------------------
        // 1) MOVEMENT
        // ------------------------------------
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        // Move in the local 'right'/'forward' directions of this transform
        Vector3 move = (transform.right * moveX) + (transform.forward * moveZ);
        move *= Speed;

        // CharacterController handles collisions
        characterController.Move(move * Time.deltaTime);

        // ------------------------------------
        // 2) MOUSE LOOK
        // ------------------------------------
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate the entire player GameObject around the Y axis (turn left/right)
        transform.Rotate(Vector3.up, mouseX);

        // Tilt the camera up/down
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        if (playerCamera != null) {
            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
}