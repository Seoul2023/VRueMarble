using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    private const float THRESHHOLD = 0.70f;
    public enum DiceState
    {
        BEFORE_READY,
        READY,      // grabbed(onSelect) by hand
        ROLLING,    // exited select
        ROLLED
    }
    private int[] faces = { 1, 2, 3, 4, 5, 6 };
    private DiceState state = DiceState.BEFORE_READY;
    private int result = 0;
    public int Result
    {
        get { return result; }
    }

    [SerializeField] private Rigidbody rigid;
    public Vector3 startPosition = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        state = DiceState.BEFORE_READY;
        Debug.Log("Dice: Start");
    }

    // Update is called once per frame
    void Update()
    {
        if(state == DiceState.ROLLING && rigid.IsSleeping())
        {
            Debug.Log("Dice: Dice is static now.");
            SetResult();
            Debug.Log("Dice: The result is " + result.ToString());
        }
    }

    private int CalculateResultFromAngle()
    {
        if (transform.forward.y > THRESHHOLD) return 1;
        if (transform.up.y > THRESHHOLD) return 2;
        if (transform.right.y > THRESHHOLD) return 4;
        if (transform.forward.y < -THRESHHOLD) return 6;
        if (transform.up.y < -THRESHHOLD) return 5;
        if (transform.right.y < -THRESHHOLD) return 3;

        return 0;
    }

    private void SetResult()
    {
        Debug.Log("Dice: Set Result.");
        result = faces[CalculateResultFromAngle() - 1];
        state = DiceState.ROLLED;
    }

    public bool IsRolled()
    {
        return state == DiceState.ROLLED;
    }

    public void SetStateBeforeReady()
    {
        state = DiceState.BEFORE_READY;
    }

    public void SetStateReady()
    {
        state = DiceState.READY;
    }

    public void SetStateRolling()
    {
        state = DiceState.ROLLING;
    }
}
