using System;
using UnityEngine;

[RequireComponent(typeof(IPlayerInput))]
[RequireComponent(typeof(PlayerLook))]
public class Player : MonoBehaviour
{
    [SerializeField] Shotgun _shotgun;
    private IPlayerInput _input;
    private PlayerLook _look;

    private void Awake()
    {
        _input = GetComponent<IPlayerInput>();
        _look = GetComponent<PlayerLook>();
    }

    private void OnEnable()
    {
        _input.OnShootPerformed += Shoot;
    }
    private void OnDisable()
    {
        _input.OnShootPerformed -= Shoot;
    }

    private void Start()
    {
        if (_shotgun == null)
            throw new NullReferenceException ("Weapon link is not set"); 
    }

    private void Shoot()
    {
        _shotgun.Shoot(_look.LookRay);
    }
}
