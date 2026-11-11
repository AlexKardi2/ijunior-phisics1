using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Shotgun : MonoBehaviour
{
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private float _reloadFallbackDuration = 1.7f;
    private Animator _animator;
    private bool _isReloading = false;
    private int _reloadGeneration;

    public void Shoot(Vector3 startPoint, Vector3 direction) => Shoot(new Ray(startPoint, direction));

    public void Shoot(Ray ray)
    {
        if (_isReloading)
            return;

        _cameraShake.MakeRecoil();
    }

    //TODO set animation event
    public void ExtractShell() => _shellEjector.Eject();

    //TODO set animauton event
    public void FinishReload ()
    {
        _isReloading = true;
    }

    private void OnValidate()
    {
        if (_cameraShake == null)
            Debug.LogError("Camera shake link is not set", this);

    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private IEnumerator ReloadFallback(int generation)
    {
        yield return new WaitForSeconds(_reloadFallbackDuration);

        if (_isReloading && generation == _reloadGeneration)
            FinishReload();
    }



    /*
    //Ref Создание префабов отделить в отдельный класс и вызывать его из оружия
    //Ref Разделить скрипт на зоны ответственности. отдельно создание эффектов, отдельно нанесение урона, нанесение урона при помощи рейкастов - отдельным скриптом, т.е. оружие должно только вызывать в скрипте необходимость послать рейкаст
    //Ref Для звуков создать отдельный скрипт, это было бы логичнее
    //TODO нельзя стрелять до окончания перезарядки
    [SerializeField] private ShootEffects _shotEffects;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private float _impactForce = 10f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _reloadSound;
    [SerializeField] private WaterSplasher _splasher;
    [SerializeField] private DecalPlacer _decalPlacer;

    [Header("Shell")]

    [SerializeField] private Transform _shellPoint;
    [SerializeField] private float _shellSpeed = 2f;
    [SerializeField] private float _shellAngularRange = 5f;

    [Header("Recoil")]
    [SerializeField] private CameraShake _cameraShake;

    private void OnValidate()
    {
        if (_shotEffects == null)
            throw new System.NullReferenceException("Shooter effects link is not set");
        if (_reloadSound == null)
            throw new System.NullReferenceException("Reload sound is not set");
        if (_animator == null)
            throw new System.NullReferenceException("Animator is not set");

        if (_splasher == null)
            throw new System.NullReferenceException("Water splasher link is not set");
    }



    public void Shoot(Ray shootRay)
    {
        _cameraShake.MakeRecoil();
        _shotEffects.Perform();
        _animator.SetTrigger("Shoot");

        if (Physics.Raycast(shootRay, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            
            //_splasher.TryCreateWaterSplash(shootRay.origin, hitInfo.point);
            _decalPlacer.Place(hitInfo);

            AbstractHealth health = hitInfo.collider.GetComponentInParent<AbstractHealth>();
            health?.TakeDamage(_damage);

            Rigidbody targetBody = hitInfo.rigidbody;
            targetBody?.AddForceAtPosition(shootRay.direction * _impactForce, hitInfo.point);
            
        }
    }

    

    public void PlayReloadSound()
    {
        _reloadSound.Play();
    }

    public void ExtractShell()
    {
        Rigidbody shell = Instantiate(_shellPrefab, _shellPoint.position, _shellPoint.rotation);
        shell.linearVelocity = _shellPoint.forward * _shellSpeed;
        shell.angularVelocity = Vector3.up * (Random.Range(-_shellAngularRange, _shellAngularRange));
        
    }*/
}
