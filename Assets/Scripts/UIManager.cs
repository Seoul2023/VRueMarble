using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Palm UI Components")]
    [SerializeField] private GameObject PalmUI;
    [SerializeField] private TMP_Text PalmMoney;
    [Header("Decision UI Components")]
    [SerializeField] private GameObject DecisionUI;
    [SerializeField] private TMP_Text DecisionTitle;
    [SerializeField] private Toggle[] DecisionToggles;
    [SerializeField] private TMP_Text DecisionMoneyInHand;
    [SerializeField] private TMP_Text DecisionMoneyRequired;
    [SerializeField] private TMP_Text DecisionMoneyRemain;
    [SerializeField] private Button OKButton;
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
            PalmUI.SetActive(true);
        }
        else
        {
            PalmUI.SetActive(false);
        }
    }

    public void OnWaitDecision(Board target)
    {

    }
}
