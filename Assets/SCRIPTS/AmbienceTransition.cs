using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.AzureSky{
    public class AmbienceTransition : MonoBehaviour{
        private AzureTimeController aTC_Script;
        public AudioSource audio1;
        public AudioSource audio2;
        public AudioClip[] tracks;
        public float trackVolume = 1f;
        public int currentTrack;
        private float timeline; 

        private void Start(){
            aTC_Script = GameObject.Find("Azure[Sky] Dynamic Skybox").GetComponent<AzureTimeController>();
            audio1.clip = tracks[0];
            audio2.clip = tracks[1];
        }

        public void Update(){
            timeline = aTC_Script.m_timeline;

            if(timeline >= 0 && timeline <= 7.30 && !audio2.isPlaying){ // NIGHT TIME
                
                StartCoroutine(CrossfadeBA(audio2, audio1, 5.0f));
            } 

            if(timeline >= 7.30 && timeline <= 18.30f  && !audio1.isPlaying){ // DAY TIME
                StartCoroutine(CrossfadeAB(audio1, audio2, 5.0f));
            }

            if(timeline >= 18.30 && !audio2.isPlaying){
                StartCoroutine(CrossfadeBA(audio2, audio1, 5.0f));
            }
        }

        /* DAY TIME */
        IEnumerator CrossfadeAB(AudioSource audio1, AudioSource audio2, float seconds){
            float stepInterval = seconds / 20.0f;
            float volumeInterval = trackVolume / 20.0f;
            audio1.Play();

            for(int i = 0; i < 20; i++){
                audio2.volume -= volumeInterval;
                audio1.volume += volumeInterval;
                yield return new WaitForSeconds(stepInterval);
            }

            audio2.Stop();
        }
        /* NIGHT TIME */
        IEnumerator CrossfadeBA(AudioSource audio2, AudioSource audio1, float seconds){
            float stepInterval = seconds / 20.0f;
            float volumeInterval = trackVolume / 20.0f;

            audio2.Play();

            for(int i = 0; i < 20; i++){
                audio2.volume += volumeInterval;
                audio1.volume -= volumeInterval;
                yield return new WaitForSeconds(stepInterval);
            }

            audio1.Stop();
        }
    }
}
