using System.Collections;
using UnityEngine;

public class UIPanelAnimationController : MonoBehaviour
{
    private Animator _uiPanelAnimator;

    private void OnEnable() => _uiPanelAnimator = GetComponent<Animator>();

    public void ChooseCardOnStart()
    {
        gameObject.SetActive(true);
        _uiPanelAnimator.SetTrigger("is_start");       
    }

    public void ChooseCardOnEnd()
    {       
        _uiPanelAnimator.SetTrigger("is_end");      
    }

}
