using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UIManager : Singleton<UIManager>
{
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }





    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        animator.SetBool("fade_in", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("fade_in", false);

        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        animator.SetBool("fade_out", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("fade_out", false);

    }
}