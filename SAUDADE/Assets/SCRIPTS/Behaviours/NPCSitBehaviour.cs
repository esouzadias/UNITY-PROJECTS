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
        Vector3 targetLookPosition;
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
            lookPos = lookPosition.position - npc.transform.position;
            targetLookPosition = new Vector3(lookPosition.position.x, npc.transform.position.y, lookPosition.position.z);
            lookPos.y = 90f;
            rotation = Quaternion.LookRotation(lookPos);
        }

        // Update is called once per frame
        void Update()
        {
            if(npc.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit")){
                sitted = true;
                /* AIRB.StartCoroutine(); */
            }

            if(atTheSpot) {
                npc.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
                npc.transform.LookAt(targetLookPosition);
            }
        }
    }
}
