using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDrivingCar : Seek
{
    public Path path;
    public float pathOffSet = 0.5f;
    float currentParam;
    public float avoidDistance = 5.0f;
    public float lookAhead = 10.0f;

    public override void Awake()
    {
        base.Awake();
        target = new GameObject();
        currentParam = 0f;
        float minPathOffSet = 0.5f;
        float maxPathOffSet = 3.5f;
        pathOffSet = Random.Range(minPathOffSet, maxPathOffSet);
        transform.parent = null;
    }

    public override Steering GetSteering()
    {
        currentParam = path.GetParam(transform.position, currentParam);
        float targetParam = currentParam + pathOffSet;
        target.transform.position = path.GetPosition(targetParam);
        Steering steering = new Steering();
        Vector3 position = transform.position;
        Vector3 rayVector = agent.velocity.normalized * lookAhead;
        Vector3 direction = rayVector;
        RaycastHit hit;

        if (Physics.Raycast(position, direction, out hit, lookAhead))
        {
            position = hit.point + hit.normal * avoidDistance;
            target.transform.position = position;
            steering = base.GetSteering();
        }

        return base.GetSteering();
    }
}
