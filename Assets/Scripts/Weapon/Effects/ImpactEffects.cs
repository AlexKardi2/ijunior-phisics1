using System;
using UnityEngine;

[RequireComponent(typeof(DecalPlacer))]
[RequireComponent(typeof(WeaponPrefabFactory))]

public class ImpactEffects : MonoBehaviour
{
    [SerializeField] private WaterSplasher _waterSplasher;
    private DecalPlacer _decalPlacer;
    private WeaponPrefabFactory _factory;

    public void Handle(Ray shootRay, RaycastHit hit)
    {
        _decalPlacer.Place(hit);
        _waterSplasher.TryCreateWaterSplash(shootRay.origin, hit.point, _factory.SplashPrefab);
    }

    private void Awake()
    {
        _decalPlacer = GetComponent<DecalPlacer>();
        _factory = GetComponent<WeaponPrefabFactory>();
    }
    private void OnValidate()
    {
        if (_waterSplasher == null)
            Debug.LogError("Water splasher ling is not set", this);
    }
}
