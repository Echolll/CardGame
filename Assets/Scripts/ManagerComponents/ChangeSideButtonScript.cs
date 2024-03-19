using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChangeSideButtonScript : MonoBehaviour
{
    [Inject]private GameManager gameManager;

    public void OnPointerClick()
    {
        gameManager.ChangeStepPlayer();
    }
}
