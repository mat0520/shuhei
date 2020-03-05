using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {

    Rigidbody2D rd;

    public bool swung = false;//スイングした？
    bool hitted = false;//あたった？
    bool ground = false;
    float hitspot;

    public Rigidbody2D rb;

    public GameObject Ball;

    public gameManager ballManager;
    public GameObject imagePitcher;
    Animator animator;
    
    const float waitTime = 1.3f;

    AudioSource pitch;

    //challenge
    static int challenge = 0;
    //スコア
    public Text scoreText;
    public Text highScoreText;
    public Text scoreListText;
    public Text totalText;
    public Text totalHighText;
    public Text StrikeCountText;

    //表示板
    public Text score1_10;

    static float score = 0.0f;
    static float highScore;
    static float totalHighScore;
  //  static float[] scoreList = new float[10];
    static List<float> scoreList = new List<float>();
    static int num = 0;
    static int miss = 0;

    bool gaming = true;
    
    //ゲームモード
    public Text gameModeText;
    public static int gameMode = 0;


    public static int getGameMode()
    {
        return gameMode;
    }

    // Use this for initialization
    void Start() {
        if (num == -1)
        {
            num = 0;
        }

        rd = Ball.GetComponent<Rigidbody2D>();
        pitch = GetComponent<AudioSource>();

        score = 0.0f;
        scoreDraw(scoreList);

        scoreText.text = "0.00m";
        highScoreText.text = highScore.ToString("#0.00") + "m";

        if (gameManager.gameMode == 0)
        {
            gameModeText.text = "10球勝負";
        }
        else if (gameManager.gameMode == 1)
        {
            gameModeText.text = "三球三振";
        }

        Invoke("pitchSound", waitTime-0.1f);

        Invoke("ballActive", waitTime);

    }


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Debug.Log("LeftClick:" + Input.mousePosition.x);

        //スイング判定
        if (Input.mousePosition.x > 250.0f && Input.mousePosition.y < 400.0f && Input.GetMouseButtonDown(0) && swung == false)
        {
            Debug.Log(Ball.transform.position);

            swung = true;   //スイングした
            Debug.Log(this.transform.position);
            if (this.transform.position.x >= 1.5 && this.transform.position.x <= 2.5)
            {
                hitted = true;
            }
        }

        //ボール飛距離更新
        if (rd.gravityScale != 0 && Ball.transform.position.x <= 1.5f && Ball.transform.position.y >= 0.0f)
        {
            score = (1.5f - Ball.transform.position.x) * 5.2f;
            scoreText.text = score.ToString("#0.00") + "m";
        }


        //ボールが着地
        if (Ball.transform.position.y <= 0.0f && ground == false)
        {
            ground = true;
        }

        if (num < 10 && num >= 0 && gameMode == 0 || miss < 3 && gameMode == 1)
        {
            if (gaming == true)
            {
                Debug.Log("ゲーム中" + gaming);

                if (gameMode == 1)
                {
                    StrikeCountText.text = "S:";
                    for (int i = 0; i < miss; i++)
                    {
                        StrikeCountText.text += "●";
                    }
                }

                //1球ごとの処理
                if (Ball.transform.position.y <= -6.0f || Ball.transform.position.x >= 12.0f)
                {
                    if (swung == false)
                    {
                        score = 0.0f;
                    }

                    if (rd.gravityScale == 0)
                    {
                        //空振り
                        miss++;
                        //gameModeText.text += "×";   //確認Debug用

                    }
                    Debug.Log("miss:" + miss);

                    if (score >= highScore)
                    {
                        highScore = score;
                        highScoreText.text = highScore.ToString("#0.00") + "m";
                        PlayerPrefs.SetFloat("highScore", highScore);
                    }
                    // scoreList[num] = score;
                    scoreList.Add(score);
                    num++;
                    scoreDraw(scoreList);

                    Ball.transform.position = new Vector3(-300.0f, 0, 0);
                    

                    swung = false;

                    if (gameMode == 0)
                    {   //10球勝負
                        if (num < 10)
                        {
                            gaming = false;
                            SceneManager.LoadScene("gameScene");
                        }
                    }
                    else if (gameMode == 1)
                    //三球三振
                    {
                        if (miss < 3)
                        {
                            gaming = false;
                            SceneManager.LoadScene("gameScene");
                        }
                    }


                }
            }
        }
        if (Ball.transform.position.x < -200.0f)
        {
            Ball.transform.position = new Vector3(-300.0f, 0, 0);
        }
    }

    void pitchSound()
    {
        pitch.PlayOneShot(pitch.clip);
    }

    void ballActive()
    {
        Ball.SetActive(true);
    }

    void scoreDraw(List<float> scoreList)
    {
        float total = 0.0f;

        //表示の変更
        score1_10.text = "";
        for (int i = 1; i <= 10; i++)
        {
            score1_10.text += ((num / 10) * 10 + i).ToString("") + ":\n";
        }


        highScore = PlayerPrefs.GetFloat("highScore");
        totalHighScore = PlayerPrefs.GetFloat("totalHighScore" + gameManager.gameMode.ToString());

        int count_s = (num / 10) * 10;
        scoreListText.text = "";
        for (var i = count_s; i < num; i++)
        {
            scoreListText.text += scoreList[i].ToString("#0.00") + "m\n";
        }
        for (var i = 0; i < num; i++)
        {
            total += scoreList[i];
        }
        totalText.text = total.ToString("#0.00") + "m";

        if (total >= totalHighScore)
        {
            totalHighScore = total;
        }
        totalHighText.text = totalHighScore.ToString("#0.00") + "m";

        PlayerPrefs.SetFloat("totalHighScore" + gameManager.gameMode.ToString(), totalHighScore);

        //10球勝負
        if (gameMode == 0)
        {
            if (num >= 10)
            {
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking(total,0);
            }

        }
        else if (gameMode == 1)
        //三球三振
        {
            if (miss >= 3)
            {
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking(total,1);
            }
        }      
    }


    public void reset()
    {
        swung = false;//スイングした？
        hitted = false;//あたった？
        ground = false;


        //challenge
        challenge = 0;
        //スコア

        miss = 0;
        score = 0.0f;
        scoreList = new List<float>();
        num = 0;

        SceneManager.LoadScene("gameScene");

    }
}


