using UnityEngine;

public class WaterSplasher : MonoBehaviour
{
    [SerializeField] private Transform _splashPrefab;
    
    private Transform _waterLevel;

    private void OnValidate ()
    {
        if (_splashPrefab == null)
            throw new System.NullReferenceException("Splash prefab link is not set");
    }
    private void Awake()
    {
        _waterLevel = transform;
    }

    private bool CheckWater (Vector3 input)
    {
        return input.y < _waterLevel.position.y;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
    private Vector3 RaycastOnVirtualPlane (Vector3 startPoint, Vector3 direction)
    {
        Plane plane = new Plane(Vector3.up, _waterLevel.position);
        Ray ray = new Ray(startPoint, direction);
        if (plane.Raycast(ray, out float enterDistance))
            return (startPoint + direction.normalized * enterDistance);
        return Vector3.zero;
    }

    public void TryCreateWaterSplash (Vector3 startPoint, Vector3 endPoint)
    {
        if (CheckWater(endPoint))
        {
            Vector3 point = RaycastOnVirtualPlane(startPoint, endPoint - startPoint);
            Destroy(Instantiate(_splashPrefab, point, _splashPrefab.rotation).gameObject, 5f);
        }
    }

}
