using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _speed = 8;
    [SerializeField] private float _strafeSpeed = 7;
    [SerializeField] private float _jumpForce = 7;
    [SerializeField] private float _gravityFactor = 1.7f;
    [SerializeField] private float _horizontalMouseSensitivity = 0.5f;
    [SerializeField] private float _verticalMouseSensitivity = 0.5f;
    [SerializeField] private float _verticalMinAngle = -89f;
    [SerializeField] private float _verticalMaxAngle = 89f;
    private Transform _transform;
    private CharacterController _characterController;
    private InputAction _movementAction;
    private InputAction _mouseDeltaAction;
    private float _cameraAngle = 0;
    private bool _isJumpCalled = false;

    private bool MustJump
    {
        get
        {
            if (!_isJumpCalled)
                return false;
            _isJumpCalled = false;
            return true;
        }
    }

    private void Awake()
    {
        _transform = transform;
        if (_cameraTransform == null)
            throw new NullReferenceException();
        _characterController = GetComponent<CharacterController>();
        _movementAction = InputSystem.actions.FindAction("Movement");
        _mouseDeltaAction = InputSystem.actions.FindAction("Looking");
        _cameraAngle = _cameraTransform.localEulerAngles.x;
    }

    private void OnEnable()
    {
        InputAction jump = InputSystem.actions.FindAction("Jump");
        if (jump != null)
            jump.performed += OnJump;
    }
    private void OnDisable()
    {
        InputAction jump = InputSystem.actions.FindAction("Jump");
        if (jump != null)
            jump.performed -= OnJump;
    }

    private void Update()
    {
        Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.TransformDirection(0, 0, 1), Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

        _cameraAngle -= _mouseDeltaAction.ReadValue<Vector2>().y * _verticalMouseSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
        _cameraTransform.localEulerAngles = Vector2.right * _cameraAngle;

        _transform.Rotate(Vector3.up * _horizontalMouseSensitivity * _mouseDeltaAction.ReadValue<Vector2>().x);

        if (_characterController == null)
            return;
        Vector3 input = _movementAction.ReadValue<Vector3>();
        Vector3 movement = forward * input.z * _speed + right * input.x * _strafeSpeed;

        if (_characterController.isGrounded)
        {
            if (MustJump)
                movement += Vector3.up * _jumpForce;
            else
                movement += Physics.gravity;
            _characterController.Move(movement * Time.deltaTime);
        }
        else
        {
            _characterController.Move((_characterController.velocity + Physics.gravity * Time.deltaTime * _gravityFactor) * Time.deltaTime);
        }
    }

    private void OnJump(InputAction.CallbackContext c)
    {
        if (_characterController == null)
            return;
        _isJumpCalled = _characterController.isGrounded;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        CharacterController character = GetComponent<CharacterController>();
        Gizmos.DrawWireCube(transform.position, Vector3.right + Vector3.forward + Vector3.up * character.height);
    }
}
