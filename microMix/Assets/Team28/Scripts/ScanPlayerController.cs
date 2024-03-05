using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team28
{
    public class ScanPlayerController : SecondOrderDynamics
    {
        public GameObject ActiveItem;
        public GameObject ScanLight;
        Transform Barcode;

        float angleTolerance = 10;
        bool canScan = true;
        // Start is called before the first frame update
        Vector3 targetAngle = Vector3.zero;
        Vector3 dynamicAngle = Vector3.zero;

        void Start()
        {
            Barcode = GetBarcodeTransform(ActiveItem);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 oldDynamicAngle = dynamicAngle;
    
            targetAngle += (Vector3)stick * 3f;
            SetTargetVector(targetAngle);
            IterateDynamics();
            dynamicAngle = GetDynamicVector();

            Vector3 angularVelocity = dynamicAngle - oldDynamicAngle;
            ActiveItem.transform.Rotate(angularVelocity.y, 0, -angularVelocity.x, Space.World);
        }

        void Update()
        {
            if (canScan && Vector3.Angle(Barcode.up, Vector3.up) < angleTolerance) // barcode visible to scanner
            {
                Invoke("FlashScanner", 0.1f); // flash
                canScan = false; // disable scanning
            }
        }
        private void FlashScanner()
        {
            ScanLight.GetComponent<Light>().intensity = 1000;
            Invoke("DisableScanLight", 0.3f);
        }

        private void DisableScanLight()
        {
            ScanLight.GetComponent<Light>().intensity = 10;
        }

        public Transform GetBarcodeTransform(GameObject ActiveItem)
        {
            return ActiveItem.transform.Find("Barcode");
        }
    }
}


