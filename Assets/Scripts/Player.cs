using System;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO выделить управление игрока в отдельный слой,  

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _speed = 1;
    private Transform _transform;
    private CharacterController _characterController;
    private InputAction _movementAction;

    private void Awake()
    {
        _transform = transform;
        if (_cameraTransform == null)
            throw new NullReferenceException();
        _characterController = GetComponent<CharacterController>();
        _movementAction = InputSystem.actions.FindAction("Movement");
    }

    private void Update()
    {
        Vector3 forward = Vector3.ProjectOnPlane( _cameraTransform.forward, Vector3.up);
        forward.Normalize();
        
        if (_characterController == null)
            return;
        Vector3 input = _movementAction.ReadValue<Vector3>();
        Vector3 movement = new Vector3(forward.x * input.x, 0, forward.z * input.z);
        movement *= _speed * Time.deltaTime;
        


        if (_characterController.isGrounded)
        {
            _characterController.Move(input + Vector3.down);
        }
        else
        {
            _characterController.Move(_characterController.velocity + Physics.gravity * Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Bath")
            return;
        hit.rigidbody.linearVelocity = Vector3.up * 100f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        CharacterController character = GetComponent<CharacterController>();
        Gizmos.DrawWireCube(transform.position, Vector3.right + Vector3.forward + Vector3.up * character.height);
    }
}
