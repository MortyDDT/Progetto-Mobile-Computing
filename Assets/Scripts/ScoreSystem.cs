using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int theScore;

    void Update() {
        scoreText.GetComponent<TMP_Text>().text = "Score: " + theScore;
    }
}
