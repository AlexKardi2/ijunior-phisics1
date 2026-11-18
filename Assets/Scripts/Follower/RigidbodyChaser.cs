
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class RigidbodyChaser : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _target;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 4.5f;
    [SerializeField] private float _stopDistance = 1.5f;
    [SerializeField] private float _rotationSpeed = 540f;

    [Header("Step climbing")]
    [SerializeField] private float _stepHeight = 0.3f;
    [SerializeField] private float _stepProbeExtraDistance = 0.15f;
    [SerializeField] private float _stepSkin = 0.02f;

    [Header("Physics")]
    [SerializeField] private LayerMask _environmentMask = ~0;
    [SerializeField, Range(0f, 89f)] private float _maxGroundAngle = 45f;

    private Rigidbody _rb;
    private CapsuleCollider _capsule;

    private float _lastGroundedTime = float.NegativeInfinity;

    private const float GroundedGracePeriod = 0.15f;
    private const float ProbeHeight = 0.08f;
    private const float ProbeAboveStep = 0.05f;

    private bool IsGrounded =>
        Time.fixedTime - _lastGroundedTime <= GroundedGracePeriod;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _capsule = GetComponent<CapsuleCollider>();

        // Запрещаем заваливание персонажа на бок.
        _rb.constraints |= RigidbodyConstraints.FreezeRotationX;
        _rb.constraints |= RigidbodyConstraints.FreezeRotationZ;

        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        if (_capsule.direction != 1)
            Debug.LogWarning("CapsuleCollider должен иметь Direction = Y.", this);
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            SetHorizontalVelocity(Vector3.zero);
            return;
        }

        // Направление от бота к игроку.
        Vector3 direction = _target.position - _rb.position;

        // Не учитываем разницу высот.
        direction.y = 0f;

        float distance = direction.magnitude;

        Vector3 movementDirection = Vector3.zero;

        // Преследуем игрока, пока он не вошёл в дистанцию остановки.
        if (distance > _stopDistance && distance > 0.001f)
        {
            movementDirection = direction.normalized;

            if (IsGrounded)
                TryStepUp(movementDirection);

            RotateTowards(movementDirection);
        }

        SetHorizontalVelocity(movementDirection);
    }

    private void SetHorizontalVelocity(Vector3 direction)
    {
        // Сохраняем вертикальную скорость, чтобы гравитация
        // продолжала работать независимо от движения по X и Z.
        Vector3 velocity = _rb.linearVelocity;

        velocity.x = direction.x * _moveSpeed;
        velocity.z = direction.z * _moveSpeed;

        _rb.linearVelocity = velocity;
    }

    private void RotateTowards(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction, Vector3.up);

        Quaternion newRotation = Quaternion.RotateTowards(
            _rb.rotation,
            targetRotation,
            _rotationSpeed * Time.fixedDeltaTime);

        _rb.MoveRotation(newRotation);
    }

    private void TryStepUp(Vector3 direction)
    {
        Vector3 feetPosition = GetFeetPosition();

        float radius = GetWorldRadius();

        // Проверяем препятствие немного дальше передней части бота.
        float maxProbeDistance = radius + _stepProbeExtraDistance;

        Vector3 lowOrigin =
            feetPosition + Vector3.up * ProbeHeight;

        if (!Physics.Raycast(
            lowOrigin,
            direction,
            out RaycastHit wallHit,
            maxProbeDistance,
            _environmentMask,
            QueryTriggerInteraction.Ignore))
        {
            return;
        }

        // Не пытаемся подниматься по наклонной поверхности.
        // Этот механизм предназначен для вертикальных ступенек.
        if (wallHit.normal.y > 0.2f)
            return;

        float forwardDistance = wallHit.distance + 0.05f;

        // Проверяем, свободно ли пространство над препятствием.
        Vector3 highOrigin =
            lowOrigin + Vector3.up * (_stepHeight + ProbeAboveStep - ProbeHeight);

        if (Physics.Raycast(
            highOrigin,
            direction,
            forwardDistance,
            _environmentMask,
            QueryTriggerInteraction.Ignore))
        {
            return;
        }

        // Ищем верхнюю поверхность ступеньки.
        Vector3 topOrigin =
            feetPosition +
            direction * forwardDistance +
            Vector3.up * (_stepHeight + ProbeAboveStep);

        if (!Physics.Raycast(
            topOrigin,
            Vector3.down,
            out RaycastHit topHit,
            _stepHeight + ProbeAboveStep + 0.2f,
            _environmentMask,
            QueryTriggerInteraction.Ignore))
        {
            return;
        }

        float stepUp = topHit.point.y - feetPosition.y;

        // Игнорируем слишком маленькие и слишком высокие препятствия.
        if (stepUp <= 0.01f || stepUp > _stepHeight)
            return;

        // Проверяем, поместится ли бот над ступенькой.
        Vector3 newPosition =
            _rb.position + Vector3.up * (stepUp + _stepSkin);

        if (!HasClearanceAt(newPosition))
            return;

        // Поднимаем Rigidbody на высоту ступеньки.
        _rb.position = newPosition;

        Vector3 velocity = _rb.linearVelocity;

        if (velocity.y < 0f)
            velocity.y = 0f;

        _rb.linearVelocity = velocity;

        _lastGroundedTime = Time.fixedTime;
    }

    private Vector3 GetFeetPosition()
    {
        Vector3 scale = transform.lossyScale;

        float radius = GetWorldRadius();

        float height = Mathf.Max(
            _capsule.height * Mathf.Abs(scale.y),
            radius * 2f);

        Vector3 center = transform.TransformPoint(_capsule.center);

        return center - transform.up * (height * 0.5f);
    }

    private float GetWorldRadius()
    {
        Vector3 scale = transform.lossyScale;

        return _capsule.radius *
               Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.z));
    }

    private bool HasClearanceAt(Vector3 proposedPosition)
    {
        Vector3 scale = transform.lossyScale;

        float radius = GetWorldRadius();

        float height = Mathf.Max(
            _capsule.height * Mathf.Abs(scale.y),
            radius * 2f);

        float halfSegment = height * 0.5f - radius;

        Vector3 center =
            transform.TransformPoint(_capsule.center) +
            (proposedPosition - _rb.position);

        Vector3 point1 = center + transform.up * halfSegment;
        Vector3 point2 = center - transform.up * halfSegment;

        Collider[] overlaps = Physics.OverlapCapsule(
            point1,
            point2,
            radius * 0.98f,
            _environmentMask,
            QueryTriggerInteraction.Ignore);

        foreach (Collider overlap in overlaps)
        {
            // Не учитываем собственные коллайдеры бота.
            if (overlap == _capsule || overlap.attachedRigidbody == _rb)
                continue;

            return false;
        }

        return true;
    }

    private void OnCollisionStay(Collision collision)
    {
        // Определяем, стоит ли бот на земле или наклонной поверхности.
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint contact = collision.GetContact(i);

            float angle = Vector3.Angle(
                contact.normal,
                Vector3.up);

            if (angle <= _maxGroundAngle)
            {
                _lastGroundedTime = Time.fixedTime;
                return;
            }
        }
    }
}
