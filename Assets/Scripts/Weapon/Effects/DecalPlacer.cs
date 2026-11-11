using System;
using UnityEditor.PackageManager;
using UnityEngine;

[RequireComponent(typeof(WeaponPrefabFactory))]
public class DecalPlacer : MonoBehaviour
{
    [SerializeField] private float _offset = 0.01f; 
    private WeaponPrefabFactory _factory;

    public void Place (RaycastHit hit)
    {
        Transform decal = _factory.CreateDecal(hit.transform);
        decal.position = hit.point + hit.normal * _offset;
        decal.LookAt(hit.point);
        decal.Rotate(180f, 0f, 0f, Space.Self);
        decal.localScale = InvertChildScale(decal.localScale, hit.transform.lossyScale);
    }
    private void Awake()
    {
        _factory = GetComponent<WeaponPrefabFactory>();
    }
    private Vector3 InvertChildScale(Vector3 childScale, Vector3 parentScale)
    {
        childScale.x /= parentScale.x;
        childScale.y /= parentScale.y;
        childScale.z /= parentScale.z;
        return childScale;
    }
}
