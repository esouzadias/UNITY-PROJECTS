using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HFPS.Systems{
    public class NPCDoorBehaviour : MonoBehaviour
    {
        AIRoutineBehaviour AIRB;
        GameObject npc;
        public MeshCollider collider;
        public Transform doorPos;
        public Transform lookPosition;
        Vector3 targetLookPosition;
        public bool goingToOpenDoor = false;
        public bool opened = false;
        public bool atTheDoorSpot = false;
        Vector3 lookPos;
        Quaternion rotation;

        // Start is called before the first frame update
        void Start()
        {
            AIRB = GameObject.FindGameObjectWithTag("NPC").GetComponent<AIRoutineBehaviour>();   
            npc = GameObject.FindGameObjectWithTag("NPC");
            doorPos = GameObject.Find("DoorPos").GetComponent<Transform>();
            lookPos = lookPosition.position - npc.transform.position;
            targetLookPosition = new Vector3(lookPosition.position.x, npc.transform.position.y, lookPosition.position.z);
            lookPos.y = 90f;
            rotation = Quaternion.LookRotation(lookPos);
            
        }

        // Update is called once per frame
        void Update()
        {
            if(npc != null && npc.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Door Open")){
                opened = true;
            }

            if(npc != null && atTheDoorSpot) {
                npc.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
                npc.transform.LookAt(targetLookPosition);
                AtTheDoorSpot();
            }
        }

        void AtTheDoorSpot(){
            if(npc != null){
                npc.transform.position = doorPos.position;
            }
        }
    }
}
