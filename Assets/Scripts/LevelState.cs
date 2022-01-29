using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public void ChangeDimension() 
    {
        Dimension nextDimension = this.GetNextDimension();
        this.RemoveTerrain();
        this.dimension = nextDimension;
        this.AddTerrain();
    }
    public string GetTerrainTag()
    {
        string currentDimensionString;
        if (this.dimension == Dimension.RED)
        {
            currentDimensionString = "Dimension0Terrain";
        }
        else if (this.dimension == Dimension.GREEN)
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

    private void RemoveTerrain()
    {
        GameObject[] terrain = GameObject.FindGameObjectsWithTag(this.GetTerrainTag());
        foreach(var piece in terrain) {
            piece.GetComponent<Renderer>().enabled = false;
            piece.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void AddTerrain()
    {
        GameObject[] terrain = GameObject.FindGameObjectsWithTag(this.GetTerrainTag());
        foreach(var piece in terrain) {
            piece.GetComponent<Renderer>().enabled = true;
            piece.GetComponent<BoxCollider2D>().enabled = true;
        }

    }



}