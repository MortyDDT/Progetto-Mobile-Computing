using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Vector2 posIniziale;
    public static float vi = 6f;
    public static float acceleration = 0f;
    private float incrementaVelocitaDi = 0.00005f;


    // Start is called before the first frame update
    void Start()
    {
        posIniziale = transform.position;
    }

    // Update is called once per frame
    void Update() {

        if (GameController.gameRunning) {
            if (transform.position.x >= -14.9f) {
                transform.position = new Vector2(transform.position.x - (vi + acceleration) * Time.deltaTime, transform.position.y);
                acceleration += incrementaVelocitaDi;
            }
            else {
                transform.position = posIniziale;
                acceleration += incrementaVelocitaDi;
            }
        }
        
    }
}
