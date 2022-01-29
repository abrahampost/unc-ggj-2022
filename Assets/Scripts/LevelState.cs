using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dimension { 
    RED,
    GREEN,
    BLUE
}
public class LevelState : ScriptableObject
{
    public Dimension dimension = Dimension.RED;
}
