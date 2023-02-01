using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameRunning) {
            transform.position = new Vector2(transform.position.x - (Ground.vi + Ground.acceleration) * Time.deltaTime, transform.position.y);

            if (transform.position.x < -25)
                Destroy(gameObject);
        }
        
    }
}
