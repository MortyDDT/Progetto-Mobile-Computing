using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectCoin : MonoBehaviour{
    private AudioSource collectSound;

    private void Start() {
        collectSound = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.tag == "Player") {
            collectSound.Play();
            ScoreSystem.theScore += 10;
            Destroy(gameObject, collectSound.clip.length);
        }

    }

    // Update is called once per frame
    void Update() {

        if (transform.position.x < -12)
            Destroy(gameObject);
    }

}
