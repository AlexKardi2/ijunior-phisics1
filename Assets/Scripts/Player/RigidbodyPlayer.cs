using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    private Rigidbody _rigidbody;
    private InputAction _movementAction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movementAction = InputSystem.actions.FindAction("Movement");
    }

    private void Update()
    {
        if (_rigidbody == null)
            return;
        Vector3 playerSpeed = _movementAction.ReadValue<Vector3>();
        playerSpeed *= _speed;
        playerSpeed.y = _rigidbody.linearVelocity.y;

        //if (playerSpeed != Vector3.zero)
        _rigidbody.linearVelocity = playerSpeed;
    }
}
