using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace team28
{
    public class ScanPlayerController : SecondOrderDynamics
    {
        [Header ("Item Variables")]
        public GameObject ActiveItem;
        public GameObject ScanLight;
        Transform Barcode;

        float angleTolerance = 10;
        bool canScan = true;
        // Start is called before the first frame update
        Vector2 angularVelocity = Vector2.zero;

        [Header("Pool Manager Script")]
        public ItemPoolManager poolManager;
        void Start()
        {
            Barcode = GetBarcodeTransform(ActiveItem);
        }

        // Update is called once per frame
        void Update()
        {
            if(ActiveItem != null)
            {
                angularVelocity += stick * 0.01f;
                angularVelocity *= 0.98f;
                ActiveItem.transform.Rotate(angularVelocity.y, 0, -angularVelocity.x, Space.World);

                if (canScan && Vector3.Angle(Barcode.up, Vector3.up) < angleTolerance) // barcode visible to scanner
                {
                    Invoke("FlashScanner", 0.1f); // flash
                    canScan = false; // disable scanning
                    KillTheItem();
                }
            }

            else
            {
                poolManager.SpawnNewItem();
                Debug.Log(ActiveItem.name);
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

        private void KillTheItem()
        {
            ActiveItem = null;
            canScan = true;
        }
    }
}


