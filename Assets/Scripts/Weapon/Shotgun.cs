using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WeaponAudio))]
[RequireComponent(typeof(HitscanShooter))]
[RequireComponent(typeof(ShellEjector))]
public class Shotgun : MonoBehaviour
{
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private float _reloadFallbackDuration = 1.7f;
    [SerializeField] private MuzzleEffects _muzzleEffects;
    private HitscanShooter _shooter;
    private Animator _animator;
    private WeaponAudio _audio;
    private ShellEjector _shellEjector;
    private bool _isReloading = false;
    private int _reloadGeneration;

    public void Shoot(Vector3 startPoint, Vector3 direction) => Shoot(new Ray(startPoint, direction));

    public void Shoot(Ray shootRay)
    {
        if (_isReloading)
        {
            Debug.Log("Still reloading");
            return;
        }
        
        _cameraShake.MakeRecoil();
        _muzzleEffects.Perform();
        _audio.PlayShoot();
        _animator.SetTrigger("Shoot");

        _shooter.Shoot(shootRay);
    }

    public void ExtractShell()
    {
        _shellEjector.Eject();
    }

    public void FinishReload()
    {
        _isReloading = true;
    }

    private void OnValidate()
    {
        if (_cameraShake == null)
            Debug.LogError("Camera shake link is not set", this);
        if (_muzzleEffects == null)
            Debug.LogError("Muzzle effects link is not set", this);

    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<WeaponAudio>();
        _shooter = GetComponent<HitscanShooter>();
        _shellEjector = GetComponent<ShellEjector>();
    }
    private IEnumerator ReloadFallback(int generation)
    {
        yield return new WaitForSeconds(_reloadFallbackDuration);

        if (_isReloading && generation == _reloadGeneration)
            FinishReload();
    }
}
