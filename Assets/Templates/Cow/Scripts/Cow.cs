using UnityEngine;
using UnityEngine.AI;

public class Cow : MainUnitController
{
    private Vector3 _targetObject;
    private NavMeshAgent navMeshAgent;

    public override Vector3 PositionTarget
    {
        set
        {
            navMeshAgent.SetDestination(value);
            _targetObject = value;
        }
    }

    protected override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //Normalize();
    }

    private void Normalize()
    {
        Ray rightRay = new(transform.position, transform.TransformDirection(Vector3.down));
        if (Physics.Raycast(rightRay, out RaycastHit hit, 100))
        {
            Vector3 _normalize = hit.normal;
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(_normalize.x, transform.rotation.y, _normalize.z, transform.rotation.w), 0.1f);
        }
    }
    protected override void GetRandomTarget()
    {
        if (AIOn)
        {
            PositionTarget = new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f) * 50;
        }
    }
}
