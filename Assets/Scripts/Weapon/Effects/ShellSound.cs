using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShellSound : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private AudioSource _sound;
    private bool _isActive = true;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isActive && collision.gameObject.tag != PLAYER_TAG)
        {
            _sound.Play();
            _isActive = false;
        }
    }
}
