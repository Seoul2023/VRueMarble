using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        none,
        player_rolling,
        player_moving,
        player_waiting,
        player_do,
        player_end,
        cpu_rolling,
        cpu_moving,
        cpu_waiting,
        cpu_do,
        cpu_end
    }
    private const int MAXBOARDNUM = 24;

    private GameState state = GameState.none;
    private int diceResult = 0;
    private int target_pos = 0;

    [SerializeField] private Player player, cpu;
    private Player playerInTurn, playerInWait;
    [SerializeField] private GameObject entire_map;
    private Board[] map;
    [SerializeField] private Dice dice;
    [SerializeField] private SkyboxChanger skyboxChanger;
    [SerializeField] private DecisionUI decisionUI;

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
            case GameState.player_rolling:
                if(dice.IsRolled())
                {
                    Debug.Log("GM: dice rolled!");
                    diceResult = dice.Result;
                    MovePlayer();
                }
                break;
            case GameState.player_moving:
                break;
            case GameState.player_waiting:
                break;
            case GameState.player_do:
                break;
            case GameState.player_end:
                break;
        }
    }

    void Init()
    {
        Debug.Log("GM: Init");
        state = GameState.player_rolling;
        decisionUI.OKButton.onClick.AddListener(EndPlayerDecision);
        map = entire_map.GetComponentsInChildren<Board>();
        playerInTurn = player; playerInWait = cpu;
    }

    private void MovePlayer()
    {
        if (state != GameState.player_rolling && state != GameState.cpu_rolling) 
        {
            Debug.Log("GM: Error! Wrong state in MovePlayer()");
            EndTurn();
        }
        Debug.Log("GM: Start player move");
        playerInTurn = (state == GameState.player_rolling) ? player : cpu;
        playerInWait = (state == GameState.player_rolling) ? cpu : player;
        state = (state == GameState.player_rolling) ? GameState.player_moving : GameState.cpu_moving;

        target_pos = (playerInTurn.CurrentPosition + diceResult) % MAXBOARDNUM;
        playerInTurn.CurrentPosition = target_pos;
        skyboxChanger.SetSkybox(target_pos);
        playerInTurn.Move(map[target_pos], (b) => { StartPlayerDecision(b); });
    }

    private void StartPlayerDecision(bool needToWait)
    {
        Debug.Log("GM: Start Player Decision");
        if (state == GameState.player_moving)
        {
            state = GameState.player_waiting;
            if (needToWait)
            {
                decisionUI.TurnOn(player, map[target_pos]);
            }
            else
            {
                DoBoardWork();
            }
        }
        else if (state == GameState.cpu_moving)
        {
            state = GameState.cpu_waiting;
        }
        else
        {
            Debug.Log("GM: Error! Wrong state in StartPlayerDecision()");
            EndTurn();
        }
    }
    
    private void EndPlayerDecision()
    {
        if(state != GameState.player_waiting && state != GameState.cpu_waiting)
        {
            Debug.Log("GM: Improper state for EndPlayerDecision");
        }
        Debug.Log("GM: End player decision");
        int remainedMoney = decisionUI.Remain;
        List<bool> decisions = decisionUI.GetDecisions();
        decisionUI.TurnOff();

        if(decisions.Count < 4)
        {
            Debug.Log("GM: Not enough decisions in EndPlayerDecision");
            EndTurn();
        }

        player.Money = remainedMoney;
        if (decisions[0]) 
        { 
            player.AddBoard(map[target_pos]);
            map[target_pos].BuyGround();
        }
        if (decisions[1]) map[target_pos].BuildVilla();
        if (decisions[2]) map[target_pos].BuildBuilding();
        if (decisions[3]) map[target_pos].BuildHotel();

        EndTurn();
    }

    private void DoBoardWork()
    {
        state = GameState.player_do;
        int ret = map[target_pos].BoardWork(playerInTurn, playerInWait);
        if (ret != -1)
        {
            playerInTurn.Move(map[ret], (b) => { EndTurn(); });
        } else { EndTurn(); }
    }

    private void EndTurn()
    {
        Debug.Log("GM: End turn");
        state = GameState.player_end;
        if(IsEnd())
        {
            Debug.Log("GM: Game End!");
        }
        else
        {
            dice.transform.position = player.transform.position;
            dice.SetStateBeforeReady();
            state = GameState.player_rolling;
        }
    }

    public bool IsEnd()
    {
        return player.Money < 0 || cpu.Money < 0;
    }
}
