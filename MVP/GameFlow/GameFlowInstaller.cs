using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class GameFlowInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<GameFlow.GameFlowModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.GameFlowPresenter>().AsCached().NonLazy();
		}
	}
}