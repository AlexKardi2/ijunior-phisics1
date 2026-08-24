using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public event Action OnShootPerformed;
    private ShooterActions _input;
    private InputAction _movement;
    private InputAction _looking;
    private InputAction _jump;
    private bool _jumpRequested;

    private void Awake()
    {
        _input = new ShooterActions();
        _input.ShooterPlayer.Enable();
        _movement = _input.ShooterPlayer.Move;
        _looking = _input.ShooterPlayer.LookDelta;
        _jump = _input.ShooterPlayer.Jump;
    }

    public Vector3 Movement =>
        _movement.ReadValue<Vector3>();

    public Vector2 Look =>
        _looking.ReadValue<Vector2>();

    private void OnEnable()
    {
        _jump.performed += OnJump;
        _input.ShooterPlayer.Shoot.performed += OnShoot;
    }

    private void OnDisable()
    {
        _jump.performed -= OnJump;
        _input.ShooterPlayer.Shoot.performed -= OnShoot;
    }

    public bool ConsumeJump()
    {
        if (!_jumpRequested)
            return false;

        _jumpRequested = false;
        return true;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        OnShootPerformed?.Invoke();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _jumpRequested = true;
    }

    private void OnDestroy()
    {
        _input.ShooterPlayer.Disable();
    }
}
