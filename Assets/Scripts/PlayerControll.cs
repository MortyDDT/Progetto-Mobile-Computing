using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private PlayfabManager pfm;
    Rigidbody2D body;
    private bool isTouchingGround;
    float jumpPower = 12f;


    public GameObject gameOver;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    public AudioSource jumpSound;
    public AudioSource deathSound;

    public static GameObject rowPrefab;
    public static Transform Table;

    public static void setTable(GameObject rp, Transform t) {
        rowPrefab = rp;
        Table = t;
    }

    // functions that waits a certain time before executing a command used with -> StartCoroutine(waitAndRun(sec));
    public IEnumerator waitAndRun(int sec) {
        yield return new WaitForSeconds(sec);
        // RUN YOUR CODE HERE ...

        pfm.GetLeaderboardAroundPlayer();

    }

    private void die() {
        body.isKinematic = true;
        gameObject.GetComponent<Animator>().enabled = false;
        deathSound.Play();

        pfm.GetComponent<PlayfabManager>().SendLeaderboard(ScoreSystem.theScore);

        GameController.gameRunning = false;
        gameOver.SetActive(true);

        StartCoroutine(waitAndRun(1));

    }

    // Start is called before the first frame update
    void Start(){
        pfm = GameObject.FindObjectOfType(typeof(PlayfabManager)) as PlayfabManager;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        if (GameController.gameRunning && transform.position.x < -11.4)
            die();

        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (GameController.gameRunning && isTouchingGround && Input.GetMouseButtonDown(0)) {
            jumpSound.Play();
            body.velocity = new Vector2(0f, jumpPower);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (GameController.gameRunning && collision.gameObject.tag == "Killer")
            die();
    }
}
