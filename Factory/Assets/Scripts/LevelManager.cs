using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    public GameObject[] TilePrefabs
    {
        get
        {
            return tilePrefabs;
        }
    }

    [SerializeField]
    private Transform map;

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.x; }
    }
    
    public Dictionary<Point, TileScript> Tiles { get; set; }

    string[]    mapData;
    int         mapX, mapY;

    // Use this for initialization
    void Start () 
    {
        CreateLevel();
    }
	
	// Update is called once per frame
	void Update () 
    {
    }

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        mapData = ReadLevelText("Level");
        mapX = mapData[0].ToCharArray().Length;
        mapY = mapData.Length;


        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapX; x++)
            {
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map);

    }

    private string[] ReadLevelText(string floor)
    {
        TextAsset bindData = Resources.Load(floor) as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    public void ShowGrid()
    {
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapX; x++)
            {
                if (Tiles[new Point(x, y)].GetComponentInChildren<SpriteRenderer>().sprite.name == "Test_Spritesheet_0")
                {
                    Tiles[new Point(x, y)].GetComponentInChildren<SpriteRenderer>().sprite = tilePrefabs[1].GetComponentInChildren<SpriteRenderer>().sprite;
                }
            }
        }
    }

    public void DisableGrid()
    {
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapX; x++)
            {
                if (Tiles[new Point(x, y)].GetComponentInChildren<SpriteRenderer>().sprite.name == "Test_Spritesheet_1")
                {
                    Tiles[new Point(x, y)].GetComponentInChildren<SpriteRenderer>().sprite = tilePrefabs[0].GetComponentInChildren<SpriteRenderer>().sprite;
                }
            }
        }
    }

    private void Rotate()
    {
        if(Input.GetMouseButton(0)){

            Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 mouseRay = Camera.main.ScreenToWorldPoint(transform.position);
            RaycastHit2D rayHit = Physics2D.Raycast(fingerPosition, Vector2.zero);

            if(rayHit.collider.GetComponentInChildren<Machine>() != null)
            {
                Debug.Log("Hitted: " + rayHit.collider.GetComponentInChildren<SpriteRenderer>().sprite.name);
                rayHit.collider.GetComponentInChildren<Machine>().Rotate();
            }
            else
            {
                Debug.Log("Ground Hitted");
            }
        }
    }

    private void Move()
    {
        

        /*
        bool selected = false;
        RaycastHit2D firstRay;

        Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 mouseRay = Camera.main.ScreenToWorldPoint(transform.position);
        RaycastHit2D rayHit = Physics2D.Raycast(fingerPosition, Vector2.zero);

        if (Input.GetMouseButton(0) && !selected)
        {
            firstRay = rayHit;

            if ((rayHit.collider.GetComponentInChildren<Machine>() != null))
            {
                string spriteName = rayHit.collider.GetComponentInChildren<SpriteRenderer>().sprite.name;

                rayHit.collider.GetComponentInChildren<SpriteRenderer>().sprite = tilePrefabs[6].GetComponentInChildren<SpriteRenderer>().sprite;

                selected = true;

                //Tiles[new Point(point.X, point.y)].GetComponentInChildren<SpriteRenderer>().sprite = tilePrefabs[7].GetComponentInChildren<SpriteRenderer>().sprite;
            } 
        } 
        else if(Input.GetMouseButton(0) && selected)
        {
            if ((rayHit.collider.GetComponentInChildren<SpriteRenderer>().sprite.name == "Test_Spritesheet_0") && selected)
            {
                selected = false;
                firstRay.collider.GetComponentInChildren<SpriteRenderer>().sprite = tilePrefabs[0].GetComponentInChildren<SpriteRenderer>().sprite;
                rayHit.collider.GetComponentInChildren<SpriteRenderer>().sprite = tilePrefabs[7].GetComponentInChildren<SpriteRenderer>().sprite;
            }
        }
        */
    }
}
