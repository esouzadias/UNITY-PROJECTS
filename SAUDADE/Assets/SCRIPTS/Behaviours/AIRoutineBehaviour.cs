using System.Collections;
using System.Collections.Generic;
using HFPS.Player;
using UnityEngine;

namespace HFPS.Systems{
    public class AIRoutineBehaviour : MonoBehaviour{
        NPCSitBehaviour sitBehaviourScript;
        NPCWallHangBehaviour wallHangBehaviourScript;
        NPCDoorBehaviour doorBehaviourScript;
        Animator anim;
        float step;
        float speed = 1.5f;

        // Start is called before the first frame update
        void Start(){
            sitBehaviourScript = GameObject.FindGameObjectWithTag("Sit")?.GetComponent<NPCSitBehaviour>();
            wallHangBehaviourScript = GameObject.FindGameObjectWithTag("WallHang")?.GetComponent<NPCWallHangBehaviour>();
            doorBehaviourScript = GameObject.FindGameObjectWithTag("Door")?.GetComponent<NPCDoorBehaviour>();
            step = speed * Time.deltaTime; // calculate distance to move
            anim = GetComponent<Animator>();
        }

        private void Update() {
            //SITTING BEHAVIOUR
            if(sitBehaviourScript != null && sitBehaviourScript.goingToSit){
                this.transform.position = Vector3.MoveTowards(transform.position, sitBehaviourScript.sittingPos.position, step);
            }

            //WALL HANG BEHAVIOUR
            if(wallHangBehaviourScript != null && wallHangBehaviourScript.goingToHang){
                this.transform.position = Vector3.MoveTowards(transform.position, wallHangBehaviourScript.hangingPos.position, step);
            }

            //OPEN DOOR BEHAVIOUR
            if(doorBehaviourScript != null && doorBehaviourScript.goingToOpenDoor){
                this.transform.position = Vector3.MoveTowards(transform.position, doorBehaviourScript.doorPos.position, step);
            }
        }

        private void OnTriggerEnter(Collider other) {
            //SITTING BEHAVIOUR
            if(other.tag == "Sit"){
                anim.SetBool("Walking", false);
                sitBehaviourScript.goingToSit = true;
                GetComponent<ZombieBehaviourAI>().enabled = false;
                sitBehaviourScript.collider.enabled = false;
            }
            if(other.name == "SittingPos"){
                sitBehaviourScript.atTheSpot = true;
                sitBehaviourScript.goingToSit = false;
                anim.SetBool("Walking", false);
                anim.SetBool("Sit", true);
            }

            //WALL HANG BEHAVIOUR
            if(other.tag == "WallHang"){
                anim.SetBool("Walking", false);
                wallHangBehaviourScript.goingToHang = true;
                GetComponent<ZombieBehaviourAI>().enabled = false;
                wallHangBehaviourScript.collider.enabled = false;
            }
            if(other.name == "HangingPos"){
                wallHangBehaviourScript.atTheHangSpot = true;
                wallHangBehaviourScript.goingToHang = false;
                anim.SetBool("Walking", false);
                anim.SetBool("Hang", true);
            }

            //OPEN DOOR BEHAVIOUR
            if(other.tag == "Door"){
                anim.SetBool("Walking", false);
                doorBehaviourScript.goingToOpenDoor = true;
                GetComponent<ZombieBehaviourAI>().enabled = false;
                doorBehaviourScript.GetComponent<Collider>().enabled = false;
            }

            if(other.name == "DoorPos"){
                doorBehaviourScript.atTheDoorSpot = true;
                doorBehaviourScript.goingToOpenDoor = false;
                anim.SetBool("Walking", false);
                anim.SetTrigger("OpenDoor");
            }
        }

        public void PlayDoorExAnim(){
            doorBehaviourScript.GetComponent<Animator>().Play("DoorOpenAnim");
            anim.ResetTrigger("OpenDoor");
        }

        public void DeactivateNPC(){
            Destroy(this.gameObject);
        }

        /* public void StartCoroutine(){
            StartCoroutine(Coroutine(0.0f));
        } */

        /* private IEnumerator Coroutine(float waitTime){
            if((sitBehaviourScript != null && sitBehaviourScript.sitted) || (wallHangBehaviourScript != null && wallHangBehaviourScript.hangged)){
                if (Random.Range(1,5) == 1) {
                    anim.Play("Sitting_3");
                };
                if(Random.Range(1,5) == 2){
                    anim.Play("Sitting_4");
                };
                if(Random.Range(1,5) == 3){
                    anim.Play("Sitting_5");
                };
                if(Random.Range(1,5) == 4){
                    anim.Play("Sitting");
                };
                if(Random.Range(1,5) == 5){
                    anim.Play("Sitting Idle");
                };
            }
            yield return new WaitForSeconds(200f);
            sitBehaviourScript.sitted = false;
            GetComponent<ZombieBehaviourAI>().enabled = true;
        } */
    }
}
