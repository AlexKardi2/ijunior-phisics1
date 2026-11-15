using UnityEngine;

public class WeaponPrefabFactory : MonoBehaviour
{
    [SerializeField] private WeaponPrefabs _weaponPrefabs;
    public Transform SplashPrefab => _weaponPrefabs.SplashPrefab;
    public Transform CreateDecal(Transform parent) =>
        Instantiate(_weaponPrefabs.DecalPrefab, parent);

    public Rigidbody CreateShell(Vector3 position, Quaternion rotation) =>
        Instantiate(_weaponPrefabs.ShellPrefab, position, rotation);


    private void OnValidate()
    {
        if (_weaponPrefabs == null)
            Debug.LogError("Weapon Prefabs scriptable object is not set", this);
    }

}
