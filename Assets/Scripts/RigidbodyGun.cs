using UnityEngine;
using UnityEngine.InputSystem;

public class RigidbodyGun : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbodyPrefab;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _speed = 10f;
    private float _timer = 1f;
    private int _shoots = 15;
    InputAction shootAction;

    private void Start()
    {
        print("Shoot action have found is " + shootAction != null);
        shootAction = InputSystem.actions.FindAction("Shoot");
        print("Shoot action have found is " + shootAction != null);
    }
    private void Update()
    {
        if (shootAction.IsPressed())
        {
            Shoot();
            _timer += 3f;
            _shoots--;
        }
        else
            _timer -= Time.deltaTime;
    }

    private void Shoot()
    {
        Rigidbody projectile = Instantiate(_rigidbodyPrefab, _startPosition.position, _rigidbodyPrefab.rotation);
        Vector3 forward = _startPosition.forward;
        projectile.linearVelocity = forward * _speed;
    }
}
