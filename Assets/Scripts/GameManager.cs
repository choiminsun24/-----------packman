using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum E_STATE
    {
        NONE = 0, 
        INTRO,
        START, 
        PLAY,
        GAMEOVER,
        GAMEWINNING,
        END
    }

    E_STATE state;

    public GameObject enemy;
    public GameObject player;

    public GameObject uiLogo;
    public GameObject uiStart;
    public GameObject uiOver;
    public GameObject uiWinning;
    public GameObject uiInfo;
    public BGM bgm;

    public Text scoreText;
    public Text lifeText;

    Player playerScript;

    int enemyNum;

    public List<GameObject> place;
    public List<Enemy> enemygroup;
    public List<Coin> coin;

    // Start is called before the first frame update
    void Start()
    {
        enemyNum = 5;
        playerScript = player.GetComponent<Player>();

        state = E_STATE.INTRO;
    }

    void Update()
    {
        GameState();
    }

    void GameState()
    {
        switch(state)
        {
            case E_STATE.NONE:
                break;

            case E_STATE.INTRO:
                state = E_STATE.NONE;
                GameIntro();
                break;

            case E_STATE.START:
                state = E_STATE.NONE;
                StartCoroutine(GameStart());
                break;

            case E_STATE.PLAY:
                Play();
                break;

            case E_STATE.GAMEOVER:
                state = E_STATE.NONE;
                GameOver();
                break;

            case E_STATE.GAMEWINNING:
                state = E_STATE.NONE;
                GameWinning();
                break;

            case E_STATE.END:
                state = E_STATE.NONE;
                GameEnd();
                break;
        }
    }

    void GameIntro()
    {
        uiInfo.SetActive(false);
        uiLogo.SetActive(true);
        uiOver.SetActive(false);
        uiStart.SetActive(false);
        uiWinning.SetActive(false);

        Time.timeScale = 0f;
    }

    IEnumerator GameStart()
    {
        uiInfo.SetActive(true);

        playerScript.init();

        yield return new WaitForSeconds(1.5f);

        bgm.change();
        uiStart.SetActive(false);
        CreateEnemy();

        state = E_STATE.PLAY;
    }

    void Play()
    {
        checkWinning();
        checkGameEnd();

        lifeText.text = playerScript.life.ToString();
        scoreText.text = playerScript.score.ToString();
    }

    void checkWinning()
    {
        if (playerScript.score >= coin.Count)
        {
            state = E_STATE.GAMEWINNING;
        }
    }

    void checkGameEnd()
    {
        Debug.Log(playerScript.life);
        if (playerScript.life <= 0)
        {
            state = E_STATE.GAMEOVER;
        }
    }

    void GameOver()
    {
        //게임오버 UI 출력
        uiLogo.SetActive(false);
        uiOver.SetActive(true);
        uiStart.SetActive(false);
        uiWinning.SetActive(false);

        DestroyAllEnemy();
        Time.timeScale = 0f;
    }

    void GameWinning()
    {
        uiLogo.SetActive(false);
        uiOver.SetActive(false);
        uiStart.SetActive(false);
        uiWinning.SetActive(true);
        Time.timeScale = 0f;
    }

    void GameEnd()
    {
        uiLogo.SetActive(false);
        uiOver.SetActive(false);
        uiStart.SetActive(false);
        uiWinning.SetActive(false);


        Application.Quit();
    }

    public void onStart()
    {
        Time.timeScale = 1f;
        state = E_STATE.START;

        uiLogo.SetActive(false);
        uiStart.SetActive(true);
    }

    public void onGameRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void onGameEnd()
    {
        state = E_STATE.END;

    }

    void CreateEnemy()
    {
        for (int i = 0; i < enemyNum; i++)
        {
            int rand = Random.Range(0, place.Count - 1);

            Vector3 vector_enemy = place[rand].transform.position;
            vector_enemy = new Vector3(vector_enemy.x, 1, vector_enemy.z);

            GameObject go = Instantiate(enemy, vector_enemy, Quaternion.identity);

            Enemy enm = go.transform.GetComponent<Enemy>();
            enm.SetGameManager(this);
            enemygroup.Add(enm);
        }
    }

    void DestroyAllEnemy()
    {
        for (int i = 0; i < enemygroup.Count; i++)
        {
            Destroy(enemygroup[i].gameObject);
            Destroy(enemygroup[i]);
        }
        enemygroup.Clear();
    }
}
