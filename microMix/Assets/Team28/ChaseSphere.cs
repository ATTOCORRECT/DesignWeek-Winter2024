using System.Collections;
using System.Collections.Generic;
using team28;
using UnityEngine;

public class ChaseSphere : SecondOrderDynamics
{
    public GameObject Sphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = Sphere.transform.position;
        SetTargetVector(targetPos);

        transform.position = GetDynamicVector();
    }
}
