using System.Collections;
using UnityEngine;

public class MuzzleEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _lightSource;

    private void OnValidate()
    {
        if (_particleSystem == null)
            Debug.LogError("Flash particles is not set", this);
        if (_lightSource == null)
            Debug.LogError("Light game object is not set",this);
    }

    public void Perform()
    {
        StartCoroutine(EffectRoutine());
    }

    private IEnumerator EffectRoutine()
    {
        _lightSource.SetActive(true);
        _particleSystem.Clear();
        _particleSystem.Play();
        
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        _lightSource.SetActive(false);
    }
}
