using Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraAnimator _camera;
    
    private StepType _stepType;

    private void Start()
    {
        StartingPlayer();
    }

    private void StartingPlayer()
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
        {
            _stepType = StepType.FirstPlayer;
            _camera.OnStartPlayerAnim(0);
            Debug.Log("�������� 1-�� �����");

        }
        else 
        {
            _stepType= StepType.SecondPlayer;
            _camera.OnStartPlayerAnim(1);
            Debug.Log("�������� 2-�� �����");
        }

        Debug.Log($"{_stepType}");
    }

    public void ChangeStepPlayer()
    {
        if(_stepType == StepType.FirstPlayer)
        {
            _stepType = StepType.SecondPlayer;
            _camera.OnStartPlayerAnim(1);
            Debug.Log("����� ������� �� ������� ������!");
        }
        else
        {
            _stepType = StepType.FirstPlayer;
            _camera.OnStartPlayerAnim(0);
            Debug.Log("����� ������� �� ������� ������!");
        }
    }
}
