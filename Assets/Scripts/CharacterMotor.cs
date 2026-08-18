using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMotor : MonoBehaviour
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
                velocity += Vector3.up * _jumpSpeed;
            else
                velocity += Vector3.down * Mathf.Max(_strafeSpeed, _speed);
        }
        else
        {
            velocity = _characterController.velocity + Physics.gravity * Time.deltaTime * _gravityFactor;
        }

        _characterController.Move(velocity * Time.deltaTime);
    }
}
