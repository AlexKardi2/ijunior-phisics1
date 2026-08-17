using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _strafeSpeed = 7f;
    [SerializeField] private float _jumpSpeed = 7f;
    [SerializeField] private float _gravityFactor = 1.7f;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(
        Vector3 input,
        Vector3 forward,
        Vector3 right,
        bool jump)
    {
        Vector3 velocity = forward * input.z * _speed + right * input.x * _strafeSpeed;

        if (_characterController.isGrounded)
        {
            if (jump)
                _verticalSpeed = _jumpSpeed;
            else
                _verticalSpeed = Physics.gravity.y;
        }
        else
        {
            _verticalSpeed +=
                Physics.gravity.y * _gravityFactor * Time.deltaTime;
        }

        velocity.y = _verticalSpeed;

        _characterController.Move(
            velocity * Time.deltaTime);
    }
}
}
