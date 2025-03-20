using UnityEngine;

public class FPSController : MonoBehaviour {
    public int FPS = 120;
    public float speed = 6.0F;
    public float jumpSpeed = 20.0F;
    public float gravity = 20.0F;
    public bool mouseOff = true;
    
    private Vector3 moveDirection = Vector3.zero;
    

    float yRotation;
    float xRotation;
    float lookSensitivity = 2;
    float currentXRotation;
    float currentYRotation;
    float yRotationV;
    float xRotationV;
    float lookSmoothness = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Application.targetFrameRate = FPS;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // probably change that so that we see a cursor
    }

    // Update is called once per frame
    void Update() {
        
            CharacterController controller = GetComponent<CharacterController>();
            if (controller.isGrounded) {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Input.GetButton("Jump")) {
                    moveDirection.y = jumpSpeed;
                }
            }
            if (mouseOff) {
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

            yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
            xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 100f);
            currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothness);
            currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothness);
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }

        if (Input.GetKey(KeyCode.Tab)) {
            MouseEnabler();
        }

    }

    public void MouseEnabler() {
        if (mouseOff) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            mouseOff = false;
        } else if (mouseOff != true) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
            mouseOff = true;
        }
        
    }
}