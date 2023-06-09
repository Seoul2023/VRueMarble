using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private const string MoneyInHand = "Money in hand: ";
    private int money_in_hand = 0;
    private const string MoneyRequired = "Money requiredL: ";
    private int money_required = 0;
    private const string MoneyRemain = "Money remain: ";
    private int money_remain = 0;

    [SerializeField] private Player player;

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
        DecisionUI.SetActive(false);
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

        // Decision UI
        if (money_remain < 0) OKButton.enabled = false;
        else OKButton.enabled = true;
    }
    
    private void UpdateDecisionUI(Board target)
    {
        // on toggle's value changed
        money_required = target.GetCost(DecisionToggles[0].enabled, DecisionToggles[1].enabled, DecisionToggles[2].enabled, DecisionToggles[3].enabled);
        money_remain = money_in_hand - money_required;
        DecisionMoneyRequired.text = MoneyRequired + money_required.ToString();
        DecisionMoneyRemain.text = MoneyRemain + money_remain.ToString();
    }

    public void OnWaitDecision(Board target)
    {
        money_in_hand = player.Money;
        foreach (Toggle t in DecisionToggles)
        {
            t.onValueChanged.AddListener(delegate
            {
                UpdateDecisionUI(target);
            });
        }
        // money_required = target.GetToll();
        // money_remain = money_in_hand - money_required;
        DecisionTitle.text = target.transform.name;
        DecisionMoneyInHand.text = MoneyInHand + money_in_hand.ToString();
        // DecisionMoneyRequired.text = MoneyRequired + money_required.ToString();
        // DecisionMoneyRemain.text = MoneyRemain + money_remain.ToString();
        DecisionUI.SetActive(true);
    }

    public void OnClickOK(Action next)
    {

    }
}
