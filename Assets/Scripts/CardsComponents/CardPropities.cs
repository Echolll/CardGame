using Cards;
using TMPro;
using UnityEngine;


public class CardPropities : MonoBehaviour
{  
    [Header("Основные параметры карты:")]
    [Range(0, 10)] public int _cardCost;
    public int _cardDamage;
    public int _cardHealth;
    public CardAbility _cardAbility;

    [Space, Header("Наименование карты:")]
    public string _cardName;
    public string _cardType;
    public string _cardDescription;

    [Space, Header("Изображенние персонажа:")]
    public Texture _cardTexture;

    [Space, Header("Ссылки на объекты:")]
    [SerializeField] private TextMeshPro _cost;
    [SerializeField] private TextMeshPro _damage;
    [SerializeField] private TextMeshPro _health;
    [SerializeField] private TextMeshPro _name;
    [SerializeField] private TextMeshPro _type;
    [SerializeField] private TextMeshPro _description;
    [SerializeField] private MeshRenderer _material;

    [Space, Header("Сыллка на аниматор:")]
    [SerializeField] private CardAnimationController _cardAnim;

    Player _currectPlayer;

    private void OnEnable()
    {
        _currectPlayer = GetComponent<CardPlayerService>().CurrectPlayer();

        _material.material.mainTexture = _cardTexture;

        //Material mat = _material.material;
        //mat.mainTexture = _cardTexture;
    }
   
    public void OnUpdateCardData(CardPropertiesData cardData)
    {
        _cardCost = cardData.Cost;
        _cardDamage = cardData.Attack;
        _cardHealth = cardData.Health;
        _cardName = cardData.Name;
        _cardTexture = cardData.Texture;
        _cardDescription = cardData.Description;
        _cardType = cardData.Type.ToString();
        _cardAbility = cardData.Ability;

        gameObject.name = _cardName;
        OnUpdateUIData();
    }

    private void OnUpdateUIData()
    {
        _cost.text = _cardCost.ToString();
        _damage.text = _cardDamage.ToString();
        _health.text = _cardHealth.ToString();
        _name.text = _cardName.ToString();
        _description.text = _cardDescription.ToString();
        _type.text = _cardType;

             
    }
    
    public void UpdateHealthAfterAttack()
    {
        _health.text = _cardHealth.ToString();
        if( _cardHealth <= 0)
        {
           Debug.Log($"Я {gameObject.name} уничтожен и удалён из списка у {_currectPlayer}!");
           _currectPlayer.RemoveCardOnTable(gameObject);
           Destroy(gameObject);
        }   
    }

    public void UpdateDataHealthAndAttack()
    {
        _health.text = _cardHealth.ToString();
        _damage.text = _cardDamage.ToString();
    }
}

    