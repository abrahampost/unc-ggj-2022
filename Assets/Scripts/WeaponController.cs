using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private LevelState levelState;
    public Ability redPrimary;
    public Ability redSecondary;
    public Ability greenPrimary;
    public Ability greenSecondary;
    public Ability bluePrimary;
    public Ability blueSecondary;
    private Dimension currentDimension;

    private void Start()
    {
        levelState = GameObject.Find("Game").GetComponent<LevelState>();
        if (levelState.dimension == Dimension.RED)
        {
            currentDimension = Dimension.RED;
        }
        else if (levelState.dimension == Dimension.GREEN)
        {
            currentDimension = Dimension.GREEN;
        }
        else if (levelState.dimension == Dimension.BLUE)
        {
            currentDimension = Dimension.BLUE;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (levelState.dimension == Dimension.RED)
            {
                if (redPrimary) redPrimary.use();
            }
            else if (levelState.dimension == Dimension.GREEN)
            {
                if (greenPrimary) greenPrimary.use();
            }
            else if (levelState.dimension == Dimension.BLUE)
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
            if (currentDimension == Dimension.RED)
            {
                if (redPrimary) redSecondary.use();
            }
            else if (currentDimension == Dimension.GREEN)
            {
                if (greenSecondary) greenSecondary.use();
            }
            else if (currentDimension == Dimension.BLUE)
            {
                if (blueSecondary) blueSecondary.use();
            }
        }
        else
        {
            if (levelState.dimension == Dimension.RED)
            {
                currentDimension = Dimension.RED;
            }
            else if (levelState.dimension == Dimension.GREEN)
            {
                currentDimension = Dimension.GREEN;
            }
            else if (levelState.dimension == Dimension.BLUE)
            {
                currentDimension = Dimension.BLUE;
            }
            if (redPrimary) redSecondary.notUsing();
            if (greenSecondary) greenSecondary.notUsing();
            if (blueSecondary) blueSecondary.notUsing();
        }
    }
}
