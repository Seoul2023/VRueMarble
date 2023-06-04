using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PalmUI : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private TMP_Text MoneyTMP;
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
        lc_rotation = left_controller.transform.rotation.eulerAngles;
        lc_yrange = lc_rotation.y > 50f && lc_rotation.y < 70f;
        lc_zrange = lc_rotation.z > 70f && lc_rotation.z < 90f;

        if (lc_yrange && lc_zrange)
        {
            main.SetActive(true);
        }
        else
        {
            main.SetActive(false);
        }
    }
}
