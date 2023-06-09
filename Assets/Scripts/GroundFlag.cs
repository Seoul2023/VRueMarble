using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFlag : MonoBehaviour
{
    public GameObject playerFlag, cpuFlag;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool active)
    {
        playerFlag.SetActive(active);
        cpuFlag.SetActive(active);
    }

    public void BuildPlayerFlag()
    {
        playerFlag.SetActive(true);
    }

    public void BuildCPUFlag()
    {
        cpuFlag.SetActive(true);
    }
}
