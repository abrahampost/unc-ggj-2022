using UnityEngine;

public class WeaponController : MonoBehaviour {
    public Ability primaryAbility;
    public Ability secondaryAbility;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            primaryAbility.use();
        }
        if (Input.GetMouseButtonDown(1))
        {
            secondaryAbility.use();
        }
    }
}
