using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPresets preset;
    [SerializeField, Range(0,24)] private float timeOfDay;
    [SerializeField] private float daySpeedInMinutes;
    private float converter;

    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    void Start(){
        converter = 145 / daySpeedInMinutes / 360;
    }

    /// Update is called every frame, if the MonoBehaviour is enabled.
    void Update(){
        if(preset == null){
            return;
        }
        if(Application.isPlaying){
            timeOfDay += Time.deltaTime * converter;
            timeOfDay %= 24;// Clamp between 0 - 24
            UpdateLighting(timeOfDay / 24f);
        }else{
            UpdateLighting(timeOfDay / 24f);
        }
    }

    void UpdateLighting(float timePercent){
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if(directionalLight != null){
            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));  
        }
    }

    
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    void OnValidate(){
        if(directionalLight != null){
            return;
        }

        if(RenderSettings.sun != null){
            directionalLight = RenderSettings.sun;
        }else{
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights){
                if(light.type == LightType.Directional){
                    directionalLight = light;
                    return;
                }
            }
        }



    }
}
