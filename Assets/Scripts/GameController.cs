using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text restartText;
    public Text gameOverText;
    public Text ScoreText;
    public Text WinText;

    private int score;
    private bool gameover;
    private bool restart;

    void Start()
    {
        gameover = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        WinText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];

                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameover)
            {
                restartText.text = "Press 'K' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            WinText.text = "You Win!" + "\n" + "Game Created by Kaitlin Long";
            gameover = true;
            restart = true;
        }
    }

    public void GameOver()
    {
        if(score >= 100)
        {
            gameOverText.text = "";
        }
        else
            gameOverText.text = "Game Over" + "\n" + "Game Created by Kaitlin Long";

        gameover = true;
    }
}
