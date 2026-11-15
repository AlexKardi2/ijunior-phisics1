using UnityEngine;

[RequireComponent(typeof(ImpactEffects))]
public class HitscanShooter : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private float _impactForce = 10f;
    [SerializeField] private LayerMask _layerMask;
    private ImpactEffects _impactEffects;
    public void Shoot(Ray shootRay)
    {
        if (Physics.Raycast(shootRay, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {

            _impactEffects.Handle(shootRay, hitInfo);
            AbstractHealth health = hitInfo.collider.GetComponentInParent<AbstractHealth>();
            health?.TakeDamage(_damage);

            Rigidbody targetBody = hitInfo.rigidbody;
            targetBody?.AddForceAtPosition(shootRay.direction * _impactForce, hitInfo.point);

        }
    }

}
