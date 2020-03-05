using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imagePitcher : MonoBehaviour {
    gameManager ballManager;

    Animator anim;

    string animename;
    // Use this for initialization
    void Start()
    {
        // animator.Play("image_pitcher");

        anim = GetComponent<Animator>();
        anim.SetTrigger("pitch");
    }

    // Update is called once per frame
    void Update () {
        animename = "pitcher_anime";
        WaitAnimationEnd(animename);
        var a = StartCoroutine("WaitAnimationEnd", animename);
        Debug.Log(a);
    }

    //アニメーション終了を判定するコルーチン
    private IEnumerator WaitAnimationEnd(string animatorName)
    {
        bool finish = false;
        while (!finish)
        {
            AnimatorStateInfo nowState = anim.GetCurrentAnimatorStateInfo(0);
            if (nowState.IsName(animatorName))
            {
                finish = true;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }



}
