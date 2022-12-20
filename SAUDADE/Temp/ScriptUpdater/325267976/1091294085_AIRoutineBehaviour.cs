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
            sitBehaviourScript = GameObject.FindGameObjectWithTag("Sit").GetComponent<NPCSitBehaviour>();
            wallHangBehaviourScript = GameObject.FindGameObjectWithTag("WallHang").GetComponent<NPCWallHangBehaviour>();
            wallHangBehaviourScript = GameObject.FindGameObjectWithTag("Door").GetComponent<NPCDoorHangBehaviour>();
            step = speed * Time.deltaTime; // calculate distance to move
            anim = GetComponent<Animator>();
        }

        private void Update() {
            //SITTING BEHAVIOUR
            if(sitBehaviourScript.goingToSit){
                this.transform.position = Vector3.MoveTowards(transform.position, sitBehaviourScript.sittingPos.position, step);
            }

            //WALL HANG BEHAVIOUR
            if(wallHangBehaviourScript.goingToHang){
                this.transform.position = Vector3.MoveTowards(transform.position, wallHangBehaviourScript.hangingPos.position, step);
            }

            //OPEN DOOR BEHAVIOUR
            if(doorBehaviourScript.goingToOpenDoor){
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
        }
    }
}
