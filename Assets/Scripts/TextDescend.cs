using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDescend : MonoBehaviour
{

    private GameObject ship;

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("Ship");    
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = ship.transform.position + Vector3.forward * 10;
        // transform.Translate(Vector3.down * 10 * Time.deltaTime);
    }
}
