using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class DisplayPalmUI : MonoBehaviour
{
    [SerializeField] private GameObject palmUI;
    private InputDevice targetDevice;

    private Vector3 rotation;
    private bool yrange;
    private bool zrange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotation = transform.rotation.eulerAngles;
        yrange = rotation.y > 50f && rotation.y < 70f;
        zrange = rotation.z > 70f && rotation.z < 90f;

        Debug.Log(rotation);
        if (yrange && zrange)
        {
            Debug.Log("Set Active");
            palmUI.SetActive(true);
        }
        else
        {
            Debug.Log("Set Deactive");
            palmUI.SetActive(false);
        }
    }
}
