using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class ReadyCountDownInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<ReadyCountDown.ReadyCountDownModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.ReadyCountDownPresenter>().AsCached().NonLazy();
		}
	}
}