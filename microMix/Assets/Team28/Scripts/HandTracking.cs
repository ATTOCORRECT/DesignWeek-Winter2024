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
        public string playerName = "Player";
        ScanPlayerController controller;
        void Start()
        {
            controller = GameObject.Find(playerName).GetComponent<ScanPlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!disableTracking)
            {
                Vector3 barcodePosition = controller.Barcode.position;
                Vector3 target = new Vector3(barcodePosition.x, barcodePosition.y + 0.8f, barcodePosition.z);
                SetTargetVector(target);

            }
            IterateDynamics();
            transform.position = GetDynamicVector();
        }

        public void PauseHandTracking()
        {
            disableTracking = true;
            Invoke("EnableTracking", 0.3f);
        }

        private void EnableTracking()
        {
            disableTracking = false;
        }
    }
}

