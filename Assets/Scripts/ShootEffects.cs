using System.Collections;
using UnityEngine;

public class ShootEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _lightSource;

    private void Start()
    {
        if (_shootSound == null)
            throw new System.NullReferenceException("Audio source is not set");
        if (_particleSystem == null)
            throw new System.NullReferenceException("Flash particles is not set");
        if (_lightSource == null)
            throw new System.NullReferenceException("Light game object is not set");
    }

    public void Perform()
    {
        StartCoroutine(EffectRoutine());
    }

    private IEnumerator EffectRoutine()
    {
        _shootSound.Play();
        _lightSource.SetActive(true);
        _particleSystem.Clear();
        _particleSystem.Play();
        
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        _lightSource.SetActive(false);
    }
}
