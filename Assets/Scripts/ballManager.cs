using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballManager : MonoBehaviour {

    System.Random r = new System.Random();

    AudioSource swing;
    public AudioClip[] sound = new AudioClip[4];

    public bool swung = false;//スイングした？
    public bool hitted = false;//あたった？
    public bool pitch = false;
    float hitspot;

    public bool through = false;

    float wait = 1.2f;

    public Rigidbody2D rb;

    public GameObject Ball;

    Animator animator;

    int ballKind;
    int ballX;

    // Use this for initialization
    void Start ()
    {

        ballX = 0;
        ballKind = r.Next(9);

        swing = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;



    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.mousePosition.x > 250.0f && Input.mousePosition.y < 400.0f && Input.GetMouseButtonDown(0) && swung == false)
        {
            Debug.Log(this.transform.position);
            if (this.transform.position.x >= 1.5 && this.transform.position.x <= 2.5)
            {
                hit();
            }

            swing.PlayOneShot(swing.clip);
            swung = true;   //スイングした
        }

        pitching(ballKind);
            
    }

    
    public void hit()
    {
        

        hitted = true;
        hitspot = (this.transform.position.x - 2.0f);  //2.0の位置が基準となって威力が+-される

        float pow = 10.0f - Mathf.Abs( hitspot*25 );
        Debug.Log(pow);

        if (pow >= 8.5f)
        {
            GetComponent<AudioSource>().PlayOneShot(sound[3]);
        }else if(pow >= 7.0f)
        {
            GetComponent<AudioSource>().PlayOneShot(sound[2]);
        }else if (pow >= 2.0f)
        {
            GetComponent<AudioSource>().PlayOneShot(sound[1]);
        }else
        {
            GetComponent<AudioSource>().PlayOneShot(sound[0]);
        }

        //this.transform.position += new Vector3(-5.1f, 0, 0);
        rb.AddForce(transform.up * 10.0f  , ForceMode2D.Impulse);
        rb.AddForce(transform.right * -1.5f * pow , ForceMode2D.Impulse);
        rb.gravityScale = 1;  //重力が加わる
        
    }



    public void pitching(int ballKind)
    {
    
        if (hitted == false && through == false)
        {
            switch (ballKind) {
                case 0:
                    this.transform.position += new Vector3(0.1f, 0, 0);
                    break;
                case 1:
                    this.transform.position += new Vector3(0.15f, 0, 0);
                    break;
                case 2:
                    this.transform.position += new Vector3(0.07f, 0, 0);
                    break;
                case 3:
                    this.transform.position += new Vector3(0.2f, 0, 0);
                    break;
                case 4:
                    if (this.transform.position.x >= 0.0f && ballX==0)
                    {
                        ballX = 1;
                        this.transform.position = new Vector3(-1.3f, 0.5f, 0);
                    }
                    this.transform.position += new Vector3(0.1f, 0, 0);
                    break;
                case 5:
                    if (this.transform.position.x <= -0.5f)
                    {
                        this.transform.position += new Vector3(0.15f, 0, 0);
                    }
                    else
                    {
                        this.transform.position += new Vector3(0.08f, 0, 0);
                    }
                    break;
                case 6:
                    if (this.transform.position.x <= 0.0f)
                    {
                        this.transform.position += new Vector3(0.09f, 0, 0);
                    }
                    else
                    {
                        this.transform.position += new Vector3(0.13f, 0, 0);
                    }
                    break;
                case 7:
                    if (this.transform.position.x > -0.1f && this.transform.position.x < 0.1f)
                    {
                        this.transform.position += new Vector3(0.001f, 0, 0);
                    }else 
                    {
                        this.transform.position += new Vector3(0.1f, 0, 0);
                    }                  
                    break;
                case 8:
                    if (this.transform.position.x > -1.0f && this.transform.position.x < 0.0f)
                    {
                        this.transform.position += new Vector3(0.1f, 0, 0);
                        this.transform.position = new Vector3(this.transform.position.x, 10000.0f, 0);
                    }
                    else
                    {
                        this.transform.position += new Vector3(0.1f, 0, 0);
                        this.transform.position = new Vector3(this.transform.position.x, 0.5f, 0);
                    }
                    break;
                case 9:
                    
                    this.transform.position += new Vector3(Random.Range(0.05f,0.15f), 0, 0);
                    break;
                case 10:

                    this.transform.position += new Vector3(Random.Range(0.06f, 0.16f), 0, 0);
                    break;
                default:
                    this.transform.position += new Vector3(0.1f, 0, 0);
                    break;
            }
        }
    }
    
}
