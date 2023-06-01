using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum State
    {
        player_rolling,
        //player_moving,
        player_waiting,
        player_do,
        player_end,
        cpu_rolling,
        cpu_moving,
        cpu_waiting,
        cpu_do,
        cpu_end
    }
    private State state;
    private int dice_result;

    [SerializeField] private Player player, cpu;
    [SerializeField] private Board[] map;
    [SerializeField] private Dice dice;
    [SerializeField] private SkyboxChanger skybox_changer;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.player_rolling:
                break;
            //case State.player_moving:
            //    break;
            case State.player_waiting:
                break;
            case State.player_do:
                break;
            case State.player_end:
                break;
        }
    }

    void Init()
    {
        state = State.player_rolling;
    }

    public void OnDiceRolled() 
    {
        if (state == State.player_rolling || state == State.cpu_rolling) 
        {
            dice_result = dice.GetResult();
            player.Move(dice_result);
            state = State.player_waiting;
        }
    }

    public void OnPlayerDecided()
    {

    }
}
