using UnityEngine;

public interface IPlayerInput
{
    public Vector3 Movement { get; }
    public Vector2 Look { get; }

    public bool ConsumeJump();
}
