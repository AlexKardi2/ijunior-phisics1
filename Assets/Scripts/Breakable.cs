using UnityEngine;

public class Breakable : AbstractHealth
{
    public override void Die()
    {
        Destroy (gameObject);
    }
}
