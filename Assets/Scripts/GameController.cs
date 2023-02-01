using System.Collections;
using UnityEngine;


public class GameController : MonoBehaviour {
    private float spawnTimer;
    public float spawnDelay;
    public GameObject coins1;
    public GameObject coins2;
    public GameObject coins3;
    public GameObject coins4;
    public GameObject spikes1;
    public GameObject spikes2;
    public GameObject platformsWCoins1;
    public GameObject platformsWCoins2;
    public GameObject platformsWSpikes;
    public GameObject Obstacle1;
    public GameObject Obstacle2;
    public GameObject Ramp;

    public GameObject prefab;
    public Transform table;

    public static bool gameRunning = true;


    private void spawn() {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay) {
            spawnTimer -= spawnDelay;

            int rng = Random.Range(0, 15);

            if (rng >= 0 && rng <= 1) {
                Vector2 spawnPos = new Vector2(30f, Random.Range(-0.6f, -2f));
                Instantiate(coins1, spawnPos, Quaternion.identity);
            }
            else if (rng >= 2 && rng <= 3) {
                Vector2 spawnPos = new Vector2(30f, 1.34f);
                Instantiate(coins2, spawnPos, Quaternion.identity);
            }
            else if (rng == 4) {
                Vector2 spawnPos = new Vector2(30f, Random.Range(-0.6f, -2f));
                Instantiate(coins3, spawnPos, Quaternion.identity);
            }
            else if (rng == 5) {
                Vector2 spawnPos = new Vector2(30f, -2.46f);
                Instantiate(spikes1, spawnPos, Quaternion.identity);
            }
            else if (rng == 6) {
                Vector2 spawnPos = new Vector2(30f, -1.076f);
                Instantiate(spikes2, spawnPos, Quaternion.identity);
            }
            else if (rng == 7) {
                Vector2 spawnPos = new Vector2(30f, 0f);
                Instantiate(platformsWCoins1, spawnPos, Quaternion.identity);
            }
            else if (rng == 8) {
                Vector2 spawnPos = new Vector2(30f, 0f);
                Instantiate(platformsWCoins2, spawnPos, Quaternion.identity);
                spawnTimer -= spawnDelay / 2;
            }
            else if (rng == 9) {
                Vector2 spawnPos = new Vector2(30f, -0.97f);
                Instantiate(platformsWSpikes, spawnPos, Quaternion.identity);
                spawnTimer -= spawnDelay / 2;
            }
            else if (rng == 10) {
                Vector2 spawnPos = new Vector2(30f, 0f);
                Instantiate(coins4, spawnPos, Quaternion.identity);
                spawnTimer -= spawnDelay / 2;
            }
            else if (rng == 11) {
                Vector2 spawnPos = new Vector2(30f, -1.026f);
                Instantiate(Obstacle1, spawnPos, Quaternion.identity);
            }
            else if (rng == 12) {
                Vector2 spawnPos = new Vector2(30f, -1.026f);
                Instantiate(Obstacle2, spawnPos, Quaternion.identity);
            }
            else if (rng == 12) {
                Vector2 spawnPos = new Vector2(30f, 0f);
                Instantiate(Obstacle2, spawnPos, Quaternion.identity);
            }
            else if (rng == 13) {
                Vector2 spawnPos = new Vector2(30f, -1.18f);
                Instantiate(Ramp, spawnPos, Quaternion.identity);
                spawnTimer -= spawnDelay;
            }

        }
    }


    private void Start() {
        PlayerControll.setTable(prefab, table);
    }

    // Update is called once per frame
    void Update() {
        if (gameRunning)
            spawn();
    }
}
