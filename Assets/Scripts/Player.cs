using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using TMPro;

public class Player : MonoBehaviour
{
    public TMP_Text money_text;
    public XROrigin xrOrigin;
    public string name;
    private const int DEFAULTMONEY = 10000;
    private const int MAXBOARDNUM = 24;
    private const float T = 3f; // flight time for Move()
    private const float g = 9.8f;

    private int money = 0;
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    private int curr_pos = 0;
    public int CurrentPosition
    {
        get { return curr_pos; }
        set { curr_pos = (curr_pos + value) % MAXBOARDNUM; }
    }
    private List<Board> owned_boards;

    // Start is called before the first frame update
    void Start()
    {
        if (name == "player")
        {
            this.money = DEFAULTMONEY;
            this.owned_boards = new List<Board>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (name == "player")
        {
            money_text.text = "Money: " + money.ToString();
        }
    }

    public void AddBoard(Board b)
    {
        owned_boards.Add(b);
        b.Owner = this;
    }

    public void Move(Board target, Action<Boolean> next)
    {
        Debug.Log("Player: Move");
        //Vector3 direction = target.transform.position - transform.position;
        //float vx = direction.x / T;
        //float vy = direction.y / T;
        //float vz = T * g / 2;
        //Vector3 launchVelocity = new Vector3(vx, vy, vz);

        // teleport move
        // need to add offsets;
        Vector3 targetPosition = target.transform.position;
        xrOrigin.transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z + 10);
        target.OnPlayer(this, next);
    }
}
