using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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
                break;
            case BoardType.Airport:
                break;
        }
    }

    public void BoardWork(Player player)
    {

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

    // Teleport type

    // Start type

    // Island type

    // Olympic type

    // Airport type
}
