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

    private void Start() {
        foreach(Dimension dim in Enum.GetValues(typeof(Dimension))) {
            if (dim != this.dimension) {
                RemoveTerrain(dim);
            }
        }
    }

    public void ChangeDimension() 
    {
        Dimension nextDimension = this.GetNextDimension();
        this.RemoveTerrain(this.dimension);
        this.AddTerrain(nextDimension);
        this.dimension = nextDimension;
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

    private void RemoveTerrain(Dimension dim)
    {
        GameObject[] terrain = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(dim));
        foreach(var piece in terrain) {
            piece.GetComponent<Renderer>().enabled = false;
            piece.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void AddTerrain(Dimension dim)
    {
        GameObject[] terrain = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(dim));
        foreach(var piece in terrain) {
            piece.GetComponent<Renderer>().enabled = true;
            piece.GetComponent<BoxCollider2D>().enabled = true;
        }

    }



}