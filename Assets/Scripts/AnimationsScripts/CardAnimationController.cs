using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimationController : MonoBehaviour
{
    [SerializeField] Animator _cardAnim;

    private void Awake()
    {
        _cardAnim = GetComponent<Animator>();
    }

    public void PutCardAnimation(int animStatus)
    {
        _cardAnim.enabled = true;
        _cardAnim.SetInteger("card_animation_status", animStatus);
    }   

    public void HideOrShowCard(string name)
    {
        _cardAnim.enabled = true;
        _cardAnim.SetTrigger(name);
    }

    public void OnAnimatorDisable()
    {
        _cardAnim.enabled = false;
    }
}
