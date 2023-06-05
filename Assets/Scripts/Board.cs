using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    private string city_name;
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
        Villa.structure.SetActive(false);
        Building.structure.SetActive(false);
        Hotel.structure.SetActive(false);
        Flag.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void OnPlayer(Player player, Action<Boolean> next)
    {
        if (owner is null)
        {

            next(true);
        }
        else if (player.transform.name == owner.transform.name)
        {

            next(false);
        }
        else
        {
            next(false);
        }
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
        if(owner.transform.name == "cpu")
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
        Villa.SetColor(owner.transform.name != "cpu");
    }

    public void BuildBuilding()
    {
        Building.structure.SetActive(true);
        Building.SetColor(owner.transform.name != "cpu");
    }

    public void BuildHotel()
    {
        Hotel.structure.SetActive(true);
        Hotel.SetColor(owner.transform.name != "cpu");
    }
}
