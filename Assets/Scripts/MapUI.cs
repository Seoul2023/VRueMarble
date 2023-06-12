using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapUI : MonoBehaviour
{
    public GameObject main;
    [SerializeField] TMP_Text title;
    [SerializeField] Image villaImage;
    [SerializeField] Image buildingImage;
    [SerializeField] Image hotelImage;
    [SerializeField] Image playerImage;
    [SerializeField] Image cpuImage;
    public Button[] tiles;

    private Board[] map;
    private ColorBlock colorBlock;
    // Start is called before the first frame update
    void Start()
    {
        main.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn(Board[] m, string type, int playerPos, int cpuPos)
    {
        map = m;
        Board cur;
        Vector3 cur_pos;

        // title
        if (type == "olympic") title.text = "Where will the next Olympic be held?";
        else if (type == "airport") title.text = "Where will you fly to?";
        else title.text = "Map";
        // board
        for (int i = 0; i < map.Length; i++)
        {
            cur = map[i];
            cur_pos = new Vector3(tiles[i].transform.position.x, tiles[i].transform.position.y, tiles[i].transform.position.z);
            colorBlock = tiles[i].colors;
            if (cur.type != Board.BoardType.City) continue;
            // set color
            //if (cur.Owner.name == "player") colorBlock.normalColor = new Color(0f, 0f, 1f, 0.5f);
            //else if (cur.Owner.name == "cpu") colorBlock.normalColor = new Color(1f, 0f, 0f, 0.5f);
            //tiles[i].colors = colorBlock;
            // set structure images
            if(cur.IsBuiltVilla())
            {
                Image villa = Image.Instantiate(villaImage);
                villa.transform.position = new Vector3(cur_pos.x - 5f, cur_pos.y + 5f, cur_pos.z);
            }
            if(cur.IsBuiltBuilding())
            {
                Image building = Image.Instantiate(buildingImage);
                building.transform.position = new Vector3(cur_pos.x, cur_pos.y + 5f, cur_pos.z);
            }
            if (cur.IsBuiltHotel())
            {
                Image hotel = Image.Instantiate(hotelImage);
                hotel.transform.position = new Vector3(cur_pos.x + 5f, cur_pos.y + 5f, cur_pos.z);
            }
        }
        // player & cpu
        Vector3 pos = tiles[playerPos].transform.position;
        playerImage.transform.position = new Vector3(pos.x, pos.y, pos.z);
        pos = tiles[cpuPos].transform.position;
        cpuImage.transform.position = new Vector3(pos.x, pos.y, pos.z);

        main.SetActive(true);
    }
}
