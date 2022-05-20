using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class MainGameTimeInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<MainGameTime.MainGameTimeModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.MainGameTimePresenter>().AsCached().NonLazy();
		}
	}
}