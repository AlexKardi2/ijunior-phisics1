using UnityEngine;
using UnityEngine.InputSystem;

public class RigidbodyGun : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbodyPrefab;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _speed = 10f;

    private void Start()
    {


    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed == false)
            return;
        Rigidbody projectile = Instantiate(_rigidbodyPrefab, _startPosition.position, _rigidbodyPrefab.rotation);
        Vector3 forward = _startPosition.forward;
        projectile.linearVelocity = forward * _speed;
    }

}
