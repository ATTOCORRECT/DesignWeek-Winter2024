using System.Collections;
using System.Collections.Generic;
using team28;
using UnityEngine;

public class AproachPoint : SecondOrderDynamics
{
    // Start is called before the first frame update
    public Vector3 aproachPoint = Vector3.zero;
    public bool disableTracking = false;
    void Start()
    {
        SetTargetVector(aproachPoint);
        SetDynamicVector(transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!disableTracking)
        {
            IterateDynamics();
            transform.position = GetDynamicVector();
        }
    }
}
