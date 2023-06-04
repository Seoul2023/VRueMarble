using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public enum StructureType
    {
        Villa,
        Building,
        Hotel
    }
    public GameObject structure;
    [SerializeField] private StructureType type;
    [SerializeField] private int cost;
    private bool is_built = false;
    public bool IsBuilt
    {
        get { return is_built; }
        set { is_built = value; }
    }
    public int Cost
    {
        get { return cost; }
    }
    [SerializeField] private int rent;
    public int Rent
    {
        get { return rent; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
