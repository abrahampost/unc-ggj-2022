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
            if (levelState.curDimension == Dimension.RED)
            {
                if (redPrimary) redPrimary.use();
            }
            else if (levelState.curDimension == Dimension.GREEN)
            {
                if (greenPrimary) greenPrimary.use();
            } else if (levelState.curDimension == Dimension.BLUE)
            {
                if (bluePrimary) bluePrimary.use();
            }
        }
        else
        {
            if (redPrimary) redPrimary.notUsing();
            if (greenPrimary) greenPrimary.notUsing();
            if (bluePrimary) bluePrimary.notUsing();
        }
        if (Input.GetMouseButton(1))
        {
            if (levelState.curDimension == Dimension.RED)
            {
                if (redPrimary) redSecondary.use();
            }
            else if (levelState.curDimension == Dimension.GREEN)
            {
                if (greenSecondary) greenSecondary.use();
            } else if (levelState.curDimension == Dimension.BLUE)
            {
                if (blueSecondary) blueSecondary.use();
            }
        }
        else
        {
            if (redPrimary) redSecondary.notUsing();
            if (greenSecondary) greenSecondary.notUsing();
            if (blueSecondary) blueSecondary.notUsing();
        }
    }
}
