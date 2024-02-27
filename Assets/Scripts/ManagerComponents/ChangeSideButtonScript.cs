using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChangeSideButtonScript : MonoBehaviour
{
    [Inject]private GameManager gameManager;

    public void OnPointerEnter()
    {
        Debug.Log("����");
    }

    public void OnPointerExit()
    {
        Debug.Log("�����");
    }

    public void OnPointerClick()
    {
        gameManager.ChangeStepPlayer();
    }
}
