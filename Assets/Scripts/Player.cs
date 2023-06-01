using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TMP_Text money_text;
    private const int default_money = 10000;
    private const int max_board = 20;

    private int money = 0;
    private int curr_pos = 0;
    private List<Board> owned_boards;

    // Start is called before the first frame update
    void Start()
    {
        this.money = default_money;
        this.owned_boards = new List<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        money_text.text = "Money: " + money.ToString();
    }

    public int getMoney()
    {
        return money;
    }
    public void addMoney(int m)
    {
        this.money += m;
    }
    
    public void addBoard(Board new_board)
    {
        owned_boards.Add(new_board);
    }

    public void Move(int dice_result)
    {

    }
}
