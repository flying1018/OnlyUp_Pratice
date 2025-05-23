using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private float jumpPower = 120f;
    private Vector2 curMovementInput;
    [SerializeField] private LayerMask groundLayer;
    private Animator animator;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float loockSensitivity;
    private Vector2 mouseDelta;

    [SerializeField] private float findObjRayRange = 6f;
    [SerializeField] private LayerMask objLayer;


    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RaycastObj();
        IsGround();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * loockSensitivity;
        camCurXRot = Math.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * loockSensitivity, 0);
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        UIManager.Instance.item.SetActive(false);
        jumpPower = 150f;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled) { curMovementInput = Vector2.zero; }
    }

    public void OnLoock(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && GameManager.Instance.isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void IsGround()
    {
        GameManager.Instance.isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.7f, groundLayer);
        animator.SetBool("Grounded", GameManager.Instance.isGrounded);
    }

    private void RaycastObj()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, findObjRayRange, objLayer))
        {
            string target = hit.collider.gameObject.name;

            UIManager.Instance.ShowObjectInfo(target);

        }
    }
}
