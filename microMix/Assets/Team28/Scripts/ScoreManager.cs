using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace team28
{
    public class ScoreManager : MonoBehaviour
    {
        public ItemPoolManager PoolManager;
        public int score;

        // Start is called before the first frame update
        void Awake()
        {
            score = 0;
        }

        // Update is called once per frame
        void Update()
        {
            PoolManager.poolID = score / 4;
            PoolManager.poolID = Mathf.Clamp(PoolManager.poolID, 0, 2);
        }
    }

}
