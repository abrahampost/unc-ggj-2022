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
    public Dimension startDimension = Dimension.RED;
    public Dimension curDimension = Dimension.RED;
    private SoundManager soundManager;

    private GameObject[] dimension0Objects;
    private GameObject[] dimension1Objects;
    private GameObject[] dimension2Objects;

    private void Start() {
        dimension0Objects = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(Dimension.RED));
        dimension1Objects = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(Dimension.GREEN));
        dimension2Objects = GameObject.FindGameObjectsWithTag(this.GetTerrainTag(Dimension.BLUE));
        foreach(Dimension dim in Enum.GetValues(typeof(Dimension))) {
            if (dim != this.curDimension) {
                RemoveTerrain(dim);
            }
        }
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>();
    }

    public void ChangeDimension() 
    {
        Dimension nextDimension = this.GetNextDimension();
        this.RemoveTerrain(this.curDimension);
        this.AddTerrain(nextDimension);
        this.curDimension = nextDimension;
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
        if (this.curDimension == Dimension.RED) {
            if (this.greenEnabled) return Dimension.GREEN;
            return Dimension.BLUE;
        } else if (this.curDimension == Dimension.GREEN) {
            if (this.blueEnabled) return Dimension.BLUE;
            return Dimension.RED;
        } else {
            if (this.redEnabled) return Dimension.RED;
            return Dimension.GREEN;
        }
    }

    private void RemoveTerrain(Dimension dim)
    {
        GameObject[] terrain = this.curDimension == Dimension.RED ? dimension0Objects : this.curDimension == Dimension.GREEN ? dimension1Objects : dimension2Objects;
        foreach(var piece in terrain) {
            piece.GetComponent<Renderer>().enabled = false;
            piece.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void AddTerrain(Dimension dim)
    {
        GameObject[] terrain = this.curDimension == Dimension.RED ? dimension0Objects : this.curDimension == Dimension.GREEN ? dimension1Objects : dimension2Objects;
        foreach(var piece in terrain) {
            piece.GetComponent<Renderer>().enabled = true;
            piece.GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    public void ResetTerrain() {
        if (curDimension == startDimension) return;
        RemoveTerrain(this.curDimension);
        AddTerrain(this.startDimension);
        this.curDimension = this.startDimension;
    }


}