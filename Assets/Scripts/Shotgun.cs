using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private float _impactForce = 10f;
    [SerializeField] LayerMask _layerMask;
    public void Shoot(Ray shootRay)
    {
        if (Physics.Raycast(shootRay, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            AbstractHealth health = hitInfo.collider.GetComponentInParent<AbstractHealth>();
            health?.TakeDamage(_damage);

            Rigidbody targetBody = hitInfo.rigidbody;
            if (targetBody != null)
            {
                targetBody.AddForceAtPosition(shootRay.direction * _impactForce, hitInfo.point);
            }
        }

    }

    public void Shoot(Vector3 startPoint, Vector3 direction) => Shoot(new Ray(startPoint, direction));
}
