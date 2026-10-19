using UnityEngine;

public class WaterSplasher : MonoBehaviour
{
    [SerializeField] Transform _prefub;
    
    private Transform _waterLevel;

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
        Gizmos.DrawSphere(_waterLevel.position, 0.1f);
    }

    private void TryCreateWaterSplash (Vector3 startPoint, Vector3 endPoint)
    {
        if (CheckWater(endPoint))
        {
            RaycastOnVirtualPlane(startPoint, endPoint - startPoint);
        }
    }

    public Vector3 RaycastOnVirtualPlane (Vector3 startPoint, Vector3 direction)
    {
        Plane plane = new Plane(Vector3.up, _waterLevel.position);
        Ray ray = new Ray(startPoint, direction);
        if (plane.Raycast(ray, out float enterDistance))
            return (startPoint + direction.normalized * enterDistance);
        return Vector3.zero;
    }
}
