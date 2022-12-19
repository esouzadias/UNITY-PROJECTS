using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.AzureSky
{
    public class LightsOnOff : MonoBehaviour
    {
        private AzureTimeController aTC_Script;

        private Light[] lights;

        private GameObject[] lightLamps;

        /* CHANGE EMISSIVE MATERIAL */
        public Material lampEmissions;

        public Material windowEmissions;

        public Material WallSmallLampEmissions;

        public Material tableLampEmissions;

        public Material ceelingLampEmissions;

        /* DEFAULT MATERIALS */
        public Material lampDefault;

        public Material windowDefault;

        public Material WallSmallLampDefault;

        public Material tableLampDefault;

        public Material ceelingLampDefault;

        private void Start()
        {
            lights = FindObjectsOfType(typeof (Light)) as Light[];
            lightLamps = FindObjectsOfType(typeof (GameObject)) as GameObject[];
            aTC_Script =
                GameObject
                    .Find("Azure[Sky] Dynamic Skybox")
                    .GetComponent<AzureTimeController>();
        }

        private void Update()
        {
            if (aTC_Script?.m_timeOfDay >= 8.0f)
            {
                TurnOffLights();
            }
            if (aTC_Script?.m_timeOfDay >= 18.0f)
            {
                TurnOnLights();
            }
        }

        public void TurnOnLights()
        {
            foreach (Light light in lights)
            {
                if (light?.tag == "Light" && !light.enabled && light != null)
                    light.enabled = true;
            }

            foreach (GameObject lightLamp in lightLamps)
            {
                if (lightLamp?.tag == "EmissiveLight")
                    lightLamp.GetComponent<Renderer>().material = lampEmissions;

                if (lightLamp?.tag == "EmissiveWindow")
                    lightLamp.GetComponent<Renderer>().material =
                        windowEmissions;

                if (lightLamp?.tag == "EmissiveWallLight")
                    lightLamp.GetComponent<Renderer>().material =
                        WallSmallLampEmissions;

                if (lightLamp?.tag == "EmissiveTableLight")
                {
                    lightLamp.GetComponent<Renderer>().material =
                        tableLampEmissions;
                }

                if (lightLamp?.tag == "EmissiveCeelingLight")
                {
                    lightLamp.GetComponent<Renderer>().material =
                        ceelingLampEmissions;
                }
            }
        }

        public void TurnOffLights()
        {
            foreach (Light light in lights)
            {
                if (light.tag == "Light" && light.enabled)
                    light.enabled = false;
            }

            foreach (GameObject lightLamp in lightLamps)
            {
                if (lightLamp?.tag == "EmissiveLight")
                    lightLamp.GetComponent<Renderer>().material = lampDefault;

                if (lightLamp?.tag == "EmissiveWindow" && lightLamp != null)
                    lightLamp.GetComponent<Renderer>().material = windowDefault;

                if (lightLamp?.tag == "EmissiveWallLight" && lightLamp != null)
                    lightLamp.GetComponent<Renderer>().material =
                        WallSmallLampDefault;

                if (lightLamp?.tag == "EmissiveTableLight" && lightLamp != null)
                {
                    lightLamp.GetComponent<Renderer>().material =
                        tableLampDefault;
                }

                if (
                    lightLamp?.tag == "EmissiveCeelingLight" &&
                    lightLamp != null
                )
                {
                    lightLamp.GetComponent<Renderer>().material =
                        ceelingLampDefault;
                }
            }
        }
    }
}
