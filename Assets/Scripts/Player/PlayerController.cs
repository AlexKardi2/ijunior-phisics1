using UnityEngine;

[RequireComponent(typeof(IPlayerInput))]
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(PlayerLook))]
public class PlayerController : MonoBehaviour
{
    private IPlayerInput _input;
    private CharacterMotor _motor;
    private PlayerLook _look;

    private void Awake()
    {
        _input = GetComponent<IPlayerInput>();
        _motor = GetComponent<CharacterMotor>();
        _look = GetComponent<PlayerLook>();
    }

    private void Update()
    {
        _look.Look(_input.Look);

        _motor.Move(
            _input.Movement,
            _look.Forward,
            _look.Right,
            _input.ConsumeJump());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        CharacterController character = GetComponent<CharacterController>();
        Gizmos.DrawWireCube(transform.position, Vector3.right + Vector3.forward + Vector3.up * character.height);
    }
}
