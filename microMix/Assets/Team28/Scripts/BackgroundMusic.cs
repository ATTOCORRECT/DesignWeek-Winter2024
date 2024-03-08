using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team28
{

    public class BackgroundMusic : MicrogameEvents
    {
        public AudioSource source;
        public AudioClip clip;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        protected override void OnGameStart()
        {
            source.PlayOneShot(clip);
        }
    }
}

