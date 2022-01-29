using UnityEngine;

public class WeaponController : MonoBehaviour {
    public GameObject primaryAbility;
    public GameObject secondaryAbility;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            primaryAbility.GetComponent<Ability>().use();
        }
        if (Input.GetMouseButtonDown(1))
        {
            secondaryAbility.GetComponent<Ability>().use();
        }
    }
}
