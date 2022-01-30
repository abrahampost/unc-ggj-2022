using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public GameObject health;
    private List<GameObject> hearts = new List<GameObject>();
    private Vector3 offset = new Vector3(0, 2, 0);

    // Start is called before the first frame update
    void Start()
    {
        AddHealth();
        AddHealth();
        AddHealth();
    }

    void RemoveHealth() {

    }

    void AddHealth() {
        // Instantiate(health, transform.position, transform.rotation);
        // Instantiate(health, transform);
            GameObject newHealth = Instantiate(health, transform);
            newHealth.transform.position = new Vector3(0, 0, 0);
            hearts.Add(newHealth);
        if (hearts.Count > 0) {
            newHealth.transform.position = hearts[hearts.Count-1].transform.position + offset;
        }
        // hearts.Add(Instantiate(health, transform));
    }

}
