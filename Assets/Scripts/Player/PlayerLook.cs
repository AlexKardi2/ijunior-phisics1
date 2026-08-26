using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _body;

    [SerializeField] private float _horizontalSensitivity = 0.5f;
    [SerializeField] private float _verticalSensitivity = 0.5f;

    [SerializeField] private float _verticalMinAngle = -89f;
    [SerializeField] private float _verticalMaxAngle = 89f;
    
    private float _pitch;

    public Vector3 CameraPosition => _cameraTransform.position;
    public Ray LookRay => new Ray(_cameraTransform.position, _cameraTransform.forward);
    public Vector3 Forward =>
        Vector3.ProjectOnPlane(
            _cameraTransform.forward,
            Vector3.up).normalized;

    public Vector3 Right =>
        Vector3.ProjectOnPlane(
            _cameraTransform.right,
            Vector3.up).normalized;

    private static float NormalizeAngle(float angle)
    {
        return angle > 180f ? angle - 360f : angle;
    }

    public void Look(Vector2 delta)
    {
        _pitch -= delta.y * _verticalSensitivity;

        _pitch = Mathf.Clamp(
            _pitch,
            _verticalMinAngle,
            _verticalMaxAngle);

        _cameraTransform.localEulerAngles = Vector3.right * _pitch;

        _body.Rotate(Vector3.up * delta.x * _horizontalSensitivity);
    }

    private void Awake()
    {
        _pitch = NormalizeAngle(_cameraTransform.localEulerAngles.x);
    }

    private void Start()
    {
        if (_cameraTransform == null || _body == null)
            Debug.LogError("Links are lost!", this);
    }
}
