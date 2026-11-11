using UnityEngine;

public class WeaponPrefabFactory : MonoBehaviour
{
    [SerializeField] private WeaponPrefabs _weaponPrefabs;

    private void OnValidate()
    {
        if (_weaponPrefabs == null)
            Debug.LogError("Weapon Prefabs scriptable object is not set", this);
    }

    public Transform CreateDecal(Transform parent) =>
        Instantiate(_weaponPrefabs.DecalPrefab, parent);

    public Rigidbody CreateShell(Vector3 position, Quaternion rotation) =>
        Instantiate(_weaponPrefabs.ShellPrefab, position, rotation);

    public Transform CreateSplash(Vector3 position) =>
        Instantiate(_weaponPrefabs.SplashPrefab, position, _weaponPrefabs.SplashPrefab.rotation);
}
