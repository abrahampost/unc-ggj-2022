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

    private void Start() {
        this.startDimension = this.dimension;
        LoadTerrain();
        foreach(Dimension dim in Enum.GetValues(typeof(Dimension))) {
            if (dim != this.dimension) {
                RemoveTerrain(dim);
            }
        }
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>();
    }

    private void LoadTerrain() {
        this.redTerrain = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(Dimension.RED));
        this.greenTerrain = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(Dimension.GREEN));
        this.blueTerrain = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(Dimension.BLUE));
    }

    public void ChangeDimension() 
    {
        Dimension nextDimension = this.GetNextDimension();
        this.RemoveTerrain(this.dimension);
        this.AddTerrain(nextDimension);
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

    private void RemoveTerrain(Dimension dim)
    {
        GameObject[] terrain = GetTerrain(dim);
        foreach(var piece in terrain) {
            piece.GetComponent<Renderer>().enabled = false;
            piece.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void AddTerrain(Dimension dim)
    {
        GameObject[] terrain = GetTerrain(dim);
        foreach(var piece in terrain) {
            piece.GetComponent<Renderer>().enabled = true;
            piece.GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    public void ResetTerrain() {
        if (this.dimension == this.startDimension) return;
        RemoveTerrain(this.dimension);
        AddTerrain(this.startDimension);
        this.dimension = this.startDimension;
    }

}