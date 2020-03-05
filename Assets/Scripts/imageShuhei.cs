using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageShuhei : MonoBehaviour {

    gameManager ballManager;

    bool swung = false;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x > 250.0f && Input.mousePosition.y < 400.0f && Input.GetMouseButtonDown(0) &&  swung == false)
        {
            anim.SetTrigger("swing");
            swung = true;
        }
    }
}
