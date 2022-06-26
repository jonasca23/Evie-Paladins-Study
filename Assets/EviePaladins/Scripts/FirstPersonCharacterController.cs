using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCharacterController : MonoBehaviour
{
    private Transform myCamera;

    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float cameraSens = 3;
    float xRotation;
    float yRotation;
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver{ get; private set; }
    [field: SerializeField] public InputHandler InputHandler{ get; private set; }

    private void OnEnable()
    {
        InputHandler.JumpEvent += Jump;
    }

    private void OnDisable()
    {
        InputHandler.JumpEvent -= Jump;
    }

    private void Awake()
    {
        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;
    }

    private void Start()
    {
        myCamera = Camera.main.transform;
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Vector3 forward = myCamera.forward * InputHandler.MovementValue.y;
        forward.y = 0;

        Vector3 right = myCamera.right * InputHandler.MovementValue.x;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Controller.Move((forward + right + ForceReceiver.ForceApplied) * Time.deltaTime * movementSpeed);
    }

    void Rotate()
    {
        float mouseX = InputHandler.MouseDelta.x * Time.deltaTime * cameraSens;
        float mouseY = InputHandler.MouseDelta.y * Time.deltaTime * cameraSens;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    void Jump()
    {
        ForceReceiver.AddForce(Vector3.up * jumpForce);
    }
}
