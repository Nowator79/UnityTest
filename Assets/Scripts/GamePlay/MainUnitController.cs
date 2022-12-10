using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUnitController : MonoBehaviour
{
    protected Vector3 _positionTarget;
    [SerializeField]
    protected bool AIOn = true;
    protected float _timeStart;

    public virtual void Init()
    {

    }
    public virtual Vector3 PositionTarget
    {
        set
        {
            _positionTarget = value - transform.position;
        }
    }

    protected virtual void Start()
    {
        _timeStart = Random.value * 8;
        InvokeRepeating(nameof(GetRandomTarget), _timeStart, 8);
    }
    protected virtual void GetRandomTarget()
    {
        if (AIOn)
        {
            PositionTarget = new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f);
        }
    }
}
