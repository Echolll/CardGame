using Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraAnimator _camera;

    [SerializeField] List<GameObject> _rotatedObjects;

    [Inject] UIMagicPanel _uiMagicPoints;

    [Inject(Id = "Player1")]private Player _firstPlayer;
    [Inject(Id = "Player2")]private Player _secondPlayer;
    private Player _currectPlayer;
     
    private bool _isStarted = true;
    
    private void Start()
    {
        StartingPlayer();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _isStarted == true)
        {
           _currectPlayer.SelectCardOnStart();
           _isStarted = false;
        }
    }

    private void StartingPlayer()
    {
        int randomNumber = Random.Range(0, 2);
        
        if (randomNumber == 0)
        {
            ChangePlayer(_firstPlayer, 0, 0);

        }
        else 
        {
            ChangePlayer(_secondPlayer, 1 , 180);
        }

        Debug.Log($"Стартует {_currectPlayer}");
    }

    public void ChangeStepPlayer()
    {
        if(_currectPlayer == _firstPlayer)
        {
            ChangePlayer(_secondPlayer, 1, 180);                   
        }
        else
        {
            ChangePlayer(_firstPlayer, 0, 0);                      
        }
        
        AnimationEnd();
        Debug.Log($"Смена стороны на {_currectPlayer} игрока!");
    }

    public void ChangePlayer(Player _nextPlayer, int playerNumber, int rotateY)
    {
        if(_currectPlayer != null) { _currectPlayer.ShowOrHideCard(false); }
        _currectPlayer = _nextPlayer;
        _uiMagicPoints.Player = _currectPlayer;
        RotateObjectsToNextPlayer(rotateY);
        _camera.OnStartPlayerAnim(playerNumber);
    }

    public void RotateObjectsToNextPlayer(float rotateToNextPlayer)
    {
        var rotateY = rotateToNextPlayer; 

        for(int i = 0; i < _rotatedObjects.Count; i++)
        {
            _rotatedObjects[i].transform.rotation = Quaternion.Euler(0 ,rotateY, 0);
        }

        rotateY = rotateY == 0 ? 180 : 0;
        _firstPlayer.transform.rotation = _secondPlayer.transform.rotation = Quaternion.Euler(0, 0, rotateY);                
    }

    private void AnimationEnd()
    {
        if(_camera.AnimationIsPlaying())    
        {           
            _currectPlayer.AddCardToHandAfterMove();
            _currectPlayer.OnPutCardToHand();
            _currectPlayer.ShowOrHideCard(true);
            _currectPlayer.RestoreManaPoint();
        }
    }
   
}
