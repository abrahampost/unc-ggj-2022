using UnityEngine;
using System;


public enum Dimension
{
    RED,
    GREEN,
    BLUE
}
public class LevelState : MonoBehaviour
{
    public bool redEnabled;
    public bool greenEnabled;
    public bool blueEnabled;
    public Dimension dimension = Dimension.RED;
    public Dimension startDimension = Dimension.RED;
    private SoundManager soundManager;

    GameObject[] redTerrain;
    GameObject[] greenTerrain;
    GameObject[] blueTerrain;

    GameObject[] redEnemy;
    GameObject[] greenEnemy;
    GameObject[] blueEnemy;

    private void Start() {
        this.startDimension = this.dimension;
        LoadTerrain();
        LoadEnemies();
        foreach(Dimension dim in Enum.GetValues(typeof(Dimension))) {
            if (dim != this.dimension) {
                RemoveTerrain(dim);
                RemoveEnemies(dim);
            }
        }
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>();
    }

    private void LoadTerrain() {
        this.redTerrain = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(Dimension.RED));
        this.greenTerrain = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(Dimension.GREEN));
        this.blueTerrain = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(Dimension.BLUE));
    }

    private void LoadEnemies() {
        this.redEnemy = GameObject.FindGameObjectsWithTag(this.GetEnemyTag(Dimension.RED));
        this.greenEnemy = GameObject.FindGameObjectsWithTag(this.GetEnemyTag(Dimension.GREEN));
        this.blueEnemy = GameObject.FindGameObjectsWithTag(this.GetEnemyTag(Dimension.BLUE));
    }

    public void ChangeDimension() 
    {
        Dimension nextDimension = this.GetNextDimension();
        this.RemoveTerrain(this.dimension);
        this.RemoveEnemies(this.dimension);
        this.AddTerrain(nextDimension);
        this.AddEnemies(nextDimension);
        this.dimension = nextDimension;
        soundManager.ChangeDimension();
    }
    public string GetTerrainTag(Dimension dim)
    {
        string currentDimensionString;
        if (dim == Dimension.RED)
        {
            currentDimensionString = "Dimension0Terrain";
        }
        else if (dim == Dimension.GREEN)
        {
            currentDimensionString = "Dimension1Terrain";
        }
        else
        {
            currentDimensionString = "Dimension2Terrain";
        }
        return currentDimensionString;
    }

    public string GetEnemyTag(Dimension dim)
    {
        string currentDimensionString;
        if (dim == Dimension.RED)
        {
            currentDimensionString = "Dimension0Enemy";
        }
        else if (dim == Dimension.GREEN)
        {
            currentDimensionString = "Dimension1Enemy";
        }
        else
        {
            currentDimensionString = "Dimension2Enemy";
        }
        return currentDimensionString;
    }

    private Dimension GetNextDimension() {
        if (dimension == Dimension.RED) {
            if (this.greenEnabled) return Dimension.GREEN;
            return Dimension.BLUE;
        } else if (dimension == Dimension.GREEN) {
            if (this.blueEnabled) return Dimension.BLUE;
            return Dimension.RED;
        } else {
            if (this.redEnabled) return Dimension.RED;
            return Dimension.GREEN;
        }
    }

    private GameObject[] GetTerrain(Dimension dim) {
        if (dim == Dimension.RED) {
            return this.redTerrain;
        } else if (dim == Dimension.GREEN) {
            return this.greenTerrain;
        } else {
            return this.blueTerrain;
        }
    }

    private GameObject[] GetEnemies(Dimension dim) {
        if (dim == Dimension.RED) {
            return this.redEnemy;
        } else if (dim == Dimension.GREEN) {
            return this.greenEnemy;
        } else {
            return this.blueEnemy;
        }
    }

    private void RemoveTerrain(Dimension dim)
    {
        GameObject[] terrain = GetTerrain(dim);
        foreach(var piece in terrain) {
            piece.GetComponent<Renderer>().enabled = false;
            piece.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void AddTerrain(Dimension dim)
    {
        GameObject[] terrain = GetTerrain(dim);
        foreach(var piece in terrain) {
            piece.GetComponent<Renderer>().enabled = true;
            piece.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void AddEnemies(Dimension dim) {
        GameObject[] enemies = GetEnemies(dim);
        foreach(var piece in enemies) {
            piece.SetActive(true);
        }
    }

    private void RemoveEnemies(Dimension dim) {
        GameObject[] enemies = GetEnemies(dim);
        foreach(var piece in enemies) {
            piece.SetActive(false);
        }
    }

    public void ResetTerrain() {
        if (this.dimension == this.startDimension) return;
        RemoveTerrain(this.dimension);
        RemoveEnemies(this.dimension);
        AddTerrain(this.startDimension);
        AddEnemies(this.startDimension);
        this.dimension = this.startDimension;
    }

}