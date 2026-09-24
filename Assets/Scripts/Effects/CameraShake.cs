using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour
{
    [Header("Noise shake")]
    [SerializeField] private float _perlinNoiseTimeScale = 1f;
    [SerializeField] private AnimationCurve _perlinNoiseAmplitudeCurve;

    [Header("Recoil")]
    [SerializeField] private float _maxImpulse = 10f;
    [SerializeField] private float _minImpulseMultipler = 0.5f;
    [SerializeField] private float _tension = 10f;
    [SerializeField] private float _damping = 10f;

    private Transform _cameraTransform;
    private Vector3 _shakeAngles = new Vector3();
    private Vector3 _recoilAngles = new Vector3();
    private Vector3 _recoilVelocity = new Vector3();
    private float _amplitude = 12f;
    private float _duration = 1f;
    private float _shakeTimer = -1f;

    private void Awake()
    {
        _cameraTransform = transform;
    }

    private void Update()
    {
        UpdateShake();
        UpdateRecoil();

        _cameraTransform.localEulerAngles = _shakeAngles + _recoilAngles;
    }

    private void UpdateRecoil()
    {
        _recoilAngles += _recoilVelocity * Time.deltaTime;
        _recoilVelocity -= _recoilAngles * Time.deltaTime * _tension;
        _recoilVelocity = Vector3.Lerp(_recoilVelocity, Vector3.zero, Time.deltaTime * _damping);
    }
    private void UpdateShake()
    {
        if (_shakeTimer > 0)
            _shakeTimer -= Time.deltaTime / _duration;

        float time = Time.time * _perlinNoiseTimeScale;
        _shakeAngles.x = Mathf.PerlinNoise(time, 0);
        _shakeAngles.y = Mathf.PerlinNoise(0, time);
        _shakeAngles.z = Mathf.PerlinNoise(time, time);

        _shakeAngles *= _amplitude;
        _shakeAngles *= _perlinNoiseAmplitudeCurve.Evaluate(Mathf.Clamp01(1 - _shakeTimer));
    }

    [ProPlayButton]
    public void MakeShake() =>
        MakeShake(15f, 2f);

    public void MakeShake(float amplitude, float duration)
    {
        _amplitude = amplitude;
        _duration = Mathf.Max(duration, 0.05f);
        _shakeTimer = 1;
    }

    [ProPlayButton]
    public void MakeRecoil() =>
        MakeRecoil(-Vector3.right * Random.Range(_maxImpulse * _minImpulseMultipler, _maxImpulse)+Vector3.up * Random.Range(-_maxImpulse , _maxImpulse) * _minImpulseMultipler);
    public void MakeRecoil(Vector3 impulse)
    {
        _recoilVelocity += impulse;

    }
}