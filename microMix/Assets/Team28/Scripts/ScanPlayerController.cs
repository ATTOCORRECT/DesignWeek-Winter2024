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
                    canScan = false;
                    Invoke("FlashScanner",0.1f);
                    Invoke("NewItem",0.2f);
                }
            }
        }
        private void FlashScanner()
        {
            ScanLight.GetComponent<Light>().intensity = 1000;
            Invoke("DisableScanLight", 0.1f);
            
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
            scoreManager.score += 1;

            BroadcastMessage("UpdateScreen");
            //fling
            ActiveItem.GetComponent<AproachPoint>().disableTracking = true;
            ActiveItem.GetComponent<Rigidbody>().isKinematic = false;
            ActiveItem.GetComponent<Rigidbody>().useGravity = true;
            ActiveItem.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1), 3 + Random.Range(-1,1), -1 + Random.Range(-1, 0)), ForceMode.Impulse);

            //ActiveItem.GetComponent<AproachPoint>().SetTargetVector(new Vector3(0, 0, -4));

            //reset
            ActiveItem = null;
            poolManager.SpawnNewItem();
            Barcode = GetBarcodeTransform(ActiveItem);

            canScan = true;
        }
    }
}


