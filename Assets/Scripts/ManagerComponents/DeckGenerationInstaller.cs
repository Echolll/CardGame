using UnityEngine;
using Zenject;

public class DeckGenerationInstaller : MonoInstaller
{
    [SerializeField] private Player _playerOne;
    [SerializeField] private Player _playerTwo;

    [SerializeField] private GameObject _cardPrefab;

    public override void InstallBindings()
    {
        Container.Bind<Player>().WithId("Player1").FromInstance(_playerOne).AsCached();
        Container.Bind<Player>().WithId("Player2").FromInstance(_playerTwo).AsCached();

        Container.BindFactory<Player, CardPlayerService, CardPlayerService.Factory>().FromComponentInNewPrefab(_cardPrefab);       
    }
}