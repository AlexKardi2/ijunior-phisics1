using UnityEngine;

public class WeaponPrefabs : ScriptableObject
{
    [SerializeField] private Rigidbody _shellPrefab;
    [SerializeField] private Transform _decalPrefab;
    [SerializeField] private Transform _splashPrefab;

    public Rigidbody ShellPrefab => _shellPrefab;
    public Transform DecalPrefab => _decalPrefab;
    public Transform SplashPrefab => _splashPrefab;

    private void OnValidate()
    {
        if (_splashPrefab == null)
            throw new System.NullReferenceException("Splash prefab link is not set");
    }
}
