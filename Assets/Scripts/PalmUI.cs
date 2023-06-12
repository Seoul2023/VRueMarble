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
    [SerializeField] private MapUI mapUI;
    [SerializeField] private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {

    }
// Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool b)
    {
        main.SetActive(b);
    }
}
