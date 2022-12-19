using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HFPS.Systems{
    public class NPCSitBehaviour : MonoBehaviour
    {
        AIRoutineBehaviour AIRB;
        GameObject npc;
        public MeshCollider collider;
        public Transform sittingPos;
        public Transform lookPosition;
        public bool goingToSit = false;
        public bool sitted = false;
        public bool atTheSpot = false;
        Vector3 lookPos;
        Quaternion rotation;

        // Start is called before the first frame update
        void Start()
        {
            AIRB = GameObject.FindGameObjectWithTag("NPC").GetComponent<AIRoutineBehaviour>();   
            npc = GameObject.FindGameObjectWithTag("NPC");
            sittingPos = GameObject.Find("SittingPos").GetComponent<Transform>();
            lookPos = lookPosition.position - transform.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos);
        }

        // Update is called once per frame
        void Update()
        {
            if(npc.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit")){
                sitted = true;
            }

            if(atTheSpot && !sitted) {
                npc.transform.position = sittingPos.position;
                npc.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            }
        }
    }
}
