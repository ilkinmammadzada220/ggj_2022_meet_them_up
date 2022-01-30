using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject visual;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private bool canControllable;

    [Space] [Header("Normal Move Settings")] [SerializeField]
    private float normalMoveSpeed;

    [SerializeField] private float normalRotateSpeed, jumpSpeed;

    [Space] [Header("Carry Move Settings")] [SerializeField]
    private float carryMoveSpeed;

    [SerializeField] private float carryRotateSpeed;

    private bool _isCarryingCube = false, _canJump;

    private static MovementController _instance;

    private float _moveSpeed, _rotateSpeed;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _moveSpeed = normalMoveSpeed;
        _rotateSpeed = normalRotateSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
            canControllable = !canControllable;

        if (Input.GetKeyDown(KeyCode.Space))
            _canJump = true;
    }

    private void FixedUpdate()
    {
        if (!canControllable) return;

        MoveAndRotate();
        if (_canJump)
            Jump();
        
        rb.AddForce(Physics.gravity * (5 - 1) * rb.mass);
    }

    private void MoveAndRotate()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * _moveSpeed;
        dir = Vector3.ClampMagnitude(dir, _moveSpeed);

        if (dir != Vector3.zero)
        {
            rb.MovePosition(transform.position + dir * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(dir),
                _rotateSpeed * Time.fixedDeltaTime));
        }
    }

    private void Jump()
    {
        _canJump = false;

        if (groundChecker.IsGrounded && !IsCarryingCube)
        {
            float jumpForce = Mathf.Sqrt(jumpSpeed * -2 * (Physics.gravity.y * 1f));
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }

    public void LookAt(Vector3 target)
    {
        rb.rotation = Quaternion.LookRotation((target - rb.position).normalized);
    }

    public bool IsCarryingCube
    {
        get => _isCarryingCube;
        set
        {
            _isCarryingCube = value;

            if (_isCarryingCube)
                SetSpeeds(carryMoveSpeed, carryRotateSpeed);
            else
                SetSpeeds(normalMoveSpeed, normalRotateSpeed);
        }
    }

    private void SetSpeeds(float moveSpeed, float rotateSpeed)
    {
        _moveSpeed = moveSpeed;
        _rotateSpeed = rotateSpeed;
    }

    public static MovementController Instance => _instance;
}