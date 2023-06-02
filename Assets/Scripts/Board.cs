using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private string city_name;
    private Structure[] buildings;
    private int location;
    private int toll;

    [SerializeField] private int ground_rent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetName()
    {
        return city_name;
    }

    public int GetLocation()
    {
        return location;
    }

    public int GetToll()
    {
        return toll;
    }
}
