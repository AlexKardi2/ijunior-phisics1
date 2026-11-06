using UnityEngine;

public class DoorButton : AbstractHealth
{
    [SerializeField] private Animator _door;

    private void OnValidate()
    {
        if (_door == null)
            Debug.LogError("Door link for button haven't set", this);
    }

    public override void Die()
    {

        _door.SetBool("IsOpened", !_door.GetBool("IsOpened"));
    }

}
