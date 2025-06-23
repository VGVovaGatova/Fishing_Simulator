using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] int Speed;
    [SerializeField] int JumpForce;
    [SerializeField] Transform CheckPostion;
    [SerializeField] float HeatingGrounded;
    [SerializeField] bool IsGrounded;
    [SerializeField] LayerMask WhatIsLayerGround;
    [SerializeField] GameObject Camera;
    public float SpeedCamera;
    private float Horizontal;
    private float Vertical;
    private float mouseY;
    private float mouseX;
    Vector3 Direction;
    private Rigidbody rb;
    public bool CanRotationCamera = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        IsGrounded = Physics.CheckSphere(CheckPostion.position, HeatingGrounded, WhatIsLayerGround);
        if (IsGrounded)
        rb.drag = 6;
        if (!IsGrounded)
        rb.drag = 1;
        if (CanRotationCamera)
        {
            mouseX += Input.GetAxis("Mouse X") * SpeedCamera;
            mouseY -= Input.GetAxis("Mouse Y") * SpeedCamera;
            mouseY = Mathf.Clamp(mouseY, -90, 90);
            Camera.transform.localRotation = Quaternion.Euler(mouseY, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, mouseX, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        PlayerPrefs.DeleteAll();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        }
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        MoventSystem();
    }
    void MoventSystem()
    {
        Vector3 direction = new Vector3(Horizontal, 0f, Vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            Vector3 move = transform.TransformDirection(direction) * Speed * Time.deltaTime;
            rb.MovePosition(rb.position + move);
        }
    }
}
