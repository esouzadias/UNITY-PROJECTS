using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HFPS.Systems{
    public class NPCWallHangBehaviour : MonoBehaviour{
        AIRoutineBehaviour AIRB;
            GameObject npc;
            public MeshCollider collider;
            public Transform hangingPos;
            public Transform lookPosition;
            public bool goingToHang = false;
            public bool hangged = false;
            public bool atTheHangSpot = false;
            Vector3 lookPos;
            Quaternion rotation;
        // Start is called before the first frame update
        void Start()
        {
            AIRB = GameObject.FindGameObjectWithTag("NPC").GetComponent<AIRoutineBehaviour>();   
            npc = GameObject.FindGameObjectWithTag("NPC");
            hangingPos = GameObject.Find("HangingPos").GetComponent<Transform>();
            lookPos = lookPosition.position - transform.position;
            lookPos.y = 90f;
            rotation = Quaternion.LookRotation(lookPos);
        }

        // Update is called once per frame
        void Update()
        {
            /* if(npc.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base Idle")){
                hangged = true;
            } */

            /* if(atTheHangSpot) {
                npc.transform.position = hangingPos.position;
                npc.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            } */
        }
    }
}
