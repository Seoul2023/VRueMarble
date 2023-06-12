using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [SerializeField] new private string name = "City";
    public string Name
    {
        get { return name; }
    }
    [SerializeField] private GroundFlag Flag;
    [SerializeField] private Structure Villa;
    [SerializeField] private Structure Building;
    [SerializeField] private Structure Hotel;
    [SerializeField] private int position;
    public int Position
    {
        get { return position;  }
    }
    [SerializeField] private int cost;
    [SerializeField] private int ground_rent;
    [SerializeField] private AudioSource bgAudio;

    public enum BoardType
    {
        City,
        Teleport,
        Start,
        Island,
        Olympic,
        Airport
    }
    public BoardType type;
    private Player owner = null;
    public Player Owner
    {
        get { return owner; }
        set { owner = value; }
    }
    private int toll;

    // Start is called before the first frame update
    void Start()
    {
        if (type == BoardType.City)
        {
            Villa.structure.SetActive(false);
            Building.structure.SetActive(false);
            Hotel.structure.SetActive(false);
            Flag.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPlayer(Player player, Action<Boolean> next)
    {
        if (player.name == "player" && bgAudio != null)
        {
            bgAudio.Play();
        }
        switch (type)
        {
            case BoardType.City:
                OnPlayerCity(player, next);
                break;
            case BoardType.Teleport:
                next(false);
                break;
            case BoardType.Start:
                next(false);
                break;
            case BoardType.Island:
                next(false);
                break;
            case BoardType.Olympic:
                next(true);
                break;
            case BoardType.Airport:
                next(true);
                break;
        }
    }

    public int BoardWork(Player playerInTurn, Player playerInWait)
    {
        switch (type)
        {
            case BoardType.City:
                int movedMoney = GetToll();
                Debug.Log("Board: Board work of city, money before: " + playerInTurn.Money.ToString());
                playerInTurn.Money -= movedMoney;
                playerInWait.Money += movedMoney;
                Debug.Log("Board: money after: " + playerInTurn.Money.ToString());
                return -1;
            case BoardType.Teleport:
                Debug.Log("Board: " + playerInTurn.ToString() + " teleported");
                return Random.Range(0, 24);
            case BoardType.Start:
                return -1;
            case BoardType.Island:
                Debug.Log("Board: " + playerInTurn.ToString() + " is in Island");
                playerInTurn.IslandCount = 3;
                return -1;
            case BoardType.Olympic:
                return -1;
            case BoardType.Airport:
                return -1;
        }
        return -1;
    }

    public void PauseAudio()
    {
        if (bgAudio != null) bgAudio.Pause();
    }

    // City type
    private void OnPlayerCity(Player player, Action<Boolean> next)
    {
        if (owner is null)
        {
            next(true);
        }
        else if (player.name == owner.name)
        {
            next(!Hotel.IsBuilt);
        }
        else
        {
            next(false);
        }
    }
    public int GetCost(bool ground, bool villa, bool building, bool hotel)
    {
        int sumOfCost = cost;
        if (!ground) return 0;
        if (villa) sumOfCost += Villa.Rent;
        if (building) sumOfCost += Building.Rent;
        if (hotel) sumOfCost += Hotel.Rent;
        return sumOfCost;
    }

    public int GetToll()
    {
        toll = ground_rent;
        if (Villa.IsBuilt) toll += Villa.Cost; else return toll;
        if (Building.IsBuilt) toll += Building.Cost; else return toll;
        if (Hotel.IsBuilt) toll += Hotel.Cost; else return toll;
        return toll;
    }

    public bool IsBuiltVilla()
    {
        return Villa.IsBuilt;
    }

    public bool IsBuiltBuilding()
    {
        return Building.IsBuilt;
    }

    public bool IsBuiltHotel()
    {
        return Hotel.IsBuilt;
    }

    public void BuyGround()
    {
        if(owner.name == "cpu")
        {
            Flag.BuildCPUFlag();
        }
        else
        {
            Flag.BuildPlayerFlag();
        }
    }

    public void BuildVilla()
    {
        Villa.structure.SetActive(true);
        Villa.SetColor(owner.name != "cpu");
    }

    public void BuildBuilding()
    {
        Building.structure.SetActive(true);
        Building.SetColor(owner.name != "cpu");
    }

    public void BuildHotel()
    {
        Hotel.structure.SetActive(true);
        Hotel.SetColor(owner.name != "cpu");
    }

    public void SetRent2x()
    {
        ground_rent *= 2;
        if (IsBuiltVilla()) Villa.Rent *= 2;
        if (IsBuiltBuilding()) Building.Rent *= 2;
        if (IsBuiltHotel()) Hotel.Rent *= 2;
    }

    // Teleport type

    // Start type

    // Island type

    // Olympic type

    // Airport type
}
