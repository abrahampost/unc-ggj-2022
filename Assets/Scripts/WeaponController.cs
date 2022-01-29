using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public LevelState levelState;
    public GameObject redPrimary;
    public GameObject redSecondary;
    public GameObject greenPrimary;
    public GameObject greenSecondary;
    public GameObject bluePrimary;
    public GameObject blueSecondary;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (levelState.dimension == Dimension.RED)
            {
                redPrimary.GetComponent<Ability>().use();
            }
            else if (levelState.dimension == Dimension.GREEN)
            {
                greenPrimary.GetComponent<Ability>().use();
            } else if (levelState.dimension == Dimension.BLUE)
            {
                bluePrimary.GetComponent<Ability>().use();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (levelState.dimension == Dimension.RED)
            {
                redSecondary.GetComponent<Ability>().use();
            }
            else if (levelState.dimension == Dimension.GREEN)
            {
                greenSecondary.GetComponent<Ability>().use();
            } else if (levelState.dimension == Dimension.BLUE)
            {
                blueSecondary.GetComponent<Ability>().use();
            }
        }
    }
}
