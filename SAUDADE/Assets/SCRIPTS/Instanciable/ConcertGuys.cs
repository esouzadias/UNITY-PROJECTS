using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.AzureSky{
    public class ConcertGuys : MonoBehaviour{

        AzureTimeController aTC_Script;
        

        public AudioClip[] concertMusics;
        public AudioClip applauseSound;
        private AudioSource aS;

        /* Booleans */
        [Header ("Booleans")]
        public bool playerInside = false; 
        public bool concertOff = true;
        public bool concertStarted;
        public bool concertStarting;


        /* GameObjects */
        GameObject player;
        [Header ("GameObjects")]
        public GameObject concertGuys;
        public GameObject normalDayLighting;
        public GameObject concertDayLighting;
        public GameObject normalDaySound;
        public GameObject concertDaySound;
        public GameObject radioSound;
        public Transform CGuitarPlayerChair;
        public GameObject CGuitarPlayer;

        [Header ("Animators")]
        public Animator ptGuitarAnimator;
        public Animator cGuitarAnimator;
        public Animator cGuitarAnimatorIdle;

        void Start()
        {
            aTC_Script = GameObject.Find("Azure[Sky] Dynamic Skybox").GetComponent<AzureTimeController>();
            player = GameObject.FindGameObjectWithTag("Player");

            aS = GetComponent<AudioSource>();
            aS.loop = false;
        }

        void Update(){
            aTC_Script.m_dayOfWeek = (int) aTC_Script.m_dateTime.DayOfWeek; // Get Week Day
            if(aTC_Script.m_dayOfWeek == 5 && aTC_Script.m_hour >= 20 && aTC_Script.m_minute > 30){
                concertOff = false;
                HaveConcert();
            } 

            if(aTC_Script.m_dayOfWeek != 5){
                concertOff = true;
            }
        }

        /* Player Walks In */
            private void OnTriggerEnter(Collider other)
            {
                if(other.gameObject.tag == "Player"){
                    playerInside = true;
                }
            } 
        /*  */

        void HaveConcert(){
            if(!concertOff){
                if(!playerInside && !concertStarting && !concertStarted){
                        concertStarted = true;
                        concertStarting = false;
                        Concert();
                        Debug.Log("Concert Starting without you");
                }
                if(playerInside && !concertStarting && !concertStarted){
                    concertStarting = true;
                    concertStarted = false;
                    concertOff = false;
                    Concert();
                    Debug.Log("Concert Starting");
                } 
                AudioClip GetRandomClip(){
                    return concertMusics[Random.Range(0, concertMusics.Length)];
                }
                void Concert(){ 
                    if(concertStarted){ // Here the player walks into a concert that already started
                        StartCoroutine(ConcertSequence());
                        concertGuys.SetActive(true);

                        concertDayLighting.SetActive(true);
                        concertDaySound.SetActive(true);

                        normalDayLighting.SetActive(false);
                        normalDaySound.SetActive(false);

                        radioSound.GetComponent<AudioSource>().enabled = false;
                    }
                    if(concertStarting){
                        StartCoroutine(ConcertBegginingSequence());
                        /* concertGuys.SetActive(true); */

                        concertDayLighting.SetActive(true);
                        concertDaySound.SetActive(true);

                        normalDayLighting.SetActive(false);
                        normalDaySound.SetActive(false);

                        radioSound.GetComponent<AudioSource>().enabled = false;
                    }
                }
                IEnumerator ConcertSequence(){
                    /* SONG 1 */
                        if(!aS.isPlaying){
                            aS.clip = GetRandomClip();
                            aS.Play();
                            ptGuitarAnimator.SetBool("Playing", true);
                            cGuitarAnimator.SetBool("playing", true);

                             ptGuitarAnimator.SetBool("Applause", false);
                             cGuitarAnimator.SetBool("applause", false);
                        }
                        yield return new WaitUntil(() => aS.isPlaying == false);
                    /* Applause */
                        ptGuitarAnimator.SetBool("Playing", false);
                        ptGuitarAnimator.SetBool("Applause", true);

                        cGuitarAnimator.SetBool("playing", false);
                        cGuitarAnimator.SetBool("applause", true);
                        aS.clip = applauseSound;
                        aS.Play();
                        yield return new WaitUntil(() => aS.isPlaying == false);
                    /* SONG 2 */
                        ptGuitarAnimator.SetBool("Playing", true);
                        ptGuitarAnimator.SetBool("Applause", false);

                        cGuitarAnimator.SetBool("playing", true);
                        cGuitarAnimator.SetBool("applause", false);
                        aS.clip = GetRandomClip();
                        aS.Play();
                        yield return new WaitUntil(() => aS.isPlaying == false);
                    /* Applause */
                    ptGuitarAnimator.SetBool("Playing", false);
                    ptGuitarAnimator.SetBool("Applause", true);

                    cGuitarAnimator.SetBool("playing", false);
                    cGuitarAnimator.SetBool("applause", true);
                    aS.clip = applauseSound;
                    aS.Play();
                    yield return new WaitUntil(() => aS.isPlaying == false);
                    ConcertOver();
                }
                IEnumerator ConcertBegginingSequence(){
                    /* Beggining Sequence */
                        /* Classical Guitar Player */
                            cGuitarAnimatorIdle.SetBool("GetUp", true);
                            yield return new WaitForSeconds(1);
                            /* Rotate towards chair */
                                    Vector3 targetDirection = CGuitarPlayerChair.position - cGuitarAnimatorIdle.gameObject.transform.position;
                                    float speed = 0.1f;
                                    float singleStep = speed * Time.deltaTime;
                                    Vector3 newDirection = Vector3.RotateTowards(cGuitarAnimatorIdle.transform.forward, targetDirection, singleStep, 0.0f);
                                    Debug.DrawRay(cGuitarAnimatorIdle.transform.position, newDirection, Color.red);
                                    cGuitarAnimatorIdle.transform.rotation = Quaternion.LookRotation(newDirection);
                            yield return new WaitForSeconds(2);
                            /* Walk towards chair */
                                    cGuitarAnimatorIdle.SetBool("walking", true);
                                    var step =  speed * Time.deltaTime; // calculate distance to move
                                    CGuitarPlayer.transform.position = CGuitarPlayerChair.position * step;
                                    yield return new WaitForSeconds(2);
                                    cGuitarAnimatorIdle.SetBool("walking", false);
                            /* Sitting */
                            cGuitarAnimatorIdle.SetBool("sit", true);
                        /*  */

                    /* SONG 1 */
                        if(!aS.isPlaying){
                            aS.clip = GetRandomClip();
                            aS.Play();
                            ptGuitarAnimator.SetBool("Playing", true);
                            cGuitarAnimator.SetBool("playing", true);

                             ptGuitarAnimator.SetBool("Applause", false);
                             cGuitarAnimator.SetBool("applause", false);
                        }
                        yield return new WaitUntil(() => aS.isPlaying == false);
                    /* Applause */
                        ptGuitarAnimator.SetBool("Playing", false);
                        ptGuitarAnimator.SetBool("Applause", true);

                        cGuitarAnimator.SetBool("playing", false);
                        cGuitarAnimator.SetBool("applause", true);
                        aS.clip = applauseSound;
                        aS.Play();
                        yield return new WaitUntil(() => aS.isPlaying == false);
                    /* SONG 2 */
                        ptGuitarAnimator.SetBool("Playing", true);
                        ptGuitarAnimator.SetBool("Applause", false);

                        cGuitarAnimator.SetBool("playing", true);
                        cGuitarAnimator.SetBool("applause", false);
                        aS.clip = GetRandomClip();
                        aS.Play();
                        yield return new WaitUntil(() => aS.isPlaying == false);
                    /* Applause */
                        ptGuitarAnimator.SetBool("Playing", false);
                        ptGuitarAnimator.SetBool("Applause", true);

                        cGuitarAnimator.SetBool("playing", false);
                        cGuitarAnimator.SetBool("applause", true);
                        aS.clip = applauseSound;
                        aS.Play();
                        yield return new WaitUntil(() => aS.isPlaying == false);
                    ConcertOver();
                }
            } 
            else{
                concertOff = true;
                concertGuys.SetActive(false);
                concertDayLighting.SetActive(false);
                concertDaySound.SetActive(false);
                normalDayLighting.SetActive(true);
                normalDaySound.SetActive(true);
                radioSound.GetComponent<AudioSource>().enabled = true;
            }
        }

        void ConcertStart(){ //Here the player experiences the full concert from the beggining with all the animations
            if(concertGuys != null) concertGuys.SetActive(true);
            if(concertDayLighting != null) concertDayLighting.SetActive(true);
            if(concertDaySound != null) concertDaySound.SetActive(true);

            if(normalDayLighting != null) normalDayLighting.SetActive(false);
            if(normalDaySound != null) normalDaySound.SetActive(false);
        }

        void ConcertOver(){
            concertGuys.SetActive(false);
            concertDayLighting.SetActive(false);
            concertDaySound.SetActive(false);
            
            normalDayLighting?.SetActive(true);
            normalDaySound?.SetActive(true);
        }
   
    }
}
