using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace team28
{
    public class ScanPlayerController : SecondOrderDynamics
    {
        public GameObject ActiveItem;
        public GameObject ScanLight;
        public Transform Barcode;

        [Header("Manager Scripts")]
        public ItemPoolManager poolManager;
        public ScoreManager scoreManager;

        float angleTolerance = 10;
        bool canScan = true;
        // Start is called before the first frame update
        Vector3 targetAngle = Vector3.zero;
        Vector3 dynamicAngle = Vector3.zero;

        bool canSpawn = false;

        void Start()
        {
            poolManager.SpawnNewItem();
            Barcode = GetBarcodeTransform(ActiveItem);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(ActiveItem)
            {
                Vector3 oldDynamicAngle = dynamicAngle;

                targetAngle += (Vector3)stick * 3f;
                SetTargetVector(targetAngle);
                IterateDynamics();
                dynamicAngle = GetDynamicVector();

                Vector3 angularVelocity = dynamicAngle - oldDynamicAngle;
                ActiveItem.transform.Rotate(angularVelocity.y, 0, -angularVelocity.x, Space.World);
            }
        }

        void Update()
        {
            if (ActiveItem)
            {
                if (canScan && Vector3.Angle(Barcode.up, Vector3.up) < angleTolerance) // barcode visible to scanner
                {
                    NewItem();
                }
            }
        }
        private void FlashScanner()
        {
            ScanLight.GetComponent<Light>().intensity = 1000;
            Invoke("DisableScanLight", 0.2f);
            
        }

        private void DisableScanLight()
        {
            ScanLight.GetComponent<Light>().intensity = 10;
        }

        public Transform GetBarcodeTransform(GameObject BarcodeItem)
        {
            return BarcodeItem.transform.Find("Barcode");
        }

        public void NewItem()
        {
            FlashScanner();

            scoreManager.score += 1;

            //fling
            //ActiveItem.GetComponent<Rigidbody>().AddForce(Vector3.right, ForceMode.Impulse); Phys 3d Not working??
            //ActiveItem.GetComponent<AproachPoint>().disableTracking = true;

            ActiveItem.GetComponent<AproachPoint>().SetTargetVector(new Vector3(0, 0, -4));

            //reset
            ActiveItem = null;
            poolManager.SpawnNewItem();
            Barcode = GetBarcodeTransform(ActiveItem);
        }
    }
}


