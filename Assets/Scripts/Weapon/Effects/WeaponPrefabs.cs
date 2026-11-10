using UnityEngine;

[CreateAssetMenu(fileName = "_ prefabs", menuName = "Scriptable objects/Weapon prefabs")]
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
        if (_shellPrefab == null)
            Debug.LogError("Shell prefab link is not set", this);
        if (_decalPrefab == null)
            Debug.LogError("Decal prefab link is not set", this);
        if (_splashPrefab == null)
            Debug.LogError("Splash prefab link is not set", this);
    }
}
