using System.Collections;
using System.Collections.Generic;
using team28;
using UnityEngine;


namespace team28
{
    public class ApproachPoint : SecondOrderDynamics
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
            SetTargetVector(aproachPoint);
            if (!disableTracking)
            {
                IterateDynamics();
                transform.position = GetDynamicVector();
            }
        }
    }
}


