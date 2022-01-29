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
}