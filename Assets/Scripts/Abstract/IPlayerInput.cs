using System;
using UnityEngine;

public interface IPlayerInput
{
    public event Action OnShootPerformed;
    public Vector3 Movement { get; }
    public Vector2 Look { get; }

    public bool ConsumeJump();
}
