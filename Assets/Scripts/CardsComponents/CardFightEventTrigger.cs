using Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardFightEventTrigger : MonoBehaviour, IDragHandler, IEndDragHandler
{
    CardPropities _cardPropities;
    Player _enemyPlayer;

    [SerializeField]bool _isReady = false;     

    private float _elapsedTime;
    private float _desiredDuration = 2f;
    Vector3 _startPos;
    
    private void OnEnable()
    {
        _startPos = transform.position;
        _cardPropities = GetComponent<CardPropities>();
        _enemyPlayer = GetComponent<CardPlayerService>().EnemyPlayer();
        AddMechanicComponent.AddComponent(gameObject, _cardPropities._cardAbility);
    }

    private void ResetTime() => _elapsedTime = 0;
    
    public void OnDrag(PointerEventData eventData)
    {
        if(ReadyToAttack())
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = Camera.main.transform.position.y;
            Vector3 newPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(newPos.x, 15, newPos.z);       
            GetComponent<CardScaleTrigger>().enabled = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.transform.CompareTag(_enemyPlayer.tag))
            {
                if (hit.transform.GetComponent<CardPropities>())
                {
                    AttackEnemy(hit.transform.gameObject);
                }
                else if(hit.transform.GetComponent<Player>())
                {
                    AttackEnemy(hit.transform.gameObject);
                }
            }
            else
            {
                StartCoroutine(MoveToPoint(null));
            }
        }
      
        GetComponent<CardScaleTrigger>().enabled = true;
    }

    private void AttackEnemy(GameObject enemyCard)
    {       
        StartCoroutine(HitEnemy(enemyCard));
    }
        
    private IEnumerator HitEnemy(GameObject _nextPoint)
    {      
        transform.position = _startPos;
        StartCoroutine(MoveToPoint(_nextPoint));
        yield return new WaitForSeconds(_desiredDuration + 1f);
        (_nextPoint.GetComponent<CardPropities>() != null ? (Action<GameObject>)TakeCardDamage : (Action<GameObject>)TakePlayerDamage)(_nextPoint);
        StartCoroutine(MoveToPoint(null));
        _isReady = false;
    }

    private IEnumerator MoveToPoint(GameObject _nextPoint)
    {
        Vector3 nextPos = _nextPoint != null ? _nextPoint.transform.position : _startPos;   
       
        ResetTime();
        while (_elapsedTime < _desiredDuration)
        {
            _elapsedTime += Time.deltaTime;
            float percentageComplete = _elapsedTime / _desiredDuration;
            transform.position = Vector3.Lerp(transform.position, nextPos, Mathf.SmoothStep(0, 1, percentageComplete));
            yield return null;
        }
    }

    private void TakeCardDamage(GameObject enemyCard)
    {
        var _enemyPropities = enemyCard.GetComponent<CardPropities>();
        if (_enemyPropities != null)
        {
            _enemyPropities._cardHealth -= _cardPropities._cardDamage;
            _cardPropities._cardHealth -= _enemyPropities._cardDamage;

            _enemyPropities.UpdateHealthAfterAttack();
            _cardPropities.UpdateHealthAfterAttack();

            _isReady = false;
        }
    }

    private void TakePlayerDamage(GameObject enemyPlayer)
    {
        var playerHealth = _enemyPlayer.GetComponent<Player>();
        var card = GetComponent<CardPropities>();

        playerHealth.Health -= card._cardDamage;
        playerHealth.UpdateHealthInfo();
    }

    private bool ReadyToAttack()
    {
        if(_isReady)
        {
            return true;
        }
        else { return false; }
    }

    public void CardCanAttack()
    {
        _isReady = true;
    }
}
