using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public LevelState levelState;
    public Ability redPrimary;
    public Ability redSecondary;
    public Ability greenPrimary;
    public Ability greenSecondary;
    public Ability bluePrimary;
    public Ability blueSecondary;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (levelState.dimension == Dimension.RED)
            {
                redPrimary.use();
            }
            else if (levelState.dimension == Dimension.GREEN)
            {
                greenPrimary.use();
            } else if (levelState.dimension == Dimension.BLUE)
            {
                bluePrimary.use();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (levelState.dimension == Dimension.RED)
            {
                redSecondary.use();
            }
            else if (levelState.dimension == Dimension.GREEN)
            {
                greenSecondary.use();
            } else if (levelState.dimension == Dimension.BLUE)
            {
                blueSecondary.use();
            }
        }
    }
}
