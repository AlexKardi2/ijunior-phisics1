using System.Threading;
using UnityEngine;

public class RigidbodyGun : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbodyPrefab;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _speed = 10f;
    private float _timer = 1f;
    private int _shoots = 15;

    private void Update()
    {
        if (_timer < 0)
        {
            _timer += 3f;
            _shoots--;
            Rigidbody projectile = Instantiate(_rigidbodyPrefab, _startPosition.position, _rigidbodyPrefab.rotation);
            Vector3 forward = _startPosition.forward;
            projectile.linearVelocity = forward * _speed;
        }
        else
            _timer -= Time.deltaTime;
    }
}
