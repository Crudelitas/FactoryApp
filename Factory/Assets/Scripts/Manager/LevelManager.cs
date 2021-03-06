﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;
    public GameObject[] TilePrefabs{ get{ return tilePrefabs; } }

    [SerializeField]
    private Transform machineHolder;
    public Transform MachineHolder { get; private set; }

    [SerializeField]
    private Transform map;
    public Transform Map { get; private set; }

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.x; }
    }
    
    public Dictionary<Point, TileScript> Tiles { get; set; }

    private string[]    mapData;
    private int         mapX, mapY;
    private Machine     selectedMachine;
    public  Machine     SelectedMachine { get { return selectedMachine; } }

    private void Start()
    {
        CreateLevel();
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

    public void PlaceMachines(GameObject machine)
    {
        foreach (Transform child in map)
        {
            if (child.gameObject.GetComponent<SpriteRenderer>().sprite.name == "Test_Spritesheet_7")
            {
                child.gameObject.GetComponent<SpriteRenderer>().sprite = tilePrefabs[0].GetComponent<SpriteRenderer>().sprite;
                GameObject newMachine = Instantiate(machine, child.transform.position, Quaternion.identity);
                newMachine.transform.SetParent(machineHolder);
                DisableGrid();
            }
        }
    }

    public void CancelSelection()
    {
        foreach (Transform child in map)
        {
            if (child.gameObject.GetComponent<SpriteRenderer>().sprite.name == "Test_Spritesheet_7")
            {
                child.gameObject.GetComponent<SpriteRenderer>().sprite = tilePrefabs[0].GetComponent<SpriteRenderer>().sprite;
                DisableGrid();
            }
        }
    }

    public void ActivateRotationMode()
    {
        foreach (Transform child in machineHolder)
        {
            if (child.tag == "machineTag")
            {
                child.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void DeactivateRotationMode()
    {
        foreach (Transform child in machineHolder)
        {
            if (child.tag == "machineTag")
            {
                child.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void DeleteMachines()
    {
        foreach (Transform child in machineHolder)
        {
            if (child.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite.name == "Test_Spritesheet_6")
            {
                Debug.Log("Deleted Machine: " + child.gameObject.name);
                Destroy(child.gameObject);
            }
        }
    }

    public void DeselectMachines()
    {
        foreach (Transform child in machineHolder)
        {
            if (child.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite.name == "Test_Spritesheet_6")
            {
                child.GetComponent<Machine>().BuildModeDeselect();
            }
        }
    }

    public void SelectMachinesToDelete(Machine machine)
    {
        selectedMachine = machine;
        machine.DeleteSelect();
    }

    public void MoveSelectMachine(Machine machine)
    {
        selectedMachine = machine;
        machine.BuildModeSelect();
    }

    public void MoveDeselectMachine()
    {
        if (selectedMachine != null)
        {
            selectedMachine.BuildModeDeselect();
        }

        selectedMachine = null;
    }
}
