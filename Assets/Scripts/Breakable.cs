using UnityEngine;

public class Breakable : AbstractHealth
{
    public override void LostHPAction()
    {
        Destroy (gameObject);
    }
}
