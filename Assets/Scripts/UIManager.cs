using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject palmUI;
    [SerializeField] private GameObject left_controller;

    private Vector3 lc_rotation;
    private bool lc_yrange;
    private bool lc_zrange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // palm UI control
        lc_rotation = left_controller.transform.rotation.eulerAngles;
        lc_yrange = lc_rotation.y > 50f && lc_rotation.y < 70f;
        lc_zrange = lc_rotation.z > 70f && lc_rotation.z < 90f;

        if (lc_yrange && lc_zrange)
        {
            palmUI.SetActive(true);
        }
        else
        {
            palmUI.SetActive(false);
        }
    }
}
