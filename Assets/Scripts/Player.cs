using UnityEngine;
using UnityEngine.InputSystem;

//TODO выделить управление игрока в отдельный слой,  

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    private Transform _transform;
    private CharacterController _characterController;
    private InputAction _movementAction;

    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        _movementAction = InputSystem.actions.FindAction("Movement");
    }

    private void Update()
    {
        if (_characterController == null)
            return;
        Vector3 playerSpeed = _movementAction.ReadValue<Vector3>();
        playerSpeed *= _speed * Time.deltaTime; 

        if (_characterController.isGrounded)
        {
            _characterController.Move(playerSpeed + Vector3.down);
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
