using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team28
{
    public class HandTracking : SecondOrderDynamics
    {
        // Start is called before the first frame update
        Vector3 aproachPoint = Vector3.zero;
        public bool disableTracking = false;
        ScanPlayerController controller;
        void Start()
        {
            controller = GameObject.Find("Player").GetComponent<ScanPlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!disableTracking)
            {
                Vector3 barcodePosition = controller.Barcode.position;
                Vector3 target = new Vector3(barcodePosition.x, 1, barcodePosition.z);
                SetTargetVector(target);

            }
            IterateDynamics();
            transform.position = GetDynamicVector();
        }

        public void PauseHandTracking()
        {
            Debug.Log("TRIGGER");
            disableTracking = true;
            Invoke("EnableTracking", 0.3f);
        }

        private void EnableTracking()
        {
            disableTracking = false;
        }
    }
}

