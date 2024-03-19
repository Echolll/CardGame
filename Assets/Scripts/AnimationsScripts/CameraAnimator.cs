using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    [SerializeField] Animator _cameraAnim;

    public void NextStateAnimation(bool boolState)
    {
        _cameraAnim.SetBool("next_turn", boolState);
    }

    public void OnStartPlayerAnim(int playerSide)
    {
        _cameraAnim.SetInteger("StateCamera1", playerSide);
    }

    public bool AnimationIsPlaying()
    {
        if(_cameraAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            return true;
        }
        else { return false; }
    }
}
