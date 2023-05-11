using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceBehaviorScript : MonoBehaviour
{
    private const float THRESHHOLD = 0.70f;
    public enum State
    {
        READY,
        ROLLING,
        ROLLED
    }
    private int[] faces = { 1, 2, 3, 4, 5, 6 };
    private State state = State.ROLLED;
    private int result = 0;

    [SerializeField] private Rigidbody rigid;
    public Vector3 startPosition = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        state = State.ROLLED;
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.ROLLING && IsStatic())
        {
            SetResult();
            Debug.Log(result);
        }
    }

    private bool IsStatic()
    {
        return rigid.velocity.sqrMagnitude < 0.000001f && rigid.angularVelocity.sqrMagnitude < 0.000001f;
    }

    private int CalculateResultFromAngle()
    {
        if (transform.forward.y > THRESHHOLD) return 1;
        if (transform.up.y > THRESHHOLD) return 2;
        if (transform.right.y > THRESHHOLD) return 3;
        if (transform.forward.y < -THRESHHOLD) return 4;
        if (transform.up.y < -THRESHHOLD) return 5;
        if (transform.right.y < -THRESHHOLD) return 6;

        return 0;
    }

    public int GetState()
    {
        return (int)state;
    }

    public void SetStateReady()
    {
        state = State.READY;
    }

    public void SetStateRolling()
    {
        state = State.ROLLING;
    }

    public int GetResult()
    {
        if (state != State.ROLLED) return -1;
        return result;
    }

    public void SetResult()
    {
        result = faces[CalculateResultFromAngle() - 1];
        state = State.ROLLED;
    }
}
