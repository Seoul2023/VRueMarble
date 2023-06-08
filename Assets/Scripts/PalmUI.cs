using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;

public class PalmUI : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private TMP_Text MoneyTMP;
    [SerializeField] private GameObject left_controller;

    private InputDevice lc;
    private Vector3 lc_rotation;
    private bool lc_yrange;
    private bool lc_zrange;
    // Start is called before the first frame update
    void Start()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            if ((device.characteristics & InputDeviceCharacteristics.Controller) != 0 &&
                (device.characteristics & InputDeviceCharacteristics.Left) != 0)
            {
                lc = device;
                Debug.Log(lc.ToString());
                break;
            }
        }
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
