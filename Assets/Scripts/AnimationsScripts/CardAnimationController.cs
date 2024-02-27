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

    public void OnHideOrShowCard(bool cardBool)
    {
        _cardAnim.enabled = true;
        _cardAnim.SetBool("hide_card", cardBool);
    }   

    public void OnAnimatorDisable()
    {
        _cardAnim.enabled = false;
    }
}
