using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthB_CharacterAnimationController : MonoBehaviour
{
    private Animator _animator;


    private readonly int _fall = Animator.StringToHash($"{nameof(_fall)}");
    private readonly int _idle = Animator.StringToHash($"{nameof(_idle)}");
    private readonly int _work = Animator.StringToHash($"{nameof(_work)}");
    private readonly int _standInspect = Animator.StringToHash($"{nameof(_standInspect)}");
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnim(int animid)
    {
        _animator.SetBool(animid,true);
    }



    public void CancelAllParameter()
    {
        _animator.SetBool(_fall,false);
        _animator.SetBool(_idle,false);
        _animator.SetBool(_work,false);
        _animator.SetBool(_standInspect,false);
    }

    public void PlayIdleAnim()
    {
        CancelAllParameter();
        _animator.SetBool(_idle,true);
    }
    
    public void PlayFallAnim()
    {
        CancelAllParameter();
        _animator.SetBool(_fall,true);
    }
    
    public void PlayWorkAnim()
    {
        CancelAllParameter();
        _animator.SetBool(_work,true);
    }
    
    public void PlayInspectAnim()
    {
        CancelAllParameter();
        _animator.SetBool(_standInspect,true);
    }
}
