using UnityEngine;

[RequireComponent(typeof(WeaponPrefabFactory))]
public class ShellEjector : MonoBehaviour
{
    [SerializeField] private Transform _shellPoint;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _angularRange = 5f;
    private WeaponPrefabFactory _factory;
    public void Eject()
    {
        Rigidbody shell = _factory.CreateShell( _shellPoint.position, _shellPoint.rotation);
        shell.linearVelocity = _shellPoint.forward * _speed;
        shell.angularVelocity = Vector3.up * (Random.Range(-_angularRange, _angularRange));
    }

    private void Awake()
    {
        _factory = GetComponent<WeaponPrefabFactory>();
    }

}
