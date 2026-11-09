using UnityEngine;

public class WaterSplasher : MonoBehaviour
{
    private Transform _waterLevel;

    private void Awake()
    {
        _waterLevel = transform;
    }

    private bool CheckWater(Vector3 input)
    {
        return input.y < _waterLevel.position.y;
    }

    private bool TryRaycastOnVirtualPlane(Vector3 startPoint, Vector3 direction, out Vector3 point)
    {
        Plane plane = new Plane(Vector3.up, _waterLevel.position);
        Ray ray = new Ray(startPoint, direction);
        if (plane.Raycast(ray, out float enterDistance))
        {
            point = startPoint + direction.normalized * enterDistance;
            return true;
        }
        point = Vector3.zero;
        return false;
    }

    public void TryCreateWaterSplash(Vector3 startPoint, Vector3 endPoint, Transform splashPrefab)
    {
        if (splashPrefab == null)
        {
            Debug.LogError("SplashPrefab is not set", this);
            return;
        }

        if (CheckWater(endPoint))
        {
            Vector3 point = RaycastOnVirtualPlane(startPoint, endPoint - startPoint);
            Destroy(Instantiate(_splashPrefab, point, _splashPrefab.rotation).gameObject, 5f);
        }
    }



    // ImpactEffects
    _waterSplasher.TryCreateWaterSplash(ray.origin, hit.point, _factory.SplashPrefab);
        _decalPlacer.Place(hit);

}
