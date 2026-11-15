using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _reloadSound;

    public void PlayShoot() => _shootSound.Play();
    public void PlayReload() => _reloadSound.Play();

    private void OnValidate()
    {
        if (_shootSound == null)
            Debug.LogError("Shoot Sound is not set", this);
        if (_reloadSound == null)
            Debug.LogError("Reload Sound object is not set", this);
    }
}
