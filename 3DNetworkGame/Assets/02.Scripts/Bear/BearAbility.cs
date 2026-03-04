using UnityEngine;

public abstract class BearAbility : MonoBehaviour
{
    protected BearController _owner { get; private set; }

    protected virtual void Awake()
    {
        _owner = GetComponentInParent<BearController>();
    }
}