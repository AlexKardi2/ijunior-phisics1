using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] LayerMask _layerMask;
    public void Shoot(Vector3 startPoint, Vector3 direction)
    {
        Debug.Log("Shooting from " + startPoint + " to " + direction);
        if (Physics.Raycast(startPoint, direction, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {

        }

    }
}
