using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using TMPro;

public class Player : MonoBehaviour
{
    public TMP_Text money_text;
    public XROrigin xrOrigin;
    new public string name;
    public PalmUI palmUI;
    private const int DEFAULTMONEY = 3000000;
    private const int MAXBOARDNUM = 24;
    private const float T = 3f; // flight time for Move()
    private const float g = 9.8f;
    private InputDevice _targetDevice;

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
    private int islandCount = 0;
    public int IslandCount
    {
        get { return islandCount; }
        set { islandCount = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.money = DEFAULTMONEY;
        this.owned_boards = new List<Board>();
        if(name == "player") TryInit();
    }
    void TryInit()
    {
        var inputDevices = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics =
            InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, inputDevices);

        if (inputDevices.Count == 0)
        {
            return;
        }

        _targetDevice = inputDevices[0];
        Debug.Log("Get Left controller");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_targetDevice.isValid)
        {
            TryInit();
        }
        else
        {
            if (name == "cpu") return;
            money_text.text = "Money: " + money.ToString();
            _targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
            if (primaryButtonValue)
            {
                Debug.Log("pressing");
            }
            palmUI.SetActive(primaryButtonValue);
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
        //xrOrigin.transform.position = new Vector3(targetPosition.x, targetPosition.y + 10, targetPosition.z);
        transform.position = new Vector3(targetPosition.x, targetPosition.y + 1f, targetPosition.z);
        target.OnPlayer(this, next);
    }
}
