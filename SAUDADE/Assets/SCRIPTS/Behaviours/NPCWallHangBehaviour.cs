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
            Vector3 targetLookPosition;
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
            lookPos = lookPosition.position - npc.transform.position;
            targetLookPosition = new Vector3(lookPosition.position.x, npc.transform.position.y, lookPosition.position.z);
            lookPos.y = 90f;
            rotation = Quaternion.LookRotation(lookPos);
        }

        // Update is called once per frame
        void Update()
        {
            if(npc.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("arms_crossed_idle")){
                hangged = true;
            }

            if(atTheHangSpot) {
                npc.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
                npc.transform.LookAt(targetLookPosition);
                AtTheHangSpot();
            }
        }

        void AtTheHangSpot(){
            npc.transform.position = hangingPos.position;
        }
    }
}
