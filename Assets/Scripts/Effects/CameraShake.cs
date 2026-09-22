using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _perlinNoiseTimeScale = 1f;
    [SerializeField] private AnimationCurve _perlinNoiseAmplitudeCurve;
    private Transform _cameraTransform;
    private Vector3 _shakeAngles = new Vector3();
    private Vector3 _recoilAngles = new Vector3();
    private Vector3 _recoilVelocity = new Vector3();
    private float _amplitude = 5f;
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

    [ContextMenu("MakeShake")]
    public void MakeShake() =>
        MakeShake(15, 3);

    public void MakeShake(float amplitude, float duration)
    {
        _amplitude = amplitude;
        _duration = Mathf.Max(duration, 0.05f);
        _shakeTimer = 1;
    }
    public void MakeRecoil(Vector3 forces, float duration)
    {

    }
}