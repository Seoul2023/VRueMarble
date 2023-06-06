using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DecisionUI : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private TMP_Text TitleTMP;
    [SerializeField] private Toggle[] Toggles;
    [SerializeField] private TMP_Text MoneyInHandTMP;
    [SerializeField] private TMP_Text MoneyRequiredTMP;
    [SerializeField] private TMP_Text MoneyRemainTMP;
    public Button OKButton;

    private const string MoneyInHand = "Money in hand: ";
    private int moneyInHand = 0;
    private const string MoneyRequired = "Money required: ";
    private int moneyRequired = 0;
    private const string MoneyRemain = "Money remain: ";
    private int moneyRemain = 0;
    public int Remain
    {
        get { return moneyRemain; }
    }
    private Board target = null;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        main.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            LockToggles(target);
            moneyRequired = SumUpCosts(target);
            moneyRemain = moneyInHand - moneyRequired;
            MoneyRequiredTMP.text = MoneyString(MoneyRequired, moneyRequired);
            MoneyRemainTMP.text = MoneyString(MoneyRemain, moneyRemain);
        }
    }

    private string MoneyString(string pre, int post)
    {
        return pre + post.ToString();
    }

    private void LockToggles(Board b)
    {
        if (b.Owner != null)
        {
            Toggles[0].enabled = true;
            Toggles[0].interactable = false;
        }
        if (b.IsBuiltVilla())
        {
            Toggles[1].enabled = true;
            Toggles[1].interactable = false;
        }
        if (b.IsBuiltBuilding())
        {
            Toggles[2].enabled = true;
            Toggles[2].interactable = false;
        }
        if (b.IsBuiltHotel())
        {
            Toggles[3].enabled = true;
            Toggles[3].interactable = false;
        }
    }

    private void UnlockToggles()
    {
        foreach(Toggle t in Toggles)
        {
            t.enabled = true;
            t.interactable = true;
        }
    }

    private int SumUpCosts(Board b)
    {
        int sumOfCosts = 0;

        return sumOfCosts;
    }

    public void TurnOn(Player p, Board b)
    {
        moneyInHand = p.Money;
        MoneyInHandTMP.text = MoneyString(MoneyInHand, moneyInHand);
        target = b;
        main.SetActive(true);
    }

    public void TurnOff()
    {
        main.SetActive(false);
        target = null;
        UnlockToggles();
    }

    public List<bool> GetDecisions()
    {
        List<bool> ret = new();
        foreach(Toggle t in Toggles)
        {
            ret.Add(t.interactable && t.enabled);
        }

        return ret;
    }
}
