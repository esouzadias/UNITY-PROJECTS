using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThunderWire.Input;
using HFPS.Systems;

namespace HFPS.Player
{
    public class PocketWatch : MonoBehaviour
    {
        [HideInInspector] public Animator pAnim;
        GameObject pocketWatchArms;
        PlayerFunctions pF_Script;

        // Start is called before the first frame update
        void Start()
        {
            pAnim = GameObject.Find("PocketWatchArms_Anim").GetComponent<Animator>();
            pocketWatchArms = GameObject.Find("PocketWatchArms_Anim");
            pF_Script = GameObject.Find("MouseLook").GetComponent<PlayerFunctions>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.F)){
                pAnim.Play("PocketWatch Draw");
                pAnim.SetBool("watchingTime", true);


            } 
            
            if(Input.GetKeyUp(KeyCode.F)) {
                pAnim.SetBool("watchingTime", false);
            }
        }
    }
}
