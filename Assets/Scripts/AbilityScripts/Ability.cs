using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour
{
    public abstract bool canUse();
    public abstract void notUsing();
    public abstract void use();
}