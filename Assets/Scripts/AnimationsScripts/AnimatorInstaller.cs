using UnityEngine;
using Zenject;

public class AnimatorInstaller : MonoInstaller
{
    [SerializeField]private UIPanelAnimationController _panelController;

    public override void InstallBindings()
    {
        Container.Bind<UIPanelAnimationController>().FromInstance(_panelController).AsCached();
    }
}