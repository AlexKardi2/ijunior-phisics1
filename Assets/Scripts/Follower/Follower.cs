using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Follower : MonoBehaviour
{
    [Header ("Dependencies")]
    [SerializeField] private Animator _animator;

    [Header("Targeting")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _distance = 2f;

    [Header ("Phisics")]
    [SerializeField] private float _speed = 3f;

    private Rigidbody _body;
    private Transform _transform;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }
    private void Update()
    {
        if (_target == null)
            return;
        
        Vector3 lookPoint = _target.position;
        lookPoint.y = _transform.position.y;
        _transform.LookAt(lookPoint);

        Vector3 velocity;
        if (Vector3.Distance(_transform.position, _target.position) > _distance)
        {
            velocity = _transform.forward * _speed;
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            velocity = Vector3.zero;
            _animator.SetBool("IsWalking", false);
        }
        velocity.y = _body.linearVelocity.y;
        _body.linearVelocity = velocity;
    }

    private void OnValidate()
    {
        if (_animator == null)
            Debug.LogError("Animator is not set", this);
    }
}
