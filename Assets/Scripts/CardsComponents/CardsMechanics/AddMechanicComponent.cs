using Cards;
using UnityEngine;

public static class AddMechanicComponent
{
    public static void AddComponent(GameObject card, CardAbility ability)
    {
        switch (ability) 
        {
          case CardAbility.None:              
                break;
                
          case CardAbility.Taunt:
                AddingComponent<Taunt>(card);
                break;

          case CardAbility.BattleCry:
                AddingComponent<BattleCry>(card);
                break;
                
          case CardAbility.PassiveBuff:
                AddingComponent<PassiveBuff>(card);
                break;
                
          case CardAbility.Charge:
                AddingComponent<Charge>(card);
                break;        
        }
    }

    private static void AddingComponent<T>(GameObject card) where T : Component
    {
        card.AddComponent<T>();
    }

}
