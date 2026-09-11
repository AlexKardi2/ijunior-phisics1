using UnityEngine;

public class Shotgun : MonoBehaviour
{
    //Ref Создание префабов отделить в отдельный класс и вызывать его из оружия
    //Ref Разделить скрипт на зоны ответственности. отдельно создание эффектов, отдельно нанесение урона, нанесение урона при помощи рейкастов - отдельным скриптом, т.е. оружие должно только вызывать в скрипте необходимость послать рейкаст
    //Ref Для звуков создать отдельный скрипт, это было бы логичнее
    [SerializeField] private ShootEffects _shotEffects;
    [SerializeField] private Transform _decalPrefab;
    [SerializeField] private float _decalOffset=0.1f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private float _impactForce = 10f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        if (_decalPrefab == null)
            throw new System.NullReferenceException("Decal prefab is not set");
        if (_shotEffects == null)
            throw new System.NullReferenceException("Shooter effects is not set");
        if (_animator == null)
            throw new System.NullReferenceException("Animator is not set");
    }
    public void Shoot(Ray shootRay)
    {
        _shotEffects.Perform();
        _animator.SetTrigger("Shoot");

        if (Physics.Raycast(shootRay, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            Transform decal = Instantiate(_decalPrefab, hitInfo.transform);
            decal.position = hitInfo.point + hitInfo.normal * _decalOffset;
            decal.LookAt(hitInfo.point);
            decal.Rotate(180f, 0f, 0f, Space.Self);
            
            AbstractHealth health = hitInfo.collider.GetComponentInParent<AbstractHealth>();
            health?.TakeDamage(_damage);

            Rigidbody targetBody = hitInfo.rigidbody;
            targetBody?.AddForceAtPosition(shootRay.direction * _impactForce, hitInfo.point);

        }

    }

    public void Shoot(Vector3 startPoint, Vector3 direction) => Shoot(new Ray(startPoint, direction));
}
