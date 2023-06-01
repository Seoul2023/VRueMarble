using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public enum Cities
    {
        Default,
        Seoul,
        Tokyo,
        Paris,
        Newyork
    }    
    public Material[] skyboxMaterials;

    private Cities currentCity = Cities.Default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSkybox(Cities newCity)
    {
        Debug.Log("set skybox: " + newCity);
        currentCity = newCity;
    }

    public void setSeoul()
    {
        currentCity = Cities.Seoul;
        RenderSettings.skybox = skyboxMaterials[(int)currentCity];
    }

    public void setTokyo()
    {
        currentCity = Cities.Tokyo;
        RenderSettings.skybox = skyboxMaterials[(int)currentCity];
    }
}
