using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeInOut : MonoBehaviour
{
    public Text regionName;

    private float fadeTime;
    private bool fadeIn;

    private void Start()
    {
        if(regionName != null) regionName.CrossFadeAlpha(0, 0.0f, false);
        fadeTime = 0f;
        fadeIn = false;
    }

    private void Update()
    {
        if(fadeIn){
            FadeIn();
        }
        else if(regionName != null && regionName.color.a != 0){
            regionName.CrossFadeAlpha(0, 0.5f, false);
        }
    }

    private void FadeIn(){
        regionName.CrossFadeAlpha(1, 0.5f, false);
        fadeTime += Time.deltaTime;
        if(regionName.color.a == 1 && fadeTime > 1.5f){
            fadeIn = false;
            fadeTime = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Region"){
            fadeIn = true;
            regionName.text = other.name;
        }
    }
}
